using UnityEditor;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// 既存のUnityコンポーネントのカスタムエディタを作成するための基底クラスです.
    /// </summary>
    /// <typeparam name="T">カスタムエディタを作成するコンポーネントの型.</typeparam>
    public abstract class UnityComponentInspector<T> : UnityEditor.Editor where T : UnityEngine.Component
    {
        private UnityEditor.Editor editor;

        /// <summary>
        /// 対象となるコンポーネントのインスペクター拡張既存クラスの名前.
        /// </summary>
        protected abstract string InspectorTypeName { get; }

        /// <inheritdoc/>
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new ();
            root.Add(new IMGUIContainer(() =>
            {
                editor?.OnInspectorGUI();
            }) { style = { paddingBottom = 2 } });
            return root;
        }

        private void OnEnable()
        {
            CreateCachedEditor(target, typeof(EditorApplication).Assembly.GetType(InspectorTypeName), ref editor);
        }

        private void OnDisable()
        {
            DestroyImmediate(editor);
        }
    }
}