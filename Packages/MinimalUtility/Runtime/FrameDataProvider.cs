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
        public class EmptyMonoBehaviour : MonoBehaviour
        {
        }

        private readonly Lazy<MonoBehaviour> coroutineRunner;

        /// <summary>
        /// コルーチンを実行するための<see cref="MonoBehaviour"/>.
        /// </summary>
        public MonoBehaviour CoroutineRunner => coroutineRunner.Value;

        private readonly FrameTiming[] frameTiming = new FrameTiming[1];

        /// <summary>
        /// フレームタイミング情報.
        /// </summary>
        public ref readonly FrameTiming LatestFrameData
        {
            get
            {
                if (coroutineRunner.IsValueCreated)
                {
                    coroutineRunner.Value.StartCoroutine(UpdateFrameTiming());
                }
                return ref frameTiming[0];
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameDataProvider"/> class.
        /// </summary>
        public FrameDataProvider()
        {
            coroutineRunner = new Lazy<MonoBehaviour>(CreateCoroutineRunner<MonoBehaviour>());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameDataProvider"/> class.
        /// </summary>
        /// <param name="coroutineRunnerFactory">コルーチンを実行するための<see cref="MonoBehaviour"/>を生成する関数.</param>
        internal FrameDataProvider(Func<MonoBehaviour> coroutineRunnerFactory)
        {
            coroutineRunner = new Lazy<MonoBehaviour>(coroutineRunnerFactory);
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
        /// コルーチンを実行するための<see cref="MonoBehaviour"/>を取得する汎用実装.
        /// </summary>
        /// <typeparam name="T"><see cref="MonoBehaviour"/>を継承したクラス.</typeparam>
        /// <returns>コルーチンを実行するための<see cref="MonoBehaviour"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T CreateCoroutineRunner<T>() where T : MonoBehaviour
        {
            var go = new GameObject("Debug Profiler", typeof(T));
            UnityEngine.Object.DontDestroyOnLoad(go);
            return go.GetComponent<T>();
        }

        private IEnumerator UpdateFrameTiming()
        {
            while (coroutineRunner.Value != null)
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