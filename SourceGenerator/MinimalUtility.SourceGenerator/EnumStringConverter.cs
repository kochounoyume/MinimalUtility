using Microsoft.CodeAnalysis;

namespace MinimalUtility.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public sealed class EnumStringConverter : IIncrementalGenerator
{
    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        // 属性クラスの出力
        context.RegisterPostInitializationOutput(static context =>
        {
            context.AddSource("GenerateStringConverterAttribute.g.cs", @"
            using System;

            namespace MinimalUtility.SourceGenerator
            {
                [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
                internal sealed class GenerateStringConverterAttribute : Attribute
                {
                }
            }
            ");
        });

        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            "MinimalUtility.SourceGenerator.GenerateStringConverterAttribute",
            static (_, _) => true,
            static (context, _) => context
        );

        context.RegisterSourceOutput(
            source,
            static (context, metaDataArray) =>
            {
                var typeSymbol = (INamedTypeSymbol)metaDataArray.TargetSymbol;

                string targetTypeName = typeSymbol.Name;

                string baseType = typeSymbol.GetEnumBaseTypeStr();

                string fullType = typeSymbol.GetFullTypeName();
            });
    }
}