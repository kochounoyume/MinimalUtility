using System;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace MinimalUtility.Debugging
{
    using UIToolkit;

    /// <summary>
    /// 実機デバッグメニューの実装をするための基底クラス.
    /// </summary>
    public class DebugViewerBase
    {
        private readonly Lazy<UIDocument> uiDocument = new (static () =>
        {
            var go = new GameObject("DebugViewer");
            Object.DontDestroyOnLoad(go);
            return go.AddComponent<UIDocument>();
        });

        /// <summary>
        /// エントリーポイント.
        /// </summary>
        /// <returns>ルート要素.</returns>
        public virtual VisualElement Start()
        {
            var root = uiDocument.Value.rootVisualElement;
            var safeAreaContainer = new SafeAreaContainer();
            root.Add(safeAreaContainer);
            return safeAreaContainer;
        }
    }
}