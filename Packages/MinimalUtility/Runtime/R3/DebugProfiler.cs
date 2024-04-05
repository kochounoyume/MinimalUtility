#if ENABLE_R3
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;
using R3;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using Screen = UnityEngine.Device.Screen;
#else
using Screen = UnityEngine.Screen;
#endif

namespace MinimalUtility.R3
{
    /// <summary>
    /// FPSなどのプロファイル情報を画面に表示するためのクラス
    /// <remarks>
    /// <see cref="Time.realtimeSinceStartup"/> の公式リファレンスサンプルコードを参照
    /// </remarks>
    /// </summary>
    public class DebugProfiler : IDisposable
    {
        /// <summary>
        /// 総メモリ使用量表示の単位指定列挙体
        /// </summary>
        public enum MemoryUnit : int
        {
            B = 0,
            KB = 1,
            MB = 2,
            GB = 3
        }

        private readonly struct MemoryUnitEqualityComparer : IEqualityComparer<MemoryUnit>
        {
            bool IEqualityComparer<MemoryUnit>.Equals(MemoryUnit x, MemoryUnit y) => (int)x == (int)y;

            int IEqualityComparer<MemoryUnit>.GetHashCode(MemoryUnit obj) => ((int)obj).GetHashCode();
        }

        private readonly IReadOnlyDictionary<MemoryUnit, string> memoryUnitStrings
            = new Dictionary<MemoryUnit, string>(new MemoryUnitEqualityComparer())
        {
            { MemoryUnit.B, "B" },
            { MemoryUnit.KB, "KB" },
            { MemoryUnit.MB, "MB" },
            { MemoryUnit.GB, "GB" }
        };

        /// <summary>
        /// 表示する総メモリ使用量の単位
        /// </summary>
        private readonly MemoryUnit memoryUnit;

        /// <summary>
        /// プロファイル情報を表示する際の表示領域(セーフエリアを考慮)
        /// </summary>
        private readonly Rect debugField;

        /// <summary>
        /// 使いまわせるStringBuilder
        /// </summary>
        private readonly StringBuilder sb = new StringBuilder();

        /// <summary>
        /// プロファイル情報を更新する間隔
        /// </summary>
        private readonly TimeSpan updateInterval = TimeSpan.FromSeconds(updateIntervalSecs);

        /// <summary>
        /// プロファイル情報を更新する間隔
        /// </summary>
        private const float updateIntervalSecs = 0.5f;

        /// <summary>
        /// このクラスのインスタンスが破棄されたときに同時に破棄される購読
        /// </summary>
        private IDisposable subscription;

        /// <summary>
        /// 次に表示するプロファイル情報テキスト
        /// </summary>
        private string nextDebugText = "";

        public DebugProfiler(MemoryUnit memoryUnit)
        {
            this.memoryUnit = memoryUnit;

            // セーフエリアを考慮した表示領域を取得
            this.debugField = new Rect(Screen.safeArea.position, new Vector2(500, 80));
        }

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
                        float fps = param.count / updateIntervalSecs;
                        const string formatF0 = "F0";
                        sb.Append(fps.ToString(formatF0));
                        const string fpsText = "fps (";
                        sb.Append(fpsText);
                        float ms = updateIntervalSecs / param.count * 1000f;
                        const string formatF1 = "F1";
                        sb.Append(ms.ToString(formatF1));
                        const string msText = "ms)";
                        sb.AppendLine(msText);
                        const string memoryText = "Memory: ";
                        sb.Append(memoryText);
                        // 確保している総メモリ
                        ref readonly MemoryUnit memoryUnit = ref param.profiler.memoryUnit;
                        float totalMemory =
                            Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int) memoryUnit);
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

        void IDisposable.Dispose()
        {
            subscription?.Dispose();
            subscription = null;
        }

        /// <summary>
        /// メインループを回すためのGameObjectを生成・取得する
        /// </summary>
        /// <returns></returns>
        static GameObject GetInstanceObj()
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