using System;

namespace MinimalUtility
{
    /// <summary>
    /// Enumの文字列変換クラスを自動生成するための属性. 以下のようなコードが自動生成されます.
    /// <code>
    /// <![CDATA[
    /// using System;
    ///
    /// namespace MinimalUtility
    /// {
    ///     public partial class MemoryUnitStringConverter
    ///     {
    ///         private readonly string[] values = new string[]
    ///         {
    ///             "B",
    ///             "KB",
    ///             "MB",
    ///             "GB",
    ///         };
    ///
    ///         public ReadOnlySpan<string> MemberValues => values;
    ///
    ///         public ref readonly string Convert(in DebugProfiler.MemoryUnit value)
    ///         {
    ///             switch ((int)value)
    ///             {
    ///                 case 0:
    ///                     return ref values[0];
    ///                 case 1:
    ///                     return ref values[1];
    ///                 case 2:
    ///                     return ref values[2];
    ///                 case 3:
    ///                     return ref values[3];
    ///                 default:
    ///                     return ref string.Empty;
    ///             }
    ///         }
    ///
    ///         public DebugProfiler.MemoryUnit ReverseConvert(in string str)
    ///         {
    ///             switch (Array.IndexOf(values, str))
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
    ///                     throw new InvalidCastException(str);
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