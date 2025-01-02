#if ENABLE_MINIMAL_DEBUGGING && ENABLE_UITOOLKIT
#nullable enable

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
            var label = new Label("<b>Performance</b>")
            {
                enableRichText = true
            };
            label.schedule.Execute(() =>
            {
                var latest = GetLatestFrameTiming();
                label.text = $@"<b>Performance</b>
CPU: {1000 / latest.cpuFrameTime:F0}fps ({latest.cpuFrameTime:F1}ms)
Memory: {GetTotalMemory(MemoryUnit.GB):F}GB";
            }).Every(500);
            return label;
        }
    }
}
#endif