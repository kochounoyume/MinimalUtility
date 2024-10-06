using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MinimalUtility
{
    /// <summary>
    /// ゲームオブジェクトのプール.
    /// <remarks>
    /// 単一のルートオブジェクト下でのプーリングを想定.
    /// </remarks>
    /// </summary>
    /// <typeparam name="T">プールする<see cref="MonoBehaviour"/>の型.</typeparam>
    public class GameObjectPool<T> : IDisposable where T : MonoBehaviour
    {
        /// <summary>
        /// プール対象のオリジナル.
        /// </summary>
        protected readonly T original;

        /// <summary>
        /// 生成するインスタンスの親オブジェクト.
        /// </summary>
        protected readonly Transform root;

        /// <summary>
        /// プールのスタック.
        /// </summary>
        protected readonly Stack<T> pool;

        /// <summary>
        /// 破棄トークン.
        /// </summary>
        protected CancellationToken DisposeToken => cts.Token;

        private readonly CancellationTokenSource cts = new ();
        private bool isDisposed;

        /// <summary>
        /// 現在プールにあるアイテムの総量.
        /// </summary>
        public virtual uint CountInactive
        {
            get => (uint)pool.Count;
            set
            {
                ThrowIfDisposed();
                if (value == pool.Count) return;
                if (value < pool.Count)
                {
                    var count = pool.Count - (int)value;
                    for (int i = 0; i < count; i++)
                    {
                        Object.Destroy(pool.Pop().gameObject);
                    }
                }
                else
                {
                    var count = (int)value - pool.Count;
                    for (int i = 0; i < count; i++)
                    {
                        pool.Push(Object.Instantiate(original, root));
                    }
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObjectPool{T}"/> class.
        /// </summary>
        /// <param name="original">プール対象のオブジェクトのオリジナル.</param>
        /// <param name="root">生成するインスタンスの親オブジェクト.</param>
        /// <param name="capacity">プールの初期容量.</param>
        public GameObjectPool(T original, Transform root,　int capacity)
        {
            this.original = original;
            this.root = root;
            this.pool = new Stack<T>(capacity);
        }

        /// <summary>
        /// プールからインスタンスを取得する.
        /// </summary>
        /// <returns>プールから取得したインスタンス.</returns>
        public T Get()
        {
            ThrowIfDisposed();
            var result = pool.TryPop(out var v) ? v : Object.Instantiate(original, root);
            result.gameObject.SetActive(true);
            return result;
        }

        /// <summary>
        /// プールからインスタンスを取得する(usingステートメントで使用するためのメソッド).
        /// </summary>
        /// <returns>インスタンスをプールに戻すための<see cref="IDisposable"/>.</returns>
        public PooledObject<T> GetScope() => CreatePooledObject(Get(), this);

        /// <summary>
        /// プールからインスタンスを取得する(usingステートメントで使用するためのメソッド).
        /// </summary>
        /// <param name="instance">取得したインスタンス.</param>
        /// <returns>インスタンスをプールに戻すための<see cref="IDisposable"/>.</returns>
        public PooledObject<T> GetScope(out T instance) => CreatePooledObject(instance = Get(), this);

        /// <summary>
        /// プールにインスタンスを返却する.
        /// </summary>
        /// <param name="element">返却するインスタンス.</param>
        public void Release(T element)
        {
            ThrowIfDisposed();
            element.gameObject.SetActive(false);
            pool.Push(element);
        }

        /// <summary>
        /// プールをクリアする.
        /// </summary>
        public void Clear()
        {
            foreach (var item in pool)
            {
                Object.Destroy(item.gameObject);
            }
            pool.Clear();
        }

        /// <inheritdoc/>
        void IDisposable.Dispose()
        {
            if (!isDisposed)
            {
                Clear();
                isDisposed = true;
                cts.Cancel();
                cts.Dispose();
            }
        }

        /// <summary>
        /// <see cref="PooledObject{T}"/>の生成メソッド.
        /// </summary>
        /// <param name="instance">インスタンス.</param>
        /// <param name="pool">プール.</param>
        /// <returns>生成した<see cref="PooledObject{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static PooledObject<T> CreatePooledObject(T instance, GameObjectPool<T> pool) => new (instance, pool);

        /// <summary>
        /// 破棄済みかどうかをチェックする.
        /// </summary>
        /// <exception cref="ObjectDisposedException">破棄済みの場合にスローされる例外.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}