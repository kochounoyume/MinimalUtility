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

        /// <summary>
        /// サイン（正弦）を返します.
        /// </summary>
        /// <param name="f">ラジアン単位の角度.</param>
        /// <returns>戻り値は-1から+1の間.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(in float f) => (float)System.Math.Sin((double)f);

        /// <summary>
        /// コサイン（余弦）を返します.
        /// </summary>
        /// <param name="f">ラジアン単位の角度.</param>
        /// <returns>戻り値は-1から+1の間.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(in float f) => (float)System.Math.Cos((double)f);

        /// <summary>
        /// タンジェント（正接）を返します.
        /// </summary>
        /// <param name="f">ラジアン単位の角度.</param>
        /// <returns>戻り値は無限大から+無限大の間.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(in float f) => (float)System.Math.Tan((double)f);

        /// <summary>
        /// fの平方根を返します.
        /// </summary>
        /// <param name="f">非負の浮動小数点値.</param>
        /// <returns>戻り値は0以上の値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(in float f) => (float)System.Math.Sqrt(f);

        /// <summary>
        /// fをp乗した値を返します.
        /// </summary>
        /// <param name="f">任意の浮動小数点値.</param>
        /// <param name="p">fを掛ける回数.</param>
        /// <returns>戻り値は0以上の値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(in float f, in float p) => (float)System.Math.Pow((double)f, (double)p);

        /// <summary>
        /// 切り上げて整数値を返します.
        /// ex.)2.3f -> 3.0f .
        /// </summary>
        /// <param name="f">任意の浮動小数点値.</param>
        /// <returns>切り上げた値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Ceil(in float f) => (float)System.Math.Ceiling((double)f);

        /// <summary>
        /// 切り上げて整数値を返します.
        /// ex.)2.3f -> 3 .
        /// </summary>
        /// <param name="f">任意の浮動小数点値.</param>
        /// <returns>切り上げた値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CeilToInt(in float f) => (int)System.Math.Ceiling((double)f);

        /// <summary>
        /// 切り捨てて整数値を返します.
        /// ex.)2.3f -> 2.0f .
        /// </summary>
        /// <param name="f">任意の浮動小数点値.</param>
        /// <returns>切り捨てた値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Floor(in float f) => (float)System.Math.Floor((double)f);

        /// <summary>
        /// 切り捨てて整数値を返します.
        /// ex.)2.3f -> 2 .
        /// </summary>
        /// <param name="f">任意の浮動小数点値.</param>
        /// <returns>切り捨てた値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FloorToInt(in float f) => (int)System.Math.Floor((double)f);

        /// <summary>
        /// 四捨五入して整数値を返します.
        /// ex.)2.3f -> 2.0f, 2.5f -> 3.0f .
        /// </summary>
        /// <param name="f">任意の浮動小数点値.</param>
        /// <returns>四捨五入した値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Round(in float f) => (float)System.Math.Round(f);

        /// <summary>
        /// 四捨五入して整数値を返します.
        /// ex.)2.3f -> 2, 2.5f -> 3 .
        /// </summary>
        /// <param name="f">任意の浮動小数点値.</param>
        /// <returns>四捨五入した値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RoundToInt(in float f) => (int)System.Math.Round(f);

        /// <summary>
        /// valueを最小値と最大値の間に制限します.
        /// ex.)Clamp(2.3f, 0.2f, 1.2f) -> 1.2f .
        /// </summary>
        /// <param name="value">制限する値.</param>
        /// <param name="min">最小値.</param>
        /// <param name="max">最大値.</param>
        /// <returns>制限された値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(in float value, in float min, in float max) => System.Math.Clamp(value, min, max);

        /// <summary>
        /// valueを最小値と最大値の間に制限します.
        /// ex.)Clamp(2, -1, 1) -> 1 .
        /// </summary>
        /// <param name="value">制限する値.</param>
        /// <param name="min">最小値.</param>
        /// <param name="max">最大値.</param>
        /// <returns>制限された値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(in int value, in int min, in int max) => System.Math.Clamp(value, min, max);

        /// <summary>
        /// valueを0.0fから1.0fの間に制限します.
        /// ex.)Clamp01(2.3f) -> 1.0f .
        /// </summary>
        /// <param name="value">制限する値.</param>
        /// <returns>制限された値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp01(in float value) => System.Math.Clamp(value, default, 1.0f);

        /// <summary>
        /// aとbの間を線形補間します.
        /// ex.)Lerp(2.5f, 3.5f, 0.5f) -> 3.0f .
        /// </summary>
        /// <param name="a">開始値.</param>
        /// <param name="b">終了値.</param>
        /// <param name="t">補間値.</param>
        /// <returns>補間された値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(in float a, in float b, in float t) => a + ((b - a) * System.Math.Clamp(t, default, 1.0f));

        /// <summary>
        /// aとbの間を線形補間します.
        /// ex.)LerpUnClamped(2.5f, 3.5f, 0.5f) -> 3.0f .
        /// </summary>
        /// <param name="a">開始値.</param>
        /// <param name="b">終了値.</param>
        /// <param name="t">補間値.</param>
        /// <returns>補間された値.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float LerpUnClamped(in float a, in float b, in float t) => a + ((b - a) * t);
    }
}