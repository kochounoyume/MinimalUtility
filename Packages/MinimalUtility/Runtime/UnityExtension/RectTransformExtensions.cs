﻿using System.Runtime.CompilerServices;
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
            target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全なサイズ設定.
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="size">任意の縦横サイズ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeSize(this RectTransform target, Vector2 size)
        {
            target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全な横幅設定.
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="width">横の長さ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeWidth(this RectTransform target, in float width)
        {
            target.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全な縦幅設定.
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="height">縦の長さ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeHeight(this RectTransform target, in float height)
        {
            target.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全なサイズ取得.
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <returns>対象のサイズ.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetSizeDelta(this RectTransform target) => target.rect.size;

        /// <summary>
        /// <see cref="RectTransform"/>を全面的に伸ばす(stretch * stretchにする).
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="left">左端のオフセット.</param>
        /// <param name="right">右端のオフセット.</param>
        /// <param name="top">上端のオフセット.</param>
        /// <param name="bottom">下端のオフセット.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFullStretch(
            this RectTransform target,
            in float left = default,
            in float right = default,
            in float top = default,
            in float bottom = default)
        {
            target.anchorMin = Vector2.zero;
            target.anchorMax = Vector2.one;
            target.offsetMin = new Vector2(left, bottom);
            target.offsetMax = new Vector2(-right, -top);
        }
    }
}