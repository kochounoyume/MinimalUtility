using UnityEngine;

namespace UnityEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Animator))]
    internal class AnimatorEditor : AnimatorInspector
    {
        private const string keepAnimatorStateDirection = "GameObjectが無効化された時にAnimatorControllerの状態を保持するかどうか";
        private const string writeDefaultValuesDirection = "Animatorが無効化された時にデフォルト値を書き込むかどうか";

        private SerializedProperty m_KeepAnimatorStateOnDisable;
        private GUIContent keepAnimatorStateContent;
        private SerializedProperty m_WriteDefaultValuesOnDisable;
        private GUIContent writeDefaultValuesContent;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            m_KeepAnimatorStateOnDisable ??= serializedObject.FindProperty(nameof(m_KeepAnimatorStateOnDisable));
            m_WriteDefaultValuesOnDisable ??= serializedObject.FindProperty(nameof(m_WriteDefaultValuesOnDisable));
            keepAnimatorStateContent ??= string.IsNullOrEmpty(m_KeepAnimatorStateOnDisable.displayName)
                    ? new GUIContent(m_KeepAnimatorStateOnDisable.displayName, keepAnimatorStateDirection) : null;
            writeDefaultValuesContent ??= string.IsNullOrEmpty(m_WriteDefaultValuesOnDisable.displayName)
                    ? new GUIContent(m_WriteDefaultValuesOnDisable.displayName, writeDefaultValuesDirection) : null;

            EditorGUILayout.PropertyField(m_KeepAnimatorStateOnDisable, keepAnimatorStateContent);
            EditorGUILayout.PropertyField(m_WriteDefaultValuesOnDisable, writeDefaultValuesContent);
            serializedObject.ApplyModifiedProperties();
        }
    }
}