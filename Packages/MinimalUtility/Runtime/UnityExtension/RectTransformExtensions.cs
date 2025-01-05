#nullable enable

using System;
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
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <param name="width">横の長さ.</param>
        /// <param name="height">縦の長さ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeSize(this RectTransform rectTransform, in float width, in float height)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全なサイズ設定.
        /// </summary>
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <param name="size">任意の縦横サイズ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeSize(this RectTransform rectTransform, in Vector2 size)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全な横幅設定.
        /// </summary>
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <param name="width">横の長さ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeWidth(this RectTransform rectTransform, in float width)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全な縦幅設定.
        /// </summary>
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <param name="height">縦の長さ.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetSafeHeight(this RectTransform rectTransform, in float height)
        {
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        /// <summary>
        /// <see cref="RectTransform.sizeDelta"/>よりも安全なサイズ取得.
        /// </summary>
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <returns>対象のサイズ.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 GetSize(this RectTransform rectTransform) => rectTransform.rect.size;

        /// <summary>
        /// <see cref="RectTransform"/>を全面的に伸ばす(stretch * stretchにする).
        /// </summary>
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <param name="left">左端のオフセット.</param>
        /// <param name="right">右端のオフセット.</param>
        /// <param name="top">上端のオフセット.</param>
        /// <param name="bottom">下端のオフセット.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetFullStretch(this RectTransform rectTransform, in float left = default, in float right = default, in float top = default, in float bottom = default)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = new Vector2(left, bottom);
            rectTransform.offsetMax = new Vector2(-right, -top);
        }

        /// <summary>
        /// ワールドスペースで計算された長方形の角を取得する.
        /// </summary>
        /// <remarks>
        /// <see cref="RectTransform.GetWorldCorners(Vector3[])"/>に同じ.
        /// </remarks>
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">取得した角の座標を格納する<see cref="Span{T}"/>.</param>
        /// <returns>取得した角の座標を格納した<see cref="ReadOnlySpan{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<Vector3> GetWorldCorners(this RectTransform rectTransform, in Span<Vector3> fourCornersSpan)
        {
            if (fourCornersSpan.IsEmpty || fourCornersSpan.Length < 4)
            {
                Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
            }
            else
            {
                rectTransform.GetCalculateLocalCorners(fourCornersSpan);
                var localToWorldMatrix = rectTransform.localToWorldMatrix;
                for (var i = 0; i < fourCornersSpan.Length; i++)
                {
                    fourCornersSpan[i] = localToWorldMatrix.MultiplyPoint(fourCornersSpan[i]);
                }
            }
            return fourCornersSpan;
        }

        /// <summary>
        /// <see cref="RectTransform"/>のローカル空間で計算された長方形の角を取得する.
        /// </summary>
        /// <remarks>
        /// <see cref="RectTransform.GetLocalCorners(Vector3[])"/>に同じ.
        /// </remarks>
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">取得した角の座標を格納する<see cref="Span{T}"/>.</param>
        /// <returns>取得した角の座標を格納した<see cref="ReadOnlySpan{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<Vector3> GetLocalCorners(this RectTransform rectTransform, in Span<Vector3> fourCornersSpan)
        {
            if (fourCornersSpan.IsEmpty || fourCornersSpan.Length < 4)
            {
                Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
            }
            else
            {
                rectTransform.GetCalculateLocalCorners(fourCornersSpan);
            }
            return fourCornersSpan;
        }

        /// <summary>
        /// <see cref="RectTransform"/>のローカル空間で計算された長方形の角を取得する.
        /// </summary>
        /// <remarks>
        /// <see cref="RectTransform.GetLocalCorners(Vector3[])"/>に同じ.
        /// </remarks>
        /// <param name="rectTransform">対象の<see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">取得した角の座標を格納する<see cref="Span{T}"/>.</param>
        /// <returns>取得した角の座標を格納した<see cref="ReadOnlySpan{T}"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static ReadOnlySpan<Vector3> GetCalculateLocalCorners(this RectTransform rectTransform, in Span<Vector3> fourCornersSpan)
        {
            var rect = rectTransform.rect;
            var (x, y, xMax, yMax) = (rect.x, rect.y, rect.xMax, rect.yMax);
            stackalloc Vector3[] { new(x, y), new(x, yMax), new(xMax, yMax), new(xMax, y) }.TryCopyTo(fourCornersSpan);
            return fourCornersSpan;
        }
    }
}