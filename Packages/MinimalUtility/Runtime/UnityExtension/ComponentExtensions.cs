using System.Collections.Generic;
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
        /// 子オブジェクトの<typeparamref name="T"/>コンポーネントを取得する.
        /// <remarks>
        /// <see cref="Component.GetComponentInChildren{T}()"/>と異なり、孫オブジェクト以降は検索しない.
        /// </remarks>
        /// </summary>
        /// <param name="self">対象の<see cref="Component"/>.</param>
        /// <typeparam name="T">取得したいコンポーネントの型.</typeparam>
        /// <returns>取得したコンポーネントインスタンス.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentInOnlyChild<T>(this Component self) where T : Component
        {
            Transform transform = self.transform;
            foreach (Transform child in transform)
            {
                if (child.gameObject.TryGetComponent(out T component))
                {
                    return component;
                }
            }
            return null;
        }

        /// <summary>
        /// 子オブジェクトの<typeparamref name="T"/>コンポーネントを取得する.
        /// </summary>
        /// <param name="self">対象の<see cref="Component"/>.</param>
        /// <param name="component">取得したコンポーネントインスタンス.</param>
        /// <typeparam name="T">取得したいコンポーネントの型.</typeparam>
        /// <returns>コンポーネントが取得できた場合はtrue.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInOnlyChild<T>(this Component self, out T component) where T : Component
        {
            Transform transform = self.transform;
            foreach (Transform child in transform)
            {
                if (child.gameObject.TryGetComponent(out component))
                {
                    return true;
                }
            }
            component = null;
            return false;
        }

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
        public static IReadOnlyList<T> GetComponentsInOnlyChildren<T>(this Component self) where T : Component
        {
            Transform transform = self.transform;
            List<T> list = new List<T>(transform.childCount);
            foreach (Transform child in transform)
            {
                if (child.gameObject.TryGetComponent(out T component))
                {
                    list.Add(component);
                }
            }
            list.TrimExcess();
            return list;
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
            if (self.TryGetComponent(out T component))
            {
                return component;
            }
            return null;
        }
    }
}