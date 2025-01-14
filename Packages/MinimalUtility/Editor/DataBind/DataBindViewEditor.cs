#if ENABLE_UGUI
#nullable enable
using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor.DataBind
{
    using MinimalUtility.DataBind;

    [CustomEditor(typeof(DataBindView))]
    internal sealed class DataBindViewEditor : UnityEditor.Editor, IItemSelectHandler
    {
        private SerializedProperty? _bindElementsProperty;
        private AddBindElementDropdown? _addBindElementDropdown;

        public override VisualElement CreateInspectorGUI()
        {
            _bindElementsProperty = serializedObject.FindProperty("_elements");
            _addBindElementDropdown = new AddBindElementDropdown(new AdvancedDropdownState(), this);

            var box = CreateBox("Bind Elements");

            //var field = new PropertyField(_bindElementsProperty);
            //box.Add(field);

            var addButton = new Button()
            {
                text = "Add Element",
                style =
                {
                    width = 200f,
                    alignSelf = Align.Center
                }
            };
            addButton.RegisterCallback<ClickEvent, AddBindElementDropdown>(static (eve, dropdown) =>
            {
                var button = (Button)eve.target;
                dropdown.Show(button.worldBound);
            }, _addBindElementDropdown);
            box.Add(addButton);

            var root = new VisualElement();
            root.Add(box);
            return root;
        }

        bool IItemSelectHandler.AlreadyExist(Type type)
        {
            if (_bindElementsProperty == null) return false;
            var size = _bindElementsProperty.arraySize;
            for (var i = 0; i < size; i++)
            {
                var property = _bindElementsProperty.GetArrayElementAtIndex(i);
                if (EqualTypeName(property.managedReferenceFullTypename, type.FullName))
                {
                    return true;
                }
            }
            return false;

            static bool EqualTypeName(in ReadOnlySpan<char> managedReferenceFullTypename, in ReadOnlySpan<char> fullName)
            {
                var index = managedReferenceFullTypename.IndexOf(' ');
                var managed = managedReferenceFullTypename[(index + 1)..];
                var span = (Span<char>)stackalloc char[managed.Length];
                managed.CopyTo(span);
                while (span.IndexOf('/') is var i && i != -1)
                {
                    span[i] = '.';
                }
                return span.SequenceEqual(fullName);
            }
        }

        void IItemSelectHandler.OnItemSelected(Type type)
        {
            if (_bindElementsProperty == null) return;
            var last = _bindElementsProperty.arraySize;
            _bindElementsProperty.InsertArrayElementAtIndex(_bindElementsProperty.arraySize);
            var property = _bindElementsProperty.GetArrayElementAtIndex(last);
            var constructor = type.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
                null, Type.EmptyTypes, null);
            property.managedReferenceValue = constructor?.Invoke(null);
            serializedObject.ApplyModifiedProperties();
        }

        private static VisualElement CreateBox(string label)
        {
            var box = new Box
            {
                style = {
                    marginTop = 6f,
                    marginBottom = 2f,
                    paddingLeft = 4f,
                    alignItems = Align.Stretch,
                    flexDirection = FlexDirection.Column,
                    flexGrow = 1f,
                }
            };
            box.Add(new Label(label)
            {
                style =
                {
                    marginTop = 5f,
                    marginBottom = 3f,
                    unityFontStyleAndWeight = FontStyle.Bold
                }
            });
            return box;
        }
    }
}

#endif