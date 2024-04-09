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
        private const float UpdateIntervalSecs = 0.5f;

        /// <summary>
        /// プロファイル情報を表示する際の表示領域(セーフエリアを考慮).
        /// </summary>
        private readonly Rect debugField;

        /// <summary>
        /// 表示する総メモリ使用量の単位.
        /// </summary>
        private readonly MemoryUnit memoryUnit;

        private readonly IReadOnlyDictionary<MemoryUnit, string> memoryUnitStrings
            = new Dictionary<MemoryUnit, string>(default(MemoryUnitEqualityComparer))
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
        /// プロファイル情報を更新する間隔.
        /// </summary>
        private readonly TimeSpan updateInterval = TimeSpan.FromSeconds(UpdateIntervalSecs);

        /// <summary>
        /// このクラスのインスタンスが破棄されたときに同時に破棄される購読.
        /// </summary>
        private IDisposable subscription;

        /// <summary>
        /// 次に表示するプロファイル情報テキスト.
        /// </summary>
        private string nextDebugText = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugProfiler"/> class.
        /// </summary>
        /// <param name="memoryUnit">総メモリ使用量表示の単位.</param>
        public DebugProfiler(MemoryUnit memoryUnit)
        {
            this.memoryUnit = memoryUnit;

            // セーフエリアを考慮した表示領域を取得
            this.debugField = new Rect(Screen.safeArea.position, new Vector2(500, 80));
        }

        /// <summary>
        /// プロファイル情報の表示を開始する.
        /// </summary>
        public void Start()
        {
            subscription?.Dispose();

            // 初期化
            subscription =
                Observable.Timer(updateInterval, updateInterval, UnityTimeProvider.UpdateRealtime)
                    .Select(static _ => Time.frameCount)
                    .Pairwise()
                    .Select(this, static (count, profiler) => (count: count.Current - count.Previous, profiler))
                    .Subscribe(static param =>
                    {
                        ref readonly StringBuilder sb = ref param.profiler.sb;
                        sb.Clear();
                        const string cpuText = "CPU: ";
                        sb.Append(cpuText);
                        float fps = param.count / UpdateIntervalSecs;
                        const string formatF0 = "F0";
                        sb.Append(fps.ToString(formatF0));
                        const string fpsText = "fps (";
                        sb.Append(fpsText);
                        float ms = UpdateIntervalSecs / param.count * 1000f;
                        const string formatF1 = "F1";
                        sb.Append(ms.ToString(formatF1));
                        const string msText = "ms)";
                        sb.AppendLine(msText);
                        const string memoryText = "Memory: ";
                        sb.Append(memoryText);
                        // 確保している総メモリ
                        ref readonly MemoryUnit memoryUnit = ref param.profiler.memoryUnit;
                        float totalMemory =
                            Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int)memoryUnit);
                        const string formatF = "F";
                        sb.Append(totalMemory.ToString(formatF));
                        sb.Append(param.profiler.memoryUnitStrings[memoryUnit]);
                        param.profiler.nextDebugText = sb.ToString();
                    });

            GetInstanceObj()
                .OnGUIAsObservable()
                .Select(this, static (_, profiler) => profiler)
                .Subscribe(static profiler =>
                {
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
                    GUI.Box(profiler.debugField, profiler.nextDebugText, styleBox);
                });
        }

        /// <inheritdoc/>
        void IDisposable.Dispose()
        {
            subscription?.Dispose();
            subscription = null;
        }

        /// <summary>
        /// メインループを回すためのGameObjectを生成・取得する.
        /// </summary>
        /// <returns>メインループを回すためのGameObject.</returns>
        private static GameObject GetInstanceObj()
        {
            const string instanceName = "DebugProfiler";
            GameObject instanceObj = new GameObject(instanceName);
            // 破壊されないように
            Object.DontDestroyOnLoad(instanceObj);
            return instanceObj;
        }
    }
}
#endif