using UnityEditor;
using UnityEngine;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// ワールドの座標・回転・大きさも表示する<see cref="Transform"/>カスタムエディタ.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Transform))]
    public class TransformInspector : UnityComponentInspector<Transform>
    {
        /// <inheritdoc/>
        protected override string InspectorTypeName => "UnityEditor.TransformInspector";

        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Target.position = EditorGUILayout.Vector3Field("World Position", Target.position);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Vector3Field("World Rotation", Target.rotation.eulerAngles);
            EditorGUILayout.Vector3Field("World Scale", Target.lossyScale);
            EditorGUI.EndDisabledGroup();
        }
    }
}