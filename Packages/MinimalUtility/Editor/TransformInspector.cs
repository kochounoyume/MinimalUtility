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
        private Transform transformCache;

        /// <inheritdoc/>
        protected override string InspectorTypeName => "UnityEditor.TransformInspector";

        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            transformCache.position = EditorGUILayout.Vector3Field("World Position", transformCache.position);
            using (new EditorGUI.DisabledScope(true))
            {
                EditorGUILayout.Vector3Field("World Rotation", transformCache.rotation.eulerAngles);
                EditorGUILayout.Vector3Field("World Scale", transformCache.lossyScale);
            }
        }
    }
}