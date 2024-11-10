using System;
using UnityEditor;
using UnityEngine;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// <see cref="Animator.keepAnimatorStateOnDisable"/>や<see cref="Animator.writeDefaultValuesOnDisable"/>も編集できる
    /// <see cref="Animator"/>カスタムエディタ.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Animator))]
    public class AnimatorInspector : UnityComponentInspector<Animator>
    {
        private readonly Lazy<GUIContent> keepAnimatorStateStyle = new (static () =>
        {
            const string displayName = "Keep Animator State On Disable";
            const string tooltip = "GameObjectが無効化された時にAnimatorControllerの状態を保持するかどうか";
            const string key = displayName + "|" + tooltip;
            return EditorGUIUtility.TrTextContent(key, displayName, tooltip, null);
        });

        private readonly Lazy<GUIContent> writeDefaultValuesStyle = new (static () =>
        {
            const string displayName = "Write Default Values On Disable";
            const string tooltip = "Animatorが無効化された時にデフォルト値を書き込むかどうか";
            const string key = displayName + "|" + tooltip;
            return EditorGUIUtility.TrTextContent(key, displayName, tooltip, null);
        });

#pragma warning disable SA1308
        private SerializedProperty m_KeepAnimatorStateOnDisable;
        private SerializedProperty m_WriteDefaultValuesOnDisable;
#pragma warning restore SA1308

        /// <inheritdoc/>
        protected override string InspectorTypeName => "UnityEditor.AnimatorInspector";

        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            m_KeepAnimatorStateOnDisable ??= serializedObject.FindProperty(nameof(m_KeepAnimatorStateOnDisable));
            m_WriteDefaultValuesOnDisable ??= serializedObject.FindProperty(nameof(m_WriteDefaultValuesOnDisable));

            EditorGUILayout.PropertyField(m_KeepAnimatorStateOnDisable, keepAnimatorStateStyle.Value);
            EditorGUILayout.PropertyField(m_WriteDefaultValuesOnDisable, writeDefaultValuesStyle.Value);

            serializedObject.ApplyModifiedProperties();
        }
    }
}