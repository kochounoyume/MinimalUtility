using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

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
        /// <inheritdoc/>
        protected override string InspectorTypeName => "UnityEditor.AnimatorInspector";

        /// <inheritdoc/>
        public override VisualElement CreateInspectorGUI()
        {
            SerializedProperty keepProperty = serializedObject.FindProperty("m_KeepAnimatorStateOnDisable");
            SerializedProperty writeProperty = serializedObject.FindProperty("m_WriteDefaultValuesOnDisable");

            VisualElement root = base.CreateInspectorGUI();
            root.Add(new PropertyField(keepProperty)
            {
                label = keepProperty.displayName,
                tooltip = "GameObjectが無効化された時にAnimatorControllerの状態を保持するかどうか"
            });
            root.Add(new PropertyField(writeProperty)
            {
                label = writeProperty.displayName,
                tooltip = "Animatorが無効化された時にデフォルト値を書き込むかどうか"
            });
            return root;
        }
    }
}