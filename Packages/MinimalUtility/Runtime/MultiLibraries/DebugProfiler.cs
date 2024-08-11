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

namespace MinimalUtility
{
    /// <summary>
    /// FPSなどのプロファイル情報を画面に表示するためのクラス.
    /// <remarks>
    /// <see cref="Time.realtimeSinceStartup"/> の公式リファレンスサンプルコードを参照
    /// </remarks>
    /// </summary>
    public class DebugProfiler
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

        private sealed class Text
        {
            public string Value { get; set; }
        }

        /// <summary>
        /// プロファイル情報を更新する間隔.
        /// </summary>
        private const float IntervalSecs = 0.5f;

        /// <summary>
        /// プロファイル情報を表示する際の表示範囲.
        /// </summary>
        private readonly Vector2 debugArea = new Vector2(500, 80);

        /// <summary>
        /// 表示する総メモリ使用量の単位.
        /// </summary>
        private readonly MemoryUnit memoryUnit;

        private readonly MemoryUnitStringConverter memoryUnitStringConverter = new MemoryUnitStringConverter();

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
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            TimeSpan interval = TimeSpan.FromSeconds(IntervalSecs);
            Text text = new Text();

            Observable.Timer(interval, interval, UnityTimeProvider.UpdateRealtime, cancellation)
                .Select(UnityFrameProvider.Update, static (_, provider) => provider.GetFrameCount())
                .Pairwise()
                .Subscribe((text, unit: memoryUnit, unitStr: memoryUnitStringConverter.Convert(memoryUnit)),
                    static (pair, param) =>
                    {
                        long count = pair.Current - pair.Previous;
                        float fps = count / IntervalSecs;
                        float ms = IntervalSecs / count * 1000f;
                        // 確保している総メモリ
                        float totalMemory = Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int)param.unit);
                        param.text.Value = $"CPU: {fps:F0}fps ({ms:F1}ms) Memory: {totalMemory:F}{param.unitStr}";
                    });

            GameObject instanceObj = new GameObject("DebugProfiler", typeof(AsyncGUITrigger));
            // 破壊されないように
            Object.DontDestroyOnLoad(instanceObj);

            AsyncGUITrigger trigger = instanceObj.GetComponent<AsyncGUITrigger>();
            await trigger.OnGUIAsync(cancellation);

            const int fontSize = 30;
            GUIStyle styleBox = new GUIStyle(GUI.skin.box)
            {
                fontSize = fontSize,
                normal =
                {
                    textColor = Color.white
                },
                alignment = TextAnchor.UpperLeft
            };
            Rect field = new Rect(Screen.safeArea.position, debugArea);

            await foreach (var unused in trigger.WithCancellation(cancellation))
            {
                GUI.Box(field, text.Value, styleBox);
            }
        }
    }
}