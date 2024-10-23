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
        public static Vector2 GetSize(this RectTransform target) => target.rect.size;

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

        /// <summary>
        /// ワールドスペースで計算された長方形の角を取得する.
        /// <remarks>
        /// <see cref="RectTransform.GetWorldCorners(Vector3[])"/>に同じ.
        /// </remarks>
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">取得した角の座標を格納する<see cref="Span{T}"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetWorldCorners(this RectTransform target, in Span<Vector3> fourCornersSpan)
        {
            if (fourCornersSpan.IsEmpty || fourCornersSpan.Length < 4)
            {
                Debug.LogError("Calling GetWorldCorners with an array that is null or has less than 4 elements.");
            }
            else
            {
                target.GetCalculateLocalCorners(fourCornersSpan);
                Matrix4x4 localToWorldMatrix = target.localToWorldMatrix;
                for (int i = 0; i < fourCornersSpan.Length; i++)
                {
                    fourCornersSpan[i] = localToWorldMatrix.MultiplyPoint(fourCornersSpan[i]);
                }
            }
        }

        /// <summary>
        /// <see cref="RectTransform"/>のローカル空間で計算された長方形の角を取得する.
        /// <remarks>
        /// <see cref="RectTransform.GetLocalCorners(Vector3[])"/>に同じ.
        /// </remarks>
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">取得した角の座標を格納する<see cref="Span{T}"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetLocalCorners(this RectTransform target, in Span<Vector3> fourCornersSpan)
        {
            if (fourCornersSpan.IsEmpty || fourCornersSpan.Length < 4)
            {
                Debug.LogError("Calling GetLocalCorners with an array that is null or has less than 4 elements.");
            }
            else
            {
                target.GetCalculateLocalCorners(fourCornersSpan);
            }
        }

        /// <summary>
        /// <see cref="RectTransform"/>のローカル空間で計算された長方形の角を取得する.
        /// <remarks>
        /// <see cref="RectTransform.GetLocalCorners(Vector3[])"/>に同じ.
        /// </remarks>
        /// </summary>
        /// <param name="target">対象の<see cref="RectTransform"/>.</param>
        /// <param name="fourCornersSpan">取得した角の座標を格納する<see cref="Span{T}"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void GetCalculateLocalCorners(this RectTransform target, in Span<Vector3> fourCornersSpan)
        {
            Rect rect = target.rect;
            (float x, float y, float xMax, float yMax) = (rect.x, rect.y, rect.xMax, rect.yMax);
            fourCornersSpan[0] = new Vector3(x, y);
            fourCornersSpan[1] = new Vector3(x, yMax);
            fourCornersSpan[2] = new Vector3(xMax, yMax);
            fourCornersSpan[3] = new Vector3(xMax, y);
        }
    }
}