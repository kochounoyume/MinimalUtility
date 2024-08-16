using UnityEditor;
using UnityEngine;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// <see cref="Transform"/>のカスタムインスペクター.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Transform))]
    public class TransformEditor : UnityEditor.Editor
    {
        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            Transform transform = target as Transform;
            if (transform == null) return;
            transform.position = EditorGUILayout.Vector3Field("World Position", transform.position);
            transform.eulerAngles = EditorGUILayout.Vector3Field("World Rotation", transform.rotation.eulerAngles);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Vector3Field("World Scale", transform.lossyScale);
            EditorGUI.EndDisabledGroup();
            base.OnInspectorGUI();
        }
    }
}