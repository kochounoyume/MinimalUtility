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

                string targetTypeName = typeSymbol.Name;

                string baseTypeString = typeSymbol.GetEnumBaseTypeStr();

                // 出力ファイル名に利用するためにエスケープ
                string fullType = typeSymbol.GetFullTypeName();

                context.AddSource($"{fullType}.EqualityComparer.g.cs",
                    typeSymbol.ContainingNamespace.IsGlobalNamespace
                        ? $$"""
                          using System.Collections.Generic;
                                                                                             
                          public sealed record {{targetTypeName}}EqualityComparer : IEqualityComparer<{{targetTypeName}}>
                          {
                              public bool Equals({{targetTypeName}} x, {{targetTypeName}} y) => ({{baseTypeString}})x == ({{baseTypeString}})y;
                          
                              public int GetHashCode({{targetTypeName}} obj) => (({{baseTypeString}})obj).GetHashCode();
                          }
                        """
                        : $$"""
                          using System.Collections.Generic;
                                                                                             
                          namespace {{typeSymbol.ContainingNamespace}}
                          {
                              public sealed record {{targetTypeName}}EqualityComparer : IEqualityComparer<{{targetTypeName}}>
                              {
                                  public bool Equals({{targetTypeName}} x, {{targetTypeName}} y) => ({{baseTypeString}})x == ({{baseTypeString}})y;
                              
                                  public int GetHashCode({{targetTypeName}} obj) => (({{baseTypeString}})obj).GetHashCode();
                              }
                          }
                        """);
            });
    }
}