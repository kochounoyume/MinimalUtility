using Microsoft.CodeAnalysis;

namespace MinimalUtility.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public sealed class EnumEqualityComparerGenerator : IIncrementalGenerator
{
    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        // 属性クラスの出力
        context.RegisterPostInitializationOutput(static context =>
        {
            context.AddSource("GenerateEqualityComparerAttribute.g.cs", """
            using System;

            namespace MinimalUtility.SourceGenerator
            {
                [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
                internal sealed class GenerateEqualityComparerAttribute : Attribute
                {
                }
            }
            """);
        });

        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            "MinimalUtility.SourceGenerator.GenerateEqualityComparerAttribute",
            static (_, _) => true,
            static (context, _) => context
        );

        context.RegisterSourceOutput(
            source,
            static (context, metaDataArray) =>
            {
                var typeSymbol = (INamedTypeSymbol)metaDataArray.TargetSymbol;

                // 対象の列挙型の基底の型を取得
                var baseType = typeSymbol.EnumUnderlyingType;

                if (baseType == null) return;

                string targetTypeName = typeSymbol.Name;

                string baseTypeString = baseType.Name switch
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

                // 出力ファイル名に利用するためにエスケープ
                var fullType = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                    .Replace("global::", "")
                    .Replace("<", "_")
                    .Replace(">", "_");

                context.AddSource($"{fullType}.EqualityComparer.g.cs",
                    typeSymbol.ContainingNamespace.IsGlobalNamespace
                        ? $$"""
                          using System.Collections.Generic;
                                                                                             
                          public readonly struct {{targetTypeName}}EqualityComparer : IEqualityComparer<{{targetTypeName}}>
                          {
                              public bool Equals({{targetTypeName}} x, {{targetTypeName}} y) => ({{baseTypeString}})x == ({{baseTypeString}})y;
                          
                              public int GetHashCode({{targetTypeName}} obj) => (({{baseTypeString}})obj).GetHashCode();
                          }
                        """
                        : $$"""
                          using System.Collections.Generic;
                                                                                             
                          namespace {{typeSymbol.ContainingNamespace}}
                          {
                              public readonly struct {{targetTypeName}}EqualityComparer : IEqualityComparer<{{targetTypeName}}>
                              {
                                  public bool Equals({{targetTypeName}} x, {{targetTypeName}} y) => ({{baseTypeString}})x == ({{baseTypeString}})y;
                              
                                  public int GetHashCode({{targetTypeName}} obj) => (({{baseTypeString}})obj).GetHashCode();
                              }
                          }
                        """);
            });
    }
}