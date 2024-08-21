using UnityEditor;
using UnityEngine;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// <see cref="Animator"/>コンポーネントのカスタムエディタ.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Animator))]
    public class AnimatorInspector : UnityComponentInspector<Animator>
    {
#pragma warning disable SA1308
        private SerializedProperty m_KeepAnimatorStateOnDisable;
        private SerializedProperty m_WriteDefaultValuesOnDisable;
#pragma warning restore SA1308
        private GUIContent keepAnimatorStateStyle;
        private GUIContent writeDefaultValuesStyle;

        /// <inheritdoc/>
        protected override string InspectorTypeName => "UnityEditor.AnimatorInspector";

        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(m_KeepAnimatorStateOnDisable, keepAnimatorStateStyle);
            EditorGUILayout.PropertyField(m_WriteDefaultValuesOnDisable, writeDefaultValuesStyle);
            serializedObject.ApplyModifiedProperties();
        }

        /// <inheritdoc/>
        protected override void OnEnable()
        {
            base.OnEnable();
            m_KeepAnimatorStateOnDisable = serializedObject.FindProperty(nameof(m_KeepAnimatorStateOnDisable));
            m_WriteDefaultValuesOnDisable = serializedObject.FindProperty(nameof(m_WriteDefaultValuesOnDisable));
            keepAnimatorStateStyle = MinimalEditorUtility.TrTextContent(
                m_KeepAnimatorStateOnDisable.displayName,
                "GameObjectが無効化された時にAnimatorControllerの状態を保持するかどうか");
            writeDefaultValuesStyle = MinimalEditorUtility.TrTextContent(
                    m_WriteDefaultValuesOnDisable.displayName,
                    "Animatorが無効化された時にデフォルト値を書き込むかどうか");
        }
    }
}