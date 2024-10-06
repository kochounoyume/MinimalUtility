using System;
using UnityEngine;

namespace MinimalUtility
{
    /// <summary>
    /// usingステートメントスコープを抜けた際にインスタンスをプールに返却する構造体.
    /// </summary>
    /// <typeparam name="T">プールする<see cref="MonoBehaviour"/>の型.</typeparam>
    public readonly struct PooledObject<T> : IDisposable where T : MonoBehaviour
    {
        /// <summary>
        /// 生成済みのインスタンス.
        /// </summary>
        public readonly T Instance;
        private readonly GameObjectPool<T> pool;

        /// <summary>
        /// Initializes a new instance of the <see cref="PooledObject{T}"/> struct.
        /// </summary>
        /// <param name="instance">生成済みのインスタンス.</param>
        /// <param name="pool">プール.</param>
        internal PooledObject(T instance, GameObjectPool<T> pool)
        {
            Instance = instance;
            this.pool = pool;
        }

        /// <inheritdoc/>
        void IDisposable.Dispose() => pool.Release(Instance);
    }
}