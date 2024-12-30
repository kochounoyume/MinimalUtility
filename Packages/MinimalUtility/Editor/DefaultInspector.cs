#nullable enable

using System;
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

            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            foreach (var methodInfo in target.GetType().GetMethods(flags).AsSpan())
            {
                foreach (var attr in methodInfo.GetCustomAttributes<ButtonAttribute>())
                {
                    if (attr == null) continue;
                    root.Add(new Button(() => methodInfo.Invoke(target, attr.parameters))
                    {
                        text = attr.buttonName
                    });
                }
            }
            return root;
        }
    }
}
