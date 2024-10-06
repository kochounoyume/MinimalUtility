using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MinimalUtility.Tasks
{
    /// <summary>
    /// 非同期でGameObjectをプールするクラス.
    /// </summary>
    /// <typeparam name="T">プールする<see cref="MonoBehaviour"/>の型.</typeparam>
    public class AsyncGameObjectPool<T> : GameObjectPool<T> where T : MonoBehaviour
    {
        /// <inheritdoc/>
        public override uint CountInactive
        {
            get => base.CountInactive;
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
                    UniTask.Void(static async args =>
                    {
                        var (self, count) = args;
                        var results
                            = await Object.InstantiateAsync(self.original, self.root).ToUniTask(cancellationToken: self.DisposeToken);
                        for (int i = 0; i < count; i++)
                        {
                            self.pool.Push(results[i]);
                        }
                    }, (self: this, count));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncGameObjectPool{T}"/> class.
        /// </summary>
        /// <param name="original">プール対象のオブジェクトのオリジナル.</param>
        /// <param name="root">生成するインスタンスの親オブジェクト.</param>
        /// <param name="capacity">プールの初期容量.</param>
        public AsyncGameObjectPool(T original, Transform root, int capacity) : base(original, root, capacity)
        {
        }

        /// <summary>
        /// プールから非同期でインスタンスを取得する.
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <returns>取得したインスタンス.</returns>
        public async UniTask<T> GetAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            if (pool.TryPop(out var v))
            {
                return v;
            }

            var results = await Object.InstantiateAsync(original, root).ToUniTask(cancellationToken: cancellationToken);
            return results[0];
        }

        /// <summary>
        /// プールから非同期でインスタンスを取得する.
        /// </summary>
        /// <param name="count">取得するインスタンスの数.</param>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <returns>取得したインスタンス.</returns>
        public async UniTask<T[]> GetAsync(int count, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            var results = new T[count];
            if (!TryGetPool(pool, results, out var alreadyCount))
            {
                var remains
                    = await Object.InstantiateAsync(original, count - alreadyCount, root).ToUniTask(cancellationToken: cancellationToken);
                remains.AsSpan().CopyTo(results.AsSpan(alreadyCount));
            }
            return results;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static bool TryGetPool(Stack<T> pool, in Span<T> results, out int count)
            {
                for (int i = 0; i < results.Length; i++)
                {
                    if (pool.TryPop(out var v))
                    {
                        results[i] = v;
                        continue;
                    }

                    count = i;
                    return false;
                }
                count = results.Length;
                return true;
            }
        }

        /// <summary>
        /// プールから非同期でインスタンスを取得する(usingステートメントで使用するためのメソッド).
        /// </summary>
        /// <param name="cancellationToken">キャンセルトークン.</param>
        /// <returns>取得したインスタンスと解放用の<see cref="System.IDisposable"/>.</returns>
        public async UniTask<PooledObject<T>> GetScopeAsync(CancellationToken cancellationToken = default)
        {
            var x = await GetAsync(cancellationToken);
            return CreatePooledObject(x, this);
        }
    }
}