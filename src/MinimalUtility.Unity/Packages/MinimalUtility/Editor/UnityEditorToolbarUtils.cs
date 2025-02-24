#nullable enable

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor
{
    public static class UnityEditorToolbarUtils
    {
        private static readonly Type s_toolbatType = typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.Toolbar");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRight(Func<VisualElement> factory) 
            => EditorApplication.delayCall += () => GetToolbarRoot()?.Q("ToolbarZoneRightAlign")?.Add(factory());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddLeft(Func<VisualElement> factory) 
            => EditorApplication.delayCall += () => GetToolbarRoot()?.Q("ToolbarZoneLeftAlign")?.Add(factory());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCenter(bool rightSide, Func<VisualElement> factory)
        {
            EditorApplication.delayCall += () =>
            {
                var center = GetToolbarRoot()?.Q("ToolbarZonePlayMode");
                center.Add(new Button(){ text = "a" });
                if (rightSide)
                {
                    center?.Add(factory());
                }
                else
                {
                    center?.Insert(0, factory());
                }
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static VisualElement? GetToolbarRoot()
        {
            var toolbar = Resources.FindObjectsOfTypeAll(s_toolbatType).FirstOrDefault();
            if (toolbar == null) return null;
            
            var fieldInfo = s_toolbatType.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
            return fieldInfo?.GetValue(toolbar) as VisualElement;
        }
    }
}