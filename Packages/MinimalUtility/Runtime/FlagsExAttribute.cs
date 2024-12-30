#nullable enable

namespace MinimalUtility
{
    /// <summary>
    /// <see cref="System.FlagsAttribute"/>の拡張版.
    /// </summary>
    /// <remarks>
    /// 以下のようなコードを自動生成して、foreach文で保持フラグの列挙を可能にする.その必要がなければ、<see cref="System.FlagsAttribute"/>を使用推奨.
    /// </remarks>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// using System.Runtime.CompilerServices;
    ///
    /// namespace MinimalUtility
    /// {
    ///     internal static class HogeExtensions
    ///     {
    ///
    ///         [MethodImpl(MethodImplOptions.AggressiveInlining)]
    ///         public static Enumerator GetEnumerator(this Hoge value)
    ///         {
    ///             return new Enumerator(value);
    ///         }
    ///
    ///         public struct Enumerator
    ///         {
    ///             private Hoge value;
    ///
    ///             public Hoge Current { get; private set; }
    ///
    ///             internal Enumerator(Hoge value)
    ///             {
    ///                 this.value = value;
    ///                 Current = default;
    ///             }
    ///
    ///             public bool MoveNext()
    ///             {
    ///                 var flags = (int)value;
    ///                 if (flags == 0) return false;
    ///                 Current = (Hoge)(flags & -flags); // get lowest flag
    ///                 value &= ~Current;
    ///                 return true;
    ///             }
    ///         }
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public sealed class FlagsExAttribute : System.FlagsAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlagsExAttribute"/> class.
        /// </summary>
        public FlagsExAttribute() : base()
        {
        }
    }
}
