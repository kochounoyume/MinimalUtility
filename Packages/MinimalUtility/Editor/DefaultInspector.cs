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
                    var btn = new Button()
                    {
                        text = attr.buttonName
                    };
                    btn.RegisterCallback<ClickEvent, (MethodInfo method, UnityEngine.Object target, object[] args)>(static (_, method) =>
                    {
                        method.method.Invoke(method.target, method.args);
                    }, (methodInfo, target, attr.parameters));
                    root.Add(btn);
                }
            }
            return root;
        }
    }
}
