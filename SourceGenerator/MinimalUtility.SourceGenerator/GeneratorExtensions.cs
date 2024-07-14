using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace MinimalUtility.SourceGenerator;

internal static class GeneratorExtensions
{
    /// <summary>
    /// 列挙型の基底の型を文字列で取得します
    /// </summary>
    /// <param name="typeSymbol">列挙型の型情報</param>
    /// <returns>列挙型の基底の型</returns>
    /// <exception cref="InvalidOperationException">指定された型が無効な場合</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string GetEnumBaseTypeStr(this INamedTypeSymbol typeSymbol)
    {
        // 対象の列挙型の基底の型を取得
        var baseType = typeSymbol.EnumUnderlyingType;

        if (baseType == null) throw new InvalidOperationException("Invalid type specified");

        return baseType.Name switch
        {
            nameof(Int32) => "int",
            nameof(UInt32) => "uint",
            nameof(Int64) => "long",
            nameof(UInt64) => "ulong",
            nameof(Int16) => "short",
            nameof(UInt16) => "ushort",
            nameof(Byte) => "byte",
            nameof(SByte) => "sbyte",
            _ => baseType.Name
        };
    }

    /// <summary>
    /// 出力ファイルに利用できる型名を取得します
    /// </summary>
    /// <param name="typeSymbol">型情報</param>
    /// <returns>出力ファイルに利用できる型名</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string GetFullTypeName(this INamedTypeSymbol typeSymbol)
    {
        return typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
            .Replace("global::", "")
            .Replace("<", "_")
            .Replace(">", "_");
    }

    /// <summary>
    /// 入れ子クラスであれば親クラス名を含めた型名を取得します
    /// </summary>
    /// <param name="typeSymbol">型情報</param>
    /// <returns>入れ子クラスであれば親クラス名を含めた型名</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static string GetNestedName(this INamedTypeSymbol typeSymbol)
    {
        return typeSymbol.ContainingType is null
            ? typeSymbol.Name
            : $"{typeSymbol.ContainingType.GetNestedName()}.{typeSymbol.Name}";
    }
}