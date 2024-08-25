using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using R3;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using Screen = UnityEngine.Device.Screen;
#else
using Screen = UnityEngine.Screen;
#endif

namespace MinimalUtility.MultiLibraries
{
    /// <summary>
    /// FPSなどのプロファイル情報を画面に表示するためのクラス.
    /// <remarks>
    /// <see cref="Time.realtimeSinceStartup"/> の公式リファレンスサンプルコードを参照
    /// </remarks>
    /// </summary>
    public sealed partial class DebugProfiler
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
        /// プロファイル情報を更新する間隔.
        /// </summary>
        private const float IntervalSecs = 0.5f;

        /// <summary>
        /// 表示する総メモリ使用量の単位.
        /// </summary>
        private readonly MemoryUnit memoryUnit;

        private readonly MemoryUnitStringConverter memoryUnitStringConverter = new ();

        private readonly FrameTiming[] frameTiming = new FrameTiming[300];

        /// <summary>
        /// フレームタイミング情報.
        /// </summary>
        public FrameTiming FrameTiming => frameTiming[0];

        /// <summary>
        /// GUI表示の可視状態.
        /// </summary>
        public bool IsGUIVisible { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugProfiler"/> class.
        /// </summary>
        /// <param name="memoryUnit">総メモリ使用量表示の単位.</param>
        public DebugProfiler(MemoryUnit memoryUnit)
        {
            this.memoryUnit = memoryUnit;
        }

        /// <summary>
        /// プロファイル情報の表示を開始する.
        /// </summary>
        /// <param name="cancellation">キャンセルトークン.</param>
        /// <returns>UniTask.</returns>
        public UniTask StartAsync(CancellationToken cancellation)
        {
            TimeSpan interval = TimeSpan.FromSeconds(IntervalSecs);

            Observable
                .EveryUpdate(cancellation)
                .Subscribe(frameTiming, static (_, frameTiming) =>
                {
                    FrameTimingManager.CaptureFrameTimings();
                    FrameTimingManager.GetLatestTimings((uint)frameTiming.Length, frameTiming);
                });

            if (IsGUIVisible)
            {
                GameObject instanceObj = new GameObject("DebugProfiler", typeof(AsyncGUITrigger));
                Object.DontDestroyOnLoad(instanceObj);
                AsyncGUITrigger trigger = instanceObj.GetComponent<AsyncGUITrigger>();

                string unitStr = memoryUnitStringConverter.Convert(memoryUnit);
                // GUIStyleは生成タイミングがOnGUIの中でないとエラーになるため、Lazyで遅延生成
                Lazy<GUIStyle> styleBox = new (static () => new GUIStyle(GUI.skin.box)
                {
                    fontSize = 30,
                    normal = { textColor = Color.white },
                    alignment = TextAnchor.UpperLeft
                });

                Observable
                    .Timer(interval, interval, cancellation)
                    .Select((frameTiming, trigger, styleBox, unit: memoryUnit, unitStr),
                        static (_, param) => param)
                    .SubscribeAwait(static async (param, ct) =>
                    {
                        await param.trigger.OnGUIAsync(ct);
                        double ms = param.frameTiming[0].cpuFrameTime;
                        double fps = 1000 / ms;
                        // 確保している総メモリ
                        float totalMemory = Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int)param.unit);
                        string text = $"CPU: {fps:F0}fps ({ms:F1}ms){Environment.NewLine}Memory: {totalMemory:F}{param.unitStr}";

                        Rect rect = new Rect(Screen.safeArea.position, new Vector2(500, 80));
                        while (!ct.IsCancellationRequested)
                        {
                            GUI.Box(rect, text, param.styleBox.Value);
                            await param.trigger.OnGUIAsync(ct);
                        }
                    }, AwaitOperation.Switch);
            }

            return UniTask.CompletedTask;
        }
    }
}