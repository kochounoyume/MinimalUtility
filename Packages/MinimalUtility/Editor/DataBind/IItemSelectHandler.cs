namespace MinimalUtility.Editor.DataBind
{
    internal interface IItemSelectHandler
    {
        bool AlreadyExist(System.Type type);

        void OnItemSelected(System.Type type);
    }
}