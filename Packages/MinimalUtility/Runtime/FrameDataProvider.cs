using System;
using System.Collections;
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
        /// 空のMonoBehaviour.
        /// </summary>
        [DisallowMultipleComponent]
        protected class EmptyMonoBehaviour : MonoBehaviour
        {
        }

        /// <summary>
        /// ランナーゲームオブジェクトの名前.
        /// </summary>
        protected const string RunnerName = "Debug Profiler";

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
        /// コルーチンを実行するための<see cref="MonoBehaviour"/>を取得します.
        /// </summary>
        /// <returns>コルーチンを実行するための<see cref="MonoBehaviour"/>.</returns>
        protected virtual EmptyMonoBehaviour GetCoroutineRunner()
        {
            var go = new GameObject(RunnerName, typeof(EmptyMonoBehaviour));
            UnityEngine.Object.DontDestroyOnLoad(go);
            return go.GetComponent<EmptyMonoBehaviour>();
        }

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