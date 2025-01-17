#nullable enable

using System;
using UnityEditor;
using UnityEngine;

namespace MinimalUtility.Editor.DataBind
{
    using MinimalUtility.DataBind;

    internal static class DataBindUtils
    {
        private static readonly Lazy<Texture2D?> s_removeIcon =
            new(static () => EditorGUIUtility.Load("Toolbar Minus") as Texture2D);
        public static Type GetTargetType(Type type)
        {
            var baseType = type.BaseType;
            while (baseType != null)
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(TargetBindElement<>))
                {
                    return baseType.GetGenericArguments()[0];
                }
                baseType = baseType.BaseType;
            }
            throw new InvalidOperationException("TargetBindElement<> not found in the type hierarchy.");
        }

        public static Texture2D? LoadMiniTypeThumbnail(Type type)
        {
#if ENABLE_TEXTMESHPRO
            if (type == typeof(TMPro.TextMeshProUGUI) || type == typeof(TMPro.TMP_InputField))
            {
#if UNITY_2023_2_OR_NEWER
                return AssetDatabase.LoadAssetAtPath<Texture2D>(
                    "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Text Component Icon.psd");
#else
                return AssetDatabase.LoadAssetAtPath<Texture2D>(
                    "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Text Component Icon.psd");
#endif
            }
#endif
            return AssetPreview.GetMiniTypeThumbnail(type) ?? EditorGUIUtility.Load("Cs Script Icon") as Texture2D;
        }
    }
}