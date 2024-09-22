using Microsoft.CodeAnalysis;

namespace MinimalUtility.SourceGenerator;

internal static class DiagnosticDescriptors
{
    private const string XEnumGeneratorCategory = nameof(XEnumGenerator);

    public static readonly DiagnosticDescriptor ENUMGEN001 = new (
        id: nameof(ENUMGEN001),
        title: "Non public enum",
        messageFormat: "The XEnumGenerator target enum {0} is not public.",
        category: XEnumGeneratorCategory,
        DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}