#nullable enable

using UnityEditor;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// 既存のUnityコンポーネントのカスタムエディタを作成するための基底クラスです.
    /// </summary>
    /// <typeparam name="T">カスタムエディタを作成するコンポーネントの型.</typeparam>
    public abstract class UnityComponentInspector<T> : UnityEditor.Editor where T : UnityEngine.Component
    {
        private UnityEditor.Editor? _editor;

        /// <summary>
        /// 対象となるコンポーネントのインスペクター拡張既存クラスの名前.
        /// </summary>
        protected abstract string inspectorTypeName { get; }

        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            _editor?.OnInspectorGUI();
        }

        private void OnEnable()
        {
            CreateCachedEditor(target, typeof(EditorApplication).Assembly.GetType(inspectorTypeName), ref _editor);
        }

        private void OnDisable()
        {
            DestroyImmediate(_editor);
        }
    }
}