using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MinimalUtility
{
    /// <summary>
    /// フレームデータを提供するクラス.
    /// </summary>
    [Unity.Profiling.IgnoredByDeepProfiler]
    public class FrameDataProvider
    {
        /// <summary>
        /// 総メモリ使用量表示の単位指定列挙体.
        /// </summary>
        [GenerateStringConverter(true)]
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
        /// 空のMonoBehaviour.
        /// </summary>
        [DisallowMultipleComponent]
        protected class EmptyMonoBehaviour : MonoBehaviour
        {
        }

        /// <summary>
        /// コルーチンを実行するための<see cref="MonoBehaviour"/>の遅延生成.
        /// </summary>
        protected readonly Lazy<EmptyMonoBehaviour> CoroutineRunner;

        private readonly FrameTiming[] frameTiming = new FrameTiming[1];

        /// <summary>
        /// フレームタイミング情報.
        /// </summary>
        public ref readonly FrameTiming LatestFrameData
        {
            get
            {
                if (CoroutineRunner.IsValueCreated)
                {
                    CoroutineRunner.Value.StartCoroutine(UpdateFrameTiming());
                }
                return ref frameTiming[0];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameDataProvider"/> class.
        /// </summary>
        public FrameDataProvider()
        {
            CoroutineRunner = new Lazy<EmptyMonoBehaviour>(GetCoroutineRunner);
        }

        /// <summary>
        /// Unityによって予約されたメモリの合計を単位指定して取得します.
        /// </summary>
        /// <param name="unit">メモリ単位.</param>
        /// <returns>指定した単位でのメモリ合計.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetTotalMemory(in MemoryUnit unit)
        {
            return UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int)unit);
        }

        /// <summary>
        /// コルーチンを実行するための<see cref="MonoBehaviour"/>を取得する汎用実装.
        /// </summary>
        /// <typeparam name="T"><see cref="EmptyMonoBehaviour"/>を継承したクラス.</typeparam>
        /// <returns>コルーチンを実行するための<see cref="MonoBehaviour"/>..</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static EmptyMonoBehaviour CreateCoroutineRunner<T>() where T : EmptyMonoBehaviour
        {
            var go = new GameObject("Debug Profiler", typeof(T));
            UnityEngine.Object.DontDestroyOnLoad(go);
            return go.GetComponent<T>();
        }

        /// <summary>
        /// コルーチンを実行するための<see cref="MonoBehaviour"/>を取得します.
        /// </summary>
        /// <returns>コルーチンを実行するための<see cref="MonoBehaviour"/>.</returns>
        protected virtual EmptyMonoBehaviour GetCoroutineRunner() => CreateCoroutineRunner<EmptyMonoBehaviour>();

        private IEnumerator UpdateFrameTiming()
        {
            while (CoroutineRunner.Value != null)
            {
                FrameTimingManager.CaptureFrameTimings();
                if (FrameTimingManager.GetLatestTimings(1, frameTiming) == 0)
                {
                    frameTiming[0] = default;
                }
                yield return null;
            }
        }
    }
}
