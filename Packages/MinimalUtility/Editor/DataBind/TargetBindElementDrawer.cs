﻿#nullable enable

using System.Reflection;
using MinimalUtility.DataBind;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor.DataBind
{
    [CustomPropertyDrawer(typeof(TargetBindElement<>), true)]
    public class TargetBindElementDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var type = property.managedReferenceValue.GetType();
            var attr = type.GetCustomAttribute<DataBindMenuAttribute>();
            if (attr == null)
            {
                throw new System.InvalidOperationException($"Type {property.managedReferenceValue.GetType().Name} does not have {nameof(DataBindMenuAttribute)}.");
            }
            var splits = attr.path.Split('/');
            var genericType = DataBindUtils.GetTargetType(type);

            var root = new Foldout { focusable = true };
            var foldoutCheck = root.Q(className: Foldout.checkmarkUssClassName);
            foldoutCheck.parent.Add(new Image()
            {
                image = DataBindUtils.LoadMiniTypeThumbnail(genericType),
                style =
                {
                    width = 18f,
                    height = 18f,
                }
            });
            foldoutCheck.parent.Add(new Label(genericType.Name + " - " + splits[^1])
            {
                style =
                {
                    marginLeft = 5f,
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 13,
                }
            });
            var removeBtn = new Button()
            {
                style =
                {
                    height = 15f,
                    width = 15f,
                    top = 4f,
                    right = 4f,
                    position = Position.Absolute,
                    backgroundImage = DataBindUtils.removeIcon,
                    backgroundColor = Color.white
                }
            };
            removeBtn.RegisterCallback<ClickEvent, SerializedProperty>(static (_, prop) =>
            {
                prop.DeleteCommand();
                prop.serializedObject.ApplyModifiedProperties();
            }, property);
            foldoutCheck.parent.Add(removeBtn);
            root.Add(new VisualElement()
            {
                style =
                {
                    height = 2,
                    backgroundColor = Color.gray,
                    marginBottom = 5,
                }
            });
            root.Add(new PropertyField(property.FindPropertyRelative("_target")));
            root.Add(new PropertyField(property.FindPropertyRelative("_propertyName")));

            // CustomPropertyDrawerAttribute.useForChildrenがバグってなければ、別クラス化したい
            var optionProperty = property.FindPropertyRelative("_option");
            if (optionProperty != null)
            {
                root.Add(new PropertyField(optionProperty));
            }

            return root;
        }
    }
}