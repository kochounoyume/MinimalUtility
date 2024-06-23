using System.Runtime.CompilerServices;
using UnityEngine;

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="RectTransform"/>の拡張メソッド.
    /// </summary>
    public static class RectTransformExtensions
    {
        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全なサイズ設定.
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="width">横の長さ.</param>
        /// <param name="height">縦の長さ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeSize(this RectTransform target, in float width, in float height)
        {
            const RectTransform.Axis horizontal = RectTransform.Axis.Horizontal;
            target.SetSizeWithCurrentAnchors(horizontal, width);
            const RectTransform.Axis vertical = RectTransform.Axis.Vertical;
            target.SetSizeWithCurrentAnchors(vertical, height);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全な横幅設定.
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="width">横の長さ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeSizeWidth(this RectTransform target, in float width)
        {
            const RectTransform.Axis horizontal = RectTransform.Axis.Horizontal;
            target.SetSizeWithCurrentAnchors(horizontal, width);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全な縦幅設定.
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="height">縦の長さ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeSizeHeight(this RectTransform target, in float height)
        {
            const RectTransform.Axis vertical = RectTransform.Axis.Vertical;
            target.SetSizeWithCurrentAnchors(vertical, height);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全なサイズ取得.
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <returns>対象のサイズ.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetSizeDelta(this RectTransform target) => target.rect.size;
    }
}