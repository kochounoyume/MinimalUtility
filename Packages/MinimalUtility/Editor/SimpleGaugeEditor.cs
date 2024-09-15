using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// <see cref="UI.SimpleGauge"/>のカスタムエディタ.
    /// </summary>
    [CustomEditor(typeof(UI.SimpleGauge))]
    public class SimpleGaugeEditor : UnityEditor.Editor
    {
        /// <inheritdoc/>
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new ();
            root.Add(new PropertyField(serializedObject.FindProperty("mode")));
            root.Add(new PropertyField(serializedObject.FindProperty("value")));
            return root;
        }
    }
}