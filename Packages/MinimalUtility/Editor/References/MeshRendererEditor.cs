using UnityEngine;

namespace UnityEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MeshRenderer))]
    internal class MeshRendererEditor : UnityEditor.MeshRendererEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Other2DSettingsGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}