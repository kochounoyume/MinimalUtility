#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
#if ENABLE_UNITASK
using Cysharp.Threading.Tasks;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace MinimalUtility.Pool
{
    using Internal;

    public sealed class GameObjectPool<T> : IDisposable, IReadOnlyCollection<T> where T : MonoBehaviour
    {
        /// <summary>
        /// プール対象のオリジナル.
        /// </summary>
        private readonly T _original;

        /// <summary>
        /// プールのスタック.
        /// </summary>
        private readonly Stack<T> _pool;

        /// <inheritdoc/>
        public int Count => _pool.Count;

        /// <summary>
        /// 破棄済みかどうか.
        /// </summary>
        public bool isDisposed { get; private set; }

#if ENABLE_UNITASK
        static GameObjectPool()
        {
            // Awake処理のスパイク軽減
            AsyncInstantiateOperation.SetIntegrationTimeMS(20);
        }
#endif

        public GameObjectPool(T original, int capacity)
        {
            this._original = original;
            _pool = new Stack<T>(capacity);
        }

        /// <summary>
        /// プールからインスタンスを取得する.
        /// </summary>
        /// <returns>プールから取得したインスタンス.</returns>
        public T Get()
        {
            ThrowIfDisposed();
            var result = _pool.TryPop(out var v) ? v : Object.Instantiate(_original, DontDestroyObject.Root);
            result.gameObject.SetActive(true);
            return result;
        }

        /// <summary>
        /// プールからインスタンスを取得する(usingステートメントで使用するためのメソッド).
        /// </summary>
        /// <returns>インスタンスをプールに戻すための<see cref="IDisposable"/>.</returns>
        public ValueDisposable.Disposable<(GameObjectPool<T>, T)> GetScope(out T instance)
        {
            instance = Get();
            return ValueDisposable.Create((pool: this, instance), static args => args.pool.Release(args.instance));
        }

        /// <summary>
        /// 事前にプールを確保する.
        /// </summary>
        /// <param name="count"></param>
        public void Preallocate(int count)
        {
            ThrowIfDisposed();
            var remains = count - _pool.Count;
            for (int i = 0; i < remains; i++)
            {
                var instance = Object.Instantiate(_original, DontDestroyObject.Root);
                instance.gameObject.SetActive(false);
                _pool.Push(instance);
            }
            _pool.TrimExcess();
        }

#if ENABLE_UNITASK
        /// <summary>
        /// プールから非同期でインスタンスを取得する.
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <returns>取得したインスタンス.</returns>
        public async UniTask<T> GetAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (_pool.TryPop(out var v))
            {
                return v;
            }

            var results = await Object.InstantiateAsync(_original, DontDestroyObject.Root).WithCancellation(cancellationToken);
            return results[0];
        }

        /// <summary>
        /// プールから非同期でインスタンスを取得する(usingステートメントで使用するためのメソッド).
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <returns>取得したインスタンスと解放用の<see cref="System.IDisposable"/>.</returns>
        public async UniTask<ValueDisposable.Disposable<(GameObjectPool<T>, T)>> GetScopeAsync(CancellationToken cancellationToken)
        {
            var instance = await GetAsync(cancellationToken);
            return ValueDisposable.Create((pool: this, instance), static args => args.pool.Release(args.instance));
        }

        /// <summary>
        /// 事前にプールを確保する.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="cancellationToken"></param>
        public async UniTask PreallocateAsync(int count, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var remains = count - _pool.Count;
            if (remains <= 0) return;

            var results = await Object.InstantiateAsync(_original, remains, DontDestroyObject.Root).WithCancellation(cancellationToken);
            foreach (var item in results)
            {
                item.gameObject.SetActive(false);
                _pool.Push(item);
            }
            _pool.TrimExcess();
        }
#endif

        /// <summary>
        /// プールにインスタンスを返却する.
        /// </summary>
        /// <param name="element">返却するインスタンス.</param>
        public void Release(T element)
        {
            ThrowIfDisposed();
            element.gameObject.SetActive(false);
            _pool.Push(element);
        }

        /// <summary>
        /// プールをクリアする.
        /// </summary>
        public void Clear()
        {
            ThrowIfDisposed();
            foreach (var item in _pool)
            {
                Object.Destroy(item.gameObject);
            }
            _pool.Clear();
        }

        public Stack<T>.Enumerator GetEnumerator() => _pool.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => _pool.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => _pool.GetEnumerator();

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!isDisposed)
            {
                Clear();
                isDisposed = true;
            }
        }

        /// <summary>
        /// 破棄済みかどうかをチェックする.
        /// </summary>
        /// <exception cref="ObjectDisposedException">破棄済みの場合にスローされる例外.</exception>
        private void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(nameof(GameObjectPool<T>));
            }
        }
    }
}