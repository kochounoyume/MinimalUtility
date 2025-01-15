#if ENABLE_UGUI
#nullable enable

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace MinimalUtility.Editor.DataBind
{
    using MinimalUtility.DataBind;

    internal sealed class AddBindElementDropdown : AdvancedDropdown
    {
        private sealed class Item : AdvancedDropdownItem
        {
            public readonly Type type;

            public Item(string name, Type type) : base(name)
            {
                this.type = type;
            }
        }

        private static readonly (Type type, string path)[] s_cache = TypeCache.GetTypesDerivedFrom<BindElement>()
                .Where(static x => x != typeof(BindElement) && x is { IsAbstract: false, IsSpecialName: false, IsGenericType: false })
                .Select(static x =>
                {
                    var attribute = x.GetCustomAttribute<DataBindMenuAttribute>();
                    if (attribute == null)
                    {
                        throw new InvalidOperationException($"Type {x.Name} does not have {nameof(DataBindMenuAttribute)}.");
                    }
                    return (x, attribute.path);
                })
                .OrderBy(static x => x.path)
                .ToArray();

        private readonly IItemSelectHandler? _handler;

        public AddBindElementDropdown(AdvancedDropdownState state, IItemSelectHandler? handler = null) : base(state)
        {
            _handler = handler;

            var minSize = minimumSize;
            minSize.x = 200;
            minimumSize = minSize;
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Add Bind Element");
            foreach (var (type, path) in s_cache)
            {
                var splits = path.Split('/');
                var parent = root;

                foreach (var split in splits)
                {
                    var foundItem = parent.children.FirstOrDefault(x => x.name == split);
                    if (foundItem != null)
                    {
                        parent = foundItem;
                        continue;
                    }

                    var child = new Item(split, type)
                    {
                        icon = IconUtils.LoadUGUIIcon(split) ?? (parent == root ? IconUtils.LoadCsIcon() : null)
                    };
                    parent.AddChild(child);
                    parent = child;
                }
            }

            return root;
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            if (item is Item bindItem)
            {
                _handler?.OnItemSelected(bindItem.type);
            }
        }
    }
}

#endif