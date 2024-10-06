using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="Component"/>の拡張メソッド.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// 子オブジェクトの<typeparamref name="T"/>コンポーネントを全て取得する.
        /// <remarks>
        /// <see cref="Component.GetComponentsInChildren{T}()"/>と異なり、孫オブジェクト以降は検索しない.
        /// </remarks>
        /// </summary>
        /// <param name="self">対象の<see cref="Component"/>.</param>
        /// <typeparam name="T">取得したいコンポーネントの型.</typeparam>
        /// <returns>取得したコンポーネントインスタンスのコレクション.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyMemory<T> GetComponentsInOnlyChildren<T>(this Component self) where T : Component
        {
            Transform transform = self.transform;
            int limit = transform.childCount;
            if (limit == 0) return ReadOnlyMemory<T>.Empty;

            T[] array = new T[limit];
            Span<T> span = array.AsSpan();
            int count = 0;
            for (int i = 0; i < span.Length; i++)
            {
                if (transform.GetChild(i).gameObject.TryGetComponent(out T component))
                {
                    span[count++] = component;
                }
            }
            return new ReadOnlyMemory<T>(array, 0, count);
        }

        /// <summary>
        /// 安全な<see cref="Component.GetComponent{T}"/>.
        /// </summary>
        /// <param name="self">対象の<see cref="Component"/>.</param>
        /// <typeparam name="T">取得したいコンポーネントの型.</typeparam>
        /// <returns>取得したコンポーネントインスタンス.なお取得不可の場合はSystemのnullを返す.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SafeGetComponent<T>(this Component self) where T : Component
        {
            self.gameObject.TryGetComponent(out T component);
            return component;
        }
    }
}