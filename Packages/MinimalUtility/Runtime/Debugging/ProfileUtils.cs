using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace MinimalUtility.Debugging
{
    /// <summary>
    /// フレームデータを提供する.
    /// </summary>
    [Unity.Profiling.IgnoredByDeepProfiler]
    public static class ProfileUtils
    {
        /// <summary>
        /// 総メモリ使用量表示の単位指定列挙体.
        /// </summary>
        public enum MemoryUnit : int
        {
            [InspectorName("バイト")]
            B = 0,

            [InspectorName("キロバイト")]
            KB = 1,

            [InspectorName("メガバイト")]
            MB = 2,

            [InspectorName("ギガバイト")]
            GB = 3
        }

        /// <summary>
        /// 最新のフレームタイミング情報を取得します.
        /// </summary>
        /// <returns>最新のフレームタイミング情報.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FrameTiming GetLatestFrameTiming()
        {
            var timings = ArrayPool<FrameTiming>.Shared.Rent(1);
            FrameTimingManager.CaptureFrameTimings();
            var result = FrameTimingManager.GetLatestTimings(1, timings) == 0 ? default : timings[0];
            ArrayPool<FrameTiming>.Shared.Return(timings);
            return result;
        }

        /// <summary>
        /// Unityによって予約されたメモリの合計を単位指定して取得します.
        /// </summary>
        /// <param name="unit">メモリ単位.</param>
        /// <returns>指定した単位でのメモリ合計.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetTotalMemory(MemoryUnit unit)
            => UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int)unit);

        /// <summary>
        /// プロファイル情報ラベルを取得します.
        /// </summary>
        /// <returns>プロファイル情報ラベル.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Label GetProfileInfoLabel()
        {
            const string title = "<b>Performance</b>";
            var label = new Label(title)
            {
                enableRichText = true
            };
            label.schedule.Execute(() =>
            {
                var latest = GetLatestFrameTiming();
                var sb = new DefaultInterpolatedStringHandler(0, 0);
                var newlineSpan = Environment.NewLine.AsSpan();
                sb.AppendLiteral(title);
                sb.AppendFormatted(newlineSpan);
                sb.AppendLiteral("CPU: ");
                sb.AppendFormatted(1000 / latest.cpuFrameTime, "F0");
                sb.AppendLiteral("fps (");
                sb.AppendFormatted(latest.cpuFrameTime, "F1");
                sb.AppendLiteral("ms)");
                sb.AppendFormatted(newlineSpan);
                sb.AppendLiteral("Memory: ");
                sb.AppendFormatted(GetTotalMemory(MemoryUnit.GB), "F");
                sb.AppendFormatted("GB");
                label.text = sb.ToString();
            }).Every(500);
            return label;
        }
    }
}