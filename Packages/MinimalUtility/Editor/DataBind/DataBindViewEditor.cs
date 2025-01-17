#nullable enable

using System;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UIElements;

namespace MinimalUtility.Editor.DataBind
{
    using MinimalUtility.DataBind;

    [CustomEditor(typeof(DataBindView))]
    internal sealed class DataBindViewEditor : UnityEditor.Editor, IItemSelectHandler
    {
        private SerializedProperty? _bindElementsProperty;

        public override VisualElement CreateInspectorGUI()
        {
            _bindElementsProperty = serializedObject.FindProperty("_elements");

            var root = new VisualElement();
            root.Add(CreateBindElementsField(new AddBindElementDropdown(new AdvancedDropdownState(), this)));
            return root;
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

        private static VisualElement CreateBindElementsField(AddBindElementDropdown addBindElementDropdown)
        {
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
            }, addBindElementDropdown);

            var box = CreateBox("Bind Elements");
            box.Add(new ListView
            {
                bindingPath = "_elements",
                reorderMode = ListViewReorderMode.Animated,
                showBorder = true,
                showAddRemoveFooter = false,
                showFoldoutHeader = false,
                showBoundCollectionSize = false,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                selectionType = SelectionType.None
            });
            box.Add(addButton);
            return box;
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