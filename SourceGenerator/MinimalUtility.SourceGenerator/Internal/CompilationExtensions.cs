using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MinimalUtility.SourceGenerator.Internal;

internal static class CompilationExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<IMethodSymbol> SelectInvocationInfo(
        this Compilation compilation, CancellationToken cancellationToken = default)
    {
        foreach (var syntaxTree in compilation.SyntaxTrees)
        {
            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            foreach (var syntaxNode in syntaxTree.GetRoot().DescendantNodes())
            {
                if (syntaxNode is not InvocationExpressionSyntax invocationExpression)
                {
                    continue;
                }
                var symbolInfo = semanticModel.GetSymbolInfo(invocationExpression, cancellationToken).Symbol;
                if (symbolInfo is IMethodSymbol methodSymbol)
                {
                    yield return methodSymbol;
                }
            }
        }
    }
}