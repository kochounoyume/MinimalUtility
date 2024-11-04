using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace MinimalUtility.Debugging
{
    using UIToolkit;

    /// <summary>
    /// 実機デバッグメニューの実装をするための基底クラス.
    /// </summary>
    public abstract class DebugViewerBase
    {
        /// <summary>
        /// パネル設定.
        /// </summary>
        public PanelSettings PanelSettings { get; set; }

        /// <summary>
        /// tss.
        /// </summary>
        public ThemeStyleSheet ThemeStyleSheet { get; set; }

        /// <summary>
        /// エントリーポイント.
        /// </summary>
        /// <returns>ルート要素.</returns>
        public virtual VisualElement Start()
        {
            var uiDocument = new GameObject("DebugViewer").AddComponent<UIDocument>();
            Object.DontDestroyOnLoad(uiDocument);
            if (PanelSettings == null)
            {
                PanelSettings = ScriptableObject.CreateInstance<PanelSettings>();
                if (ThemeStyleSheet == null)
                {
                    ThemeStyleSheet = Resources.Load<ThemeStyleSheet>("DefaultRuntimeTheme");
                }
                PanelSettings.themeStyleSheet = ThemeStyleSheet;
                PanelSettings.scaleMode = PanelScaleMode.ScaleWithScreenSize;
            }
            uiDocument.panelSettings = PanelSettings;

            var root = uiDocument.rootVisualElement;
            var safeAreaContainer = new SafeAreaContainer();
            root.Add(safeAreaContainer);
            return safeAreaContainer;
        }
    }
}