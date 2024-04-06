using System.Runtime.CompilerServices;
using Mathf = UnityEngine.Mathf;

namespace MinimalUtility
{
    /// <summary>
    /// 最小限の数学関数.
    /// </summary>
    public static class Math
    {
        /// <summary>
        /// 円周率（読み取り専用）.
        /// </summary>
        public const float PI = 3.1415927f;

        /// <summary>
        /// 無限大（読み取り専用）.
        /// </summary>
        public const float Infinity = float.PositiveInfinity;

        /// <summary>
        /// 負の無限大（読み取り専用）.
        /// </summary>
        public const float NegativeInfinity = float.NegativeInfinity;

        /// <summary>
        /// 度からラジアンに変換する定数（読み取り専用）.
        /// </summary>
        public const float Deg2Rad = 0.017453292f;

        /// <summary>
        /// ラジアンから度に変換する定数（読み取り専用）.
        /// </summary>
        public const float Rad2Deg = 57.29578f;

        /// <summary>
        /// ごくわずかな浮動小数点の値（読み取り専用）.
        /// </summary>
        public static float Epsilon
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Mathf.Epsilon;
        }

        /// <summary>
        /// /f/の絶対値を返します.
        /// </summary>
        /// <param name="f">任意の浮動小数点値.</param>
        /// <returns>求めた絶対値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(in float f) => System.Math.Abs(f);

        /// <summary>
        /// /value/の絶対値を返します.
        /// </summary>
        /// <param name="value">任意の整数値.</param>
        /// <returns>求めた絶対値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(in int value) => System.Math.Abs(value);

        /// <summary>
        /// 2つの浮動小数点値を比較し、近似している場合は true を返します.
        /// </summary>
        /// <param name="a">比較する一つ目の浮動小数点値.</param>
        /// <param name="b">比較する二つ目の浮動小数点値.</param>
        /// <returns>近似している場合は true.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Approximately(in float a, in float b)
        {
            return (double)System.Math.Abs(b - a) < (double)Max(1E-06f * Max(System.Math.Abs(a), System.Math.Abs(b)), Mathf.Epsilon * 8f);
        }

        /// <summary>
        /// 2つ以上の値から最小値を返します.
        /// </summary>
        /// <param name="a">比較する一つ目の浮動小数点値.</param>
        /// <param name="b">比較する二つ目の浮動小数点値.</param>
        /// <returns>最小値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(in float a, in float b) => (double)a < (double)b ? a : b;

        /// <summary>
        /// 2つ以上の値から最小値を返します.
        /// </summary>
        /// <param name="a">比較する一つ目の整数値.</param>
        /// <param name="b">比較する二つ目の整数値.</param>
        /// <returns>最小値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Min(in int a, in int b) => a < b ? a : b;

        /// <summary>
        /// 2つ以上の値から最大値を返します.
        /// </summary>
        /// <param name="a">比較する一つ目の浮動小数点値.</param>
        /// <param name="b">比較する二つ目の浮動小数点値.</param>
        /// <returns>最大値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(in float a, in float b) => (double)a > (double)b ? a : b;

        /// <summary>
        /// 2つ以上の値から最大値を返します.
        /// </summary>
        /// <param name="a">比較する一つ目の整数値.</param>
        /// <param name="b">比較する二つ目の整数値.</param>
        /// <returns>最大値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Max(in int a, in int b) => a > b ? a : b;
    }
}