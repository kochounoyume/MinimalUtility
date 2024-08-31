using System;
using System.Collections;
using UnityEngine;

namespace MinimalUtility
{
    using static FrameDataProvider;

    /// <summary>
    /// プロファイル情報を表示するためのクラス.
    /// </summary>
    [Unity.Profiling.IgnoredByDeepProfiler]
    public class DebugProfileViewer
    {
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
        private readonly FrameDataProvider frameDataProvider = new (CreateCoroutineRunner<OnGUITrigger>);
        private readonly WaitForSeconds intervalWait = new (IntervalSecs);
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
                if (value == isGUIVisible) return;
                isGUIVisible = value;
                if (value)
                {
                    onGUITrigger.Value.OnGUIEvent += UpdateGUI;
                    frameDataProvider.CoroutineRunner.StartCoroutine(UpdateGUIInterval());
                }
                else
                {
                    onGUITrigger.Value.OnGUIEvent -= UpdateGUI;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugProfileViewer"/> class.
        /// </summary>
        public DebugProfileViewer()
        {
            onGUITrigger = new Lazy<OnGUITrigger>(Cast);

            OnGUITrigger Cast() => frameDataProvider.CoroutineRunner as OnGUITrigger;
        }

        private IEnumerator UpdateGUIInterval()
        {
            while (isGUIVisible && frameDataProvider.CoroutineRunner != null)
            {
                onGUITrigger.Value.State = frameDataProvider.LatestFrameData;
                yield return intervalWait;
            }
        }

        private void UpdateGUI(FrameTiming latest)
        {
            double ms = latest.cpuFrameTime;
            double fps = 1000 / ms;
            // 確保している総メモリ
            float totalMemory = GetTotalMemory(Unit);
            string text = $"CPU: {fps:F0}fps ({ms:F1}ms){Environment.NewLine}Memory: {totalMemory:F}{memoryUnitStringConverter.Convert(Unit)}";
            Rect rect = new Rect(Screen.safeArea.position, new Vector2(500, 80));
            GUI.Box(rect, text, styleBox.Value);
        }
    }
}