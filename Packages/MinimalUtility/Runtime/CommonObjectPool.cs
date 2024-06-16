using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace MinimalUtility
{
    /// <summary>
    /// ObjectPoolを提供するインターフェイス.
    /// </summary>
    /// <typeparam name="T">プールするオブジェクトの型.</typeparam>
#pragma warning disable SA1649
    public interface IObjectPool<T> : UnityEngine.Pool.IObjectPool<T> where T : MonoBehaviour
#pragma warning restore SA1649
    {
        /// <summary>
        /// 取得済みの全てのオブジェクトを解放する.
        /// </summary>
        void ReleaseAll();
    }

    /// <summary>
    /// 汎用オブジェクトプール実装.
    /// </summary>
    /// <typeparam name="T">プールするオブジェクトの型.</typeparam>
    public class CommonObjectPool<T> : IDisposable, IObjectPool<T> where T : MonoBehaviour
    {
        private readonly T prefab;

        private readonly Transform root;

        private readonly ObjectPool<T> pool;

        private readonly ICollection<T> activeInstances;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonObjectPool{T}"/> class.
        /// </summary>
        /// <param name="prefab">プール対象のプレハブ.</param>
        /// <param name="root">プールから取得されたインスタンスに指定する親オブジェクト.</param>
        /// <param name="setting">プールの初期容量と最大サイズ.</param>
        public CommonObjectPool(T prefab, Transform root, PoolSizeSetting setting)
        {
            this.prefab = prefab;
            this.root = root;
            this.pool = new ObjectPool<T>(
                OnCreate,
                static instance => instance.gameObject.SetActive(true),
                static instance => instance.gameObject.SetActive(false),
                static instance =>
                {
                    if (instance == null) return;
                    Object.Destroy(instance.gameObject);
                },
                false,
                setting.DefaultCapacity,
                setting.MaxSize);
            this.activeInstances = new List<T>(setting.DefaultCapacity);
        }

        /// <inheritdoc/>
        public int CountInactive => pool.CountInactive;

        /// <inheritdoc/>
        public T Get()
        {
            T v = pool.Get();
            activeInstances.Add(v);
            return v;
        }

        /// <inheritdoc/>
        public PooledObject<T> Get(out T v)
        {
            PooledObject<T> result = pool.Get(out v);
            activeInstances.Add(v);
            return result;
        }

        /// <inheritdoc/>
        public void Release(T element)
        {
            if (activeInstances.Remove(element))
            {
                pool.Release(element);
            }
        }

        /// <inheritdoc/>
        public void Clear() => pool.Clear();

        /// <inheritdoc/>
        public void ReleaseAll()
        {
            foreach (T activeInstance in activeInstances)
            {
                pool.Release(activeInstance);
            }
            activeInstances.Clear();
        }

        /// <inheritdoc/>
        void IDisposable.Dispose()
        {
            pool.Dispose();
            activeInstances.Clear();
        }

        private T OnCreate() => Object.Instantiate(prefab, root);

        /// <summary>
        /// プールの初期容量と最大サイズを指定する構造体.
        /// </summary>
        public readonly ref struct PoolSizeSetting
        {
            /// <summary>
            /// スタックが作成されるときの初期容量.
            /// </summary>
            internal readonly int DefaultCapacity;

            /// <summary>
            /// プールの最大サイズ.
            /// </summary>
            internal readonly int MaxSize;

            /// <summary>
            /// Initializes a new instance of the <see cref="PoolSizeSetting"/> struct.
            /// </summary>
            /// <param name="defaultCapacity">スタックが作成されるときの初期容量.</param>
            /// <param name="maxSize">プールの最大サイズ.</param>
            public PoolSizeSetting(in int defaultCapacity, in int maxSize)
            {
                this.DefaultCapacity = defaultCapacity;
                this.MaxSize = maxSize;
            }
        }
    }
}