using System;
using System.Collections;
using UnityEngine;

namespace MinimalUtility
{
    /// <summary>
    /// プロファイル情報を表示するためのクラス.
    /// </summary>
    [Unity.Profiling.IgnoredByDeepProfiler]
    public partial class DebugProfileViewer : FrameDataProvider
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
        /// OnGUIイベントを取得するトリガークラス.
        /// </summary>
        [DisallowMultipleComponent]
        protected class OnGUITrigger : EmptyMonoBehaviour
        {
            /// <summary>
            /// OnGUIのタイミングで呼ばれるイベント.
            /// </summary>
            public event Action<FrameTiming> OnGUIEvent;

            /// <summary>
            /// イベントの引数として渡される、フレームタイミング情報.
            /// </summary>
            public FrameTiming State { get; set; }

            private void OnGUI() => OnGUIEvent?.Invoke(State);
        }

        private const float IntervalSecs = 0.5f;
        private readonly Lazy<WaitForSeconds> intervalWait = new (static () => new WaitForSeconds(IntervalSecs));
        private readonly Lazy<OnGUITrigger> onGUITrigger;
        private readonly MemoryUnitStringConverter memoryUnitStringConverter = new ();
        // GUIStyleは生成タイミングがOnGUIの中でないとエラーになるため、Lazyで遅延生成
        private readonly Lazy<GUIStyle> styleBox = new (static () => new GUIStyle(GUI.skin.box)
        {
            fontSize = 30,
            normal = { textColor = Color.white },
            alignment = TextAnchor.UpperLeft
        });

        /// <summary>
        /// 表示する総メモリ使用量の単位.
        /// </summary>
        public MemoryUnit Unit { get; set; } = MemoryUnit.GB;

        private bool isGUIVisible = false;

        /// <summary>
        /// GUI表示の可視状態.
        /// </summary>
        public bool IsGUIVisible
        {
            get => isGUIVisible;
            set
            {
                var before = isGUIVisible;
                isGUIVisible = value;
                if (value && !before)
                {
                    onGUITrigger.Value.OnGUIEvent += UpdateGUI;
                    CoroutineRunner.Value.StartCoroutine(UpdateGUIInterval());
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugProfileViewer"/> class.
        /// </summary>
        public DebugProfileViewer() : base()
        {
            onGUITrigger = new Lazy<OnGUITrigger>(Cast);

            OnGUITrigger Cast() => CoroutineRunner.Value as OnGUITrigger;
        }

        /// <inheritdoc/>
        protected override EmptyMonoBehaviour GetCoroutineRunner()
        {
            var go = new GameObject(RunnerName, typeof(OnGUITrigger));
            UnityEngine.Object.DontDestroyOnLoad(go);
            return go.GetComponent<OnGUITrigger>();
        }

        private IEnumerator UpdateGUIInterval()
        {
            while (isGUIVisible && CoroutineRunner.Value != null)
            {
                onGUITrigger.Value.State = LatestFrameData;
                yield return intervalWait.Value;
            }
        }

        private void UpdateGUI(FrameTiming latest)
        {
            double ms = latest.cpuFrameTime;
            double fps = 1000 / ms;
            // 確保している総メモリ
            float totalMemory = UnityEngine.Profiling.Profiler.GetTotalReservedMemoryLong() / Mathf.Pow(1024f, (int)Unit);
            string text = $"CPU: {fps:F0}fps ({ms:F1}ms){Environment.NewLine}Memory: {totalMemory:F}{memoryUnitStringConverter.Convert(Unit)}";
            Rect rect = new Rect(Screen.safeArea.position, new Vector2(500, 80));
            GUI.Box(rect, text, styleBox.Value);
        }
    }
}