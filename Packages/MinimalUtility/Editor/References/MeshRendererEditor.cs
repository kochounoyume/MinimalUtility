using UnityEngine;

namespace UnityEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MeshRenderer))]
    internal class MeshRendererEditor : UnityEditor.MeshRendererEditor
    {
        private SerializedProperty m_SortingOrder;
        private SerializedProperty m_SortingLayerID;
        private GUIContent extraSettingsStyle;

        public override void OnEnable()
        {
            base.OnEnable();
            m_SortingOrder = serializedObject.FindProperty(nameof(m_SortingOrder));
            m_SortingLayerID = serializedObject.FindProperty(nameof(m_SortingLayerID));
            extraSettingsStyle = EditorGUIUtility.TrTextContent("Extra Settings");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ExtraSettingsGUI();
            serializedObject.ApplyModifiedProperties();
        }

        private void ExtraSettingsGUI()
        {
            m_ShowOtherSettings.value = EditorGUILayout.BeginFoldoutHeaderGroup(m_ShowOtherSettings.value, extraSettingsStyle);
            if (m_ShowOtherSettings.value)
            {
                ++EditorGUI.indentLevel;
                SortingLayerEditorUtility.RenderSortingLayerFields(m_SortingOrder, m_SortingLayerID);
                DrawRenderingLayer();
                --EditorGUI.indentLevel;
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}