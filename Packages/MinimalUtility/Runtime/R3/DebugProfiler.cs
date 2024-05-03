#if ENABLE_R3
using System;
using System.Collections.Generic;
using System.Text;
using R3;
using UnityEngine;
using UnityEngine.Profiling;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using Screen = UnityEngine.Device.Screen;
#else
using Screen = UnityEngine.Screen;
#endif

namespace MinimalUtility.R3
{
    using SourceGenerator;

    /// <summary>
    /// 総メモリ使用量表示の単位指定列挙体.
    /// </summary>
    [GenerateEqualityComparer]
    public enum MemoryUnit : int
    {
        /// <summary>バイト</summary>
        B = 0,

        /// <summary>キロバイト</summary>
        KB = 1,

        /// <summary>メガバイト</summary>
        MB = 2,

        /// <summary>ギガバイト</summary>
        GB = 3
    }

    /// <summary>
    /// FPSなどのプロファイル情報を画面に表示するためのクラス.
    /// <remarks>
    /// <see cref="Time.realtimeSinceStartup"/> の公式リファレンスサンプルコードを参照
    /// </remarks>
    /// </summary>
    public class DebugProfiler : IDisposable
    {
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

        private readonly IReadOnlyDictionary<MemoryUnit, string> memoryUnitStrings
            = new Dictionary<MemoryUnit, string>(new MemoryUnitEqualityComparer())
            {
                { MemoryUnit.B, "B" },
                { MemoryUnit.KB, "KB" },
                { MemoryUnit.MB, "MB" },
                { MemoryUnit.GB, "GB" }
            };

        /// <summary>
        /// 使いまわせるStringBuilder.
        /// </summary>
        private readonly StringBuilder sb = new StringBuilder();

        /// <summary>
        /// このクラスのインスタンスが破棄されたときに同時に破棄される購読.
        /// </summary>
        private IDisposable subscription;

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
        public void Start()
        {
            subscription?.Dispose();

            TimeSpan interval = TimeSpan.FromSeconds(IntervalSecs);

            Observable.Timer(interval, interval, UnityTimeProvider.UpdateRealtime)
                .Select(UnityFrameProvider.Update, (_, provider) => provider.GetFrameCount())
                .Pairwise()
                .Subscribe(new { sb, unit = memoryUnit, unitStr = memoryUnitStrings[memoryUnit] },
                    static (pair, param) =>
                    {
                        long count = pair.Current - pair.Previous;
                        param.sb.Clear();
                        const string cpuText = "CPU: ";
                        param.sb.Append(cpuText);
                        float fps = count / IntervalSecs;
                        const string formatF0 = "F0";
                        param.sb.Append(fps.ToString(formatF0));
                        const string fpsText = "fps (";
                        param.sb.Append(fpsText);
                        float ms = IntervalSecs / count * 1000f;
                        const string formatF1 = "F1";
                        param.sb.Append(ms.ToString(formatF1));
                        const string msText = "ms)";
                        param.sb.AppendLine(msText);
                        const string memoryText = "Memory: ";
                        param.sb.Append(memoryText);
                        // 確保している総メモリ
                        float totalMemory =
                            Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int)param.unit);
                        const string formatF = "F";
                        param.sb.Append(totalMemory.ToString(formatF));
                        param.sb.Append(param.unitStr);
                    })
                .AddTo(ref subscription);

            const string instanceName = "DebugProfiler";
            GameObject instanceObj = new GameObject(instanceName);
            // 破壊されないように
            Object.DontDestroyOnLoad(instanceObj);

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

            instanceObj
                .OnGUIAsObservable()
                .Subscribe(new { sb, styleBox, field }, static (_, param) =>
                {
                    GUI.Box(param.field, param.sb.ToString(), param.styleBox);
                });
        }

        /// <inheritdoc/>
        void IDisposable.Dispose()
        {
            subscription?.Dispose();
            subscription = null;
        }
    }
}
#endif