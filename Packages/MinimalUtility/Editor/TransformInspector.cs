using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
        public override VisualElement CreateInspectorGUI()
        {
            transformCache = (Transform)target;

            VisualElement root = base.CreateInspectorGUI();
            root.Add(new IMGUIContainer(() =>
            {
                transformCache.position = EditorGUILayout.Vector3Field("World Position", transformCache.position);
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.Vector3Field("World Rotation", transformCache.rotation.eulerAngles);
                    EditorGUILayout.Vector3Field("World Scale", transformCache.lossyScale);
                }
            }));
            return root;
        }
    }
}