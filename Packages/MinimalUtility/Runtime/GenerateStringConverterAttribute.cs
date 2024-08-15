using System;

namespace MinimalUtility
{
    /// <summary>
    /// Enumの文字列変換クラスを自動生成するための属性. 以下のようなコードが自動生成されます.
    /// <code>
    /// <![CDATA[
    /// using System;
    ///
    /// namespace MinimalUtility.MultiLibraries
    /// {
    ///     public partial class MemoryUnitStringConverter
    ///     {
    ///         private readonly string[] names = new string[]
    ///         {
    ///             "B",
    ///             "KB",
    ///             "MB",
    ///             "GB",
    ///         };
    ///
    ///         public ReadOnlySpan<string> MemberNames => names;
    ///
    ///         public ReadOnlySpan<DebugProfiler.MemoryUnit> MemberValues
    ///             => new DebugProfiler.MemoryUnit[] { DebugProfiler.MemoryUnit.B, DebugProfiler.MemoryUnit.KB, DebugProfiler.MemoryUnit.MB, DebugProfiler.MemoryUnit.GB, };
    ///
    ///         public ref readonly string Convert(in DebugProfiler.MemoryUnit value)
    ///         {
    ///             switch ((int)value)
    ///             {
    ///                 case 0:
    ///                     return ref names[0];
    ///                 case 1:
    ///                     return ref names[1];
    ///                 case 2:
    ///                     return ref names[2];
    ///                 case 3:
    ///                     return ref names[3];
    ///                 default:
    ///                     return ref string.Empty;
    ///             }
    ///         }
    ///
    ///         public DebugProfiler.MemoryUnit ReverseConvert(in string name)
    ///         {
    ///             switch (Array.IndexOf(names, name))
    ///             {
    ///                 case 0:
    ///                     return DebugProfiler.MemoryUnit.B;
    ///                 case 1:
    ///                     return DebugProfiler.MemoryUnit.KB;
    ///                 case 2:
    ///                     return DebugProfiler.MemoryUnit.MB;
    ///                 case 3:
    ///                     return DebugProfiler.MemoryUnit.GB;
    ///                 default:
    ///                     throw new InvalidCastException(name);
    ///             }
    ///         }
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    public sealed class GenerateStringConverterAttribute : Attribute
    {
        private readonly bool autoGenerate;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateStringConverterAttribute"/> class.
        /// </summary>
        /// <param name="autoGenerate">trueの場合、列挙を列挙のプロパティ名と同じ文字列に変換するクラスを自動的に生成します.</param>
        public GenerateStringConverterAttribute(bool autoGenerate = false)
        {
            this.autoGenerate = autoGenerate;
        }
    }
}