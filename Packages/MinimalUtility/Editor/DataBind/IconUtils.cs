#if ENABLE_UGUI
#nullable enable

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace MinimalUtility.Editor.DataBind
{
    internal static class IconUtils
    {
        public static Texture2D? LoadRemoveIcon()
        {
            return EditorGUIUtility.Load("Toolbar Minus") as Texture2D;
        }

        public static Texture2D? LoadCsIcon()
        {
            return EditorGUIUtility.Load("Cs Script Icon") as Texture2D;
        }

        public static Texture2D? LoadUGUIIcon(string iconName)
        {
            return iconName switch
            {
                nameof(Button) => AssetPreview.GetMiniTypeThumbnail(typeof(Button)),
                nameof(Dropdown) => AssetPreview.GetMiniTypeThumbnail(typeof(Dropdown)),
                nameof(Image) => AssetPreview.GetMiniTypeThumbnail(typeof(Image)),
                nameof(RawImage) => AssetPreview.GetMiniTypeThumbnail(typeof(RawImage)),
                nameof(Scrollbar) => AssetPreview.GetMiniTypeThumbnail(typeof(Scrollbar)),
                nameof(ScrollRect) => AssetPreview.GetMiniTypeThumbnail(typeof(ScrollRect)),
                nameof(Slider) => AssetPreview.GetMiniTypeThumbnail(typeof(Slider)),
                nameof(Toggle) => AssetPreview.GetMiniTypeThumbnail(typeof(Toggle)),
#if ENABLE_TEXTMESHPRO
                nameof(TMPro.TextMeshProUGUI) or nameof(TMPro.TMP_InputField)
#if UNITY_2023_2_OR_NEWER
                    => AssetDatabase.LoadAssetAtPath<Texture2D>(
                        "Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Text Component Icon.psd"),
#else
                    => AssetDatabase.LoadAssetAtPath<Texture2D>(
                        "Packages/com.unity.textmeshpro/Editor Resources/Gizmos/TMP - Text Component Icon.psd"),
#endif
#endif
                _ => null
            };
        }

        public static Texture2D? LoadUGUIIcon(in ReadOnlySpan<string> iconNames)
        {
            foreach (var iconName in iconNames)
            {
                var icon = LoadUGUIIcon(iconName);
                if (icon != null)
                {
                    return icon;
                }
            }
            return null;
        }
    }
}

#endif