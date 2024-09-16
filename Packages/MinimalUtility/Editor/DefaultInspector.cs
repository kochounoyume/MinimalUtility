using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor
{
    /// <summary>
    /// デフォルトのインスペクタービュー.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class DefaultInspector : UnityEditor.Editor
    {
        /// <inheritdoc/>
        public override VisualElement CreateInspectorGUI()
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            VisualElement root = new ();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            foreach (MethodInfo methodInfo in target.GetType().GetMethods(flags))
            {
                foreach (ButtonAttribute attr in methodInfo.GetCustomAttributes<ButtonAttribute>())
                {
                    if (attr == null) continue;
                    root.Add(new Button(() => methodInfo.Invoke(target, attr.Parameters))
                    {
                        text = attr.ButtonName
                    });
                }
            }
            return root;
        }
    }
}
