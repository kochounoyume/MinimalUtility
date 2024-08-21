using UnityEditor;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// 既存のUnityコンポーネントのカスタムエディタを作成するための基底クラスです.
    /// </summary>
    /// <typeparam name="T">カスタムエディタを作成するコンポーネントの型.</typeparam>
    public abstract class UnityComponentInspector<T> : UnityEditor.Editor where T : UnityEngine.Component
    {
        private UnityEditor.Editor editor;
        private new T target;

        /// <summary>
        /// 対象となるコンポーネントのインスタンス.
        /// </summary>
        protected T Target => target == null ? target = base.target as T : target;

        /// <summary>
        /// 対象となるコンポーネントのインスペクター拡張既存クラスの名前.
        /// </summary>
        protected abstract string InspectorTypeName { get; }

        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            editor.OnInspectorGUI();
        }

        /// <summary>
        /// ロードされたときに呼び出される.
        /// </summary>
        protected virtual void OnEnable()
        {
            editor = CreateEditor(Target, typeof(EditorApplication).Assembly.GetType(InspectorTypeName));
        }

        /// <summary>
        /// オブジェクトがスコープ外になったときに呼び出される.
        /// </summary>
        protected virtual void OnDisable()
        {
            DestroyImmediate(editor);
        }
    }
}