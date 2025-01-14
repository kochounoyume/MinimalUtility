using System;

namespace MinimalUtility.DataBind
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class DataBindMenuAttribute : Attribute
    {
        public string path { get; }

        public DataBindMenuAttribute(string path)
        {
            this.path = path;
        }
    }
}