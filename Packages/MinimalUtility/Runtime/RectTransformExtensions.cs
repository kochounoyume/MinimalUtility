using System.Runtime.CompilerServices;
using UnityEngine;

namespace MinimalUtility
{
    public static class RectTransformExtensions
    {
        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全なサイズ設定
        /// </summary>
        /// <param name="target"></param>
        /// <param name="sizeX">横の長さ</param>
        /// <param name="sizeY">縦の長さ</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeSize(this RectTransform target, in float sizeX, in float sizeY)
        {
            const RectTransform.Axis horizontal = RectTransform.Axis.Horizontal;
            target.SetSizeWithCurrentAnchors(horizontal, sizeX);
            const RectTransform.Axis vertical = RectTransform.Axis.Vertical;
            target.SetSizeWithCurrentAnchors(vertical, sizeY);
        }
    }
}