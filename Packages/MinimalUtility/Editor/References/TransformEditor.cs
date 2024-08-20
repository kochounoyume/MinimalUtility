using UnityEngine;

namespace UnityEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Transform))]
    internal class TransformEditor : TransformInspector
    {
        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Transform transform = target as Transform;
            if (transform == null) return;
            transform.position = EditorGUILayout.Vector3Field("World Position", transform.position);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Vector3Field("World Rotation", transform.rotation.eulerAngles);
            EditorGUILayout.Vector3Field("World Scale", transform.lossyScale);
            EditorGUI.EndDisabledGroup();
        }
    }
}