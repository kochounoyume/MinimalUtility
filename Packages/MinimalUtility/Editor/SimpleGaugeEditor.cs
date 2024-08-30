﻿using UnityEditor;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// <see cref="SimpleGauge"/>のカスタムエディタ.
    /// </summary>
    [CustomEditor(typeof(UI.SimpleGauge))]
    public class SimpleGaugeEditor : UnityEditor.Editor
    {
        private SerializedProperty mode;
        private SerializedProperty value;

        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(mode);
            EditorGUILayout.PropertyField(value);
            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            mode = serializedObject.FindProperty(nameof(mode));
            value = serializedObject.FindProperty(nameof(value));
        }
    }
}