﻿#if ENABLE_TEXTMESHPRO
#nullable enable

using System;
using MinimalUtility.UGUI;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor
{
    [CustomPropertyDrawer(typeof(TextView), true)]
    internal sealed class TextViewDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new Foldout();
            var foldoutCheck = root.Q(className: Foldout.checkmarkUssClassName);
            foldoutCheck.parent.Add(new Label(property.type)
            {
                style =
                {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    flexGrow = 1
                }
            });
            var objectField = new ObjectField()
            {
                bindingPath = "_textMeshProUGUI",
                objectType = typeof(TMPro.TextMeshProUGUI),
                style = { flexGrow = 1 }
            };
            objectField.Q(className: ObjectField.ussClassName + "-display__label").style.marginLeft = 2;
            foldoutCheck.parent.Add(objectField);

            var formatProperty = property.FindPropertyRelative("_format");
            var value = formatProperty.stringValue;
            var defaultValue = new FormatSettings.KeyValuePair { value = string.IsNullOrEmpty(value) ? "" : value };
            var options = FormatSettings.instance.GetFormats(property.type switch
            {
                nameof(IntTextView) => typeof(int),
                nameof(FloatTextView) => typeof(float),
                nameof(DateTimeTextView) => typeof(DateTime),
                nameof(TimeSpanTextView) => typeof(TimeSpan),
                _ => throw new NotImplementedException(property.type)
            });
            var popupField = new PopupField<FormatSettings.KeyValuePair>("Format", options, defaultValue)
            {
                style = { marginBottom = 3 },
            };
            popupField.RegisterCallback<ChangeEvent<FormatSettings.KeyValuePair>, SerializedProperty>(static (evt, property) =>
            {
                property.stringValue = evt.newValue.value;
                property.serializedObject.ApplyModifiedProperties();
            }, formatProperty);
            root.Add(popupField);

            var range = property.FindPropertyRelative("_range");
            if (range != null)
            {
                switch (range.propertyType)
                {
                    case SerializedPropertyType.Vector2:
                        root.Add(new FloatField("Min")
                        {
                            bindingPath = "_range.x",
                            style = { marginBottom = 3 }
                        });
                        root.Add(new FloatField("Max")
                        {
                            bindingPath = "_range.y",
                            style = { marginBottom = 3 }
                        });
                        break;
                    case SerializedPropertyType.Vector2Int:
                        root.Add(new IntegerField("Min")
                        {
                            bindingPath = "_range.x",
                            style = { marginBottom = 3 }
                        });
                        root.Add(new IntegerField("Max")
                        {
                            bindingPath = "_range.y",
                            style = { marginBottom = 3 }
                        });
                        break;
                    default:
                        throw new NotImplementedException(range.propertyType.ToString());
                }
            }

            return root;
        }
    }
}

#endif