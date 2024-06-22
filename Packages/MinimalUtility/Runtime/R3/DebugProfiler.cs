#if ENABLE_R3 && ENABLE_UNITASK
using System;
using System.Runtime.Serialization;
using System.Text;
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

namespace MinimalUtility.R3
{
    using SourceGenerator;

    /// <summary>
    /// 総メモリ使用量表示の単位指定列挙体.
    /// </summary>
    [GenerateStringConverter]
    public enum MemoryUnit : int
    {
        /// <summary>バイト</summary>
        [EnumMember(Value = "B")]
        B = 0,

        /// <summary>キロバイト</summary>
        [EnumMember(Value = "KB")]
        KB = 1,

        /// <summary>メガバイト</summary>
        [EnumMember(Value = "MB")]
        MB = 2,

        /// <summary>ギガバイト</summary>
        [EnumMember(Value = "GB")]
        GB = 3
    }

    /// <summary>
    /// FPSなどのプロファイル情報を画面に表示するためのクラス.
    /// <remarks>
    /// <see cref="Time.realtimeSinceStartup"/> の公式リファレンスサンプルコードを参照
    /// </remarks>
    /// </summary>
    public class DebugProfiler
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
#pragma warning disable SA1615 // ElementReturnValueMustBeDocumented
        public async UniTask StartAsync(CancellationToken cancellation)
#pragma warning restore SA1615 // ElementReturnValueMustBeDocumented
        {
            TimeSpan interval = TimeSpan.FromSeconds(IntervalSecs);
            StringBuilder sb = new StringBuilder();

            Observable.Timer(interval, interval, UnityTimeProvider.UpdateRealtime)
                .Select(UnityFrameProvider.Update, (_, provider) => provider.GetFrameCount())
                .Pairwise()
                .Subscribe(new { sb, unit = memoryUnit, unitStr = memoryUnitStringConverter.Convert(memoryUnit) },
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
                        float totalMemory = Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int)param.unit);
                        const string formatF = "F";
                        param.sb.Append(totalMemory.ToString(formatF));
                        param.sb.Append(param.unitStr);
                    })
                .RegisterTo(cancellation);

            const string instanceName = "DebugProfiler";
            GameObject instanceObj = new GameObject(instanceName, typeof(AsyncGUITrigger));
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
                GUI.Box(field, sb.ToString(), styleBox);
            }
        }
    }
}
#endif