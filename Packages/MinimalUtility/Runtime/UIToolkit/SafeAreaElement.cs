using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using Screen = UnityEngine.Device.Screen;
#else
using Screen = UnityEngine.Screen;
#endif

namespace MinimalUtility.UIToolkit
{
    /// <summary>
    /// セーフエリアを考慮した<see cref="VisualElement"/>.
    /// </summary>
    public class SafeAreaElement : VisualElement
    {
        /// <summary>
        /// UIBuilderのLibraryに登録するためのUXML要素のファクトリクラス.
        /// </summary>
        public class SafeAreaFactory : UxmlFactory<SafeAreaElement, UxmlTraits>
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeAreaElement"/> class.
        /// </summary>
        public SafeAreaElement()
        {
            style.flexGrow = 1;
            style.flexShrink = 1;

            RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);

#pragma warning disable SA1313
            void OnGeometryChanged(GeometryChangedEvent _)
#pragma warning restore SA1313
            {
#if UNITY_EDITOR
                if (panel.GetType().Name == "EditorPanel") return;
#endif
                Rect safeArea = Screen.safeArea;
                Vector2 leftTop
                    = RuntimePanelUtils.ScreenToPanel(panel, new (safeArea.xMin, Screen.height - safeArea.yMax));
                Vector2 rightBottom
                    = RuntimePanelUtils.ScreenToPanel(panel, new (Screen.width - safeArea.xMax, safeArea.yMin));

                style.marginLeft = leftTop.x;
                style.marginTop = leftTop.y;
                style.marginRight = rightBottom.x;
                style.marginBottom = rightBottom.y;
            }
        }
    }
}