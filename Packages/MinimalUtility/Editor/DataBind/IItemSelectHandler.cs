#if ENABLE_UGUI
#nullable enable

namespace MinimalUtility.Editor.DataBind
{
    internal interface IItemSelectHandler
    {
        void OnItemSelected(System.Type type);
    }
}

#endif