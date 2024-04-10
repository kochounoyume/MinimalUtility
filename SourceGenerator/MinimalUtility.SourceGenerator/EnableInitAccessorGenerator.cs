using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MinimalUtility.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public sealed class EnableInitAccessorGenerator : IIncrementalGenerator
{
    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        // System.Runtime.CompilerServices.IsExternalInitクラスがあるかどうかをチェック
        var source = context.SyntaxProvider.CreateSyntaxProvider(
                static (node, _) => node is ClassDeclarationSyntax {Identifier.ValueText: "IsExternalInit"},
                static (context, _) => (ClassDeclarationSyntax) context.Node)
            .Where(static m => m is not null)
            .Collect();

        context.RegisterSourceOutput(
            source,
            static (context, metaDataArray) =>
            {
                if (!metaDataArray.Any())
                {
                    context.AddSource("IsExternalInit.g.cs", """
                    #if !NET5_0_OR_GREATER
                    namespace System.Runtime.CompilerServices
                    {
                        internal class IsExternalInit
                        {
                        }
                    }
                    #endif                                         
                    """);
                }
            });
    }
}