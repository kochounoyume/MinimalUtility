using System.Text;
using Microsoft.CodeAnalysis;

namespace MinimalUtility.SourceGenerator;

[Generator(LanguageNames.CSharp)]
internal sealed class EnumStringConverterGenerator : IIncrementalGenerator
{
    private const string AttributeName = "MinimalUtility.GenerateStringConverterAttribute";

    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        var source = context.SyntaxProvider.ForAttributeWithMetadataName(
            AttributeName,
            static (_, _) => true,
            static (context, _) => context
        ).Collect();

        context.RegisterSourceOutput(
            source,
            static (context, metaDataArray) =>
            {
                foreach (var syntaxContext in metaDataArray)
                {
                    var typeSymbol = (INamedTypeSymbol)syntaxContext.TargetSymbol;
                    string targetTypeName = typeSymbol.Name;
                    string targetNestedTypeName = typeSymbol.GetNestedName();
                    string baseType = typeSymbol.GetEnumBaseTypeStr();
                    string fullType = typeSymbol.GetFullTypeName();
                    bool isGlobalNamespace = typeSymbol.ContainingNamespace.IsGlobalNamespace;

                    bool autoGenerate = syntaxContext.Attributes[0].ConstructorArguments[0].Value?.ToString() == "True";

                    List<string> values = new ();
                    List<string> fieldNames = new ();
                    StringBuilder builder = new ();

                    builder.Append(isGlobalNamespace ?
                        $$"""
                          using System;

                          public partial class {{targetTypeName}}StringConverter
                          {
                              private readonly string[] names = new string[]
                              {
                          """ :
                        $$"""
                          using System;

                          namespace {{typeSymbol.ContainingNamespace}}
                          {
                              public partial class {{targetTypeName}}StringConverter
                              {
                                  private readonly string[] names = new string[]
                                  {
                          """
                    );
                    builder.AppendLine();

                    foreach (ISymbol member in typeSymbol.GetMembers())
                    {
                        bool shouldAutoGenerate = autoGenerate && member.Kind == SymbolKind.Field;
                        foreach (AttributeData attribute in member.GetAttributes())
                        {
                            if (attribute.AttributeClass?.Name != nameof(System.Runtime.Serialization.EnumMemberAttribute)) continue;
                            foreach (KeyValuePair<string, TypedConstant> argument in attribute.NamedArguments)
                            {
                                if (argument.Key != "Value") continue;
                                string? enumMemberValue = argument.Value.Value?.ToString();
                                if (enumMemberValue is null) continue;
                                AddMember(builder, isGlobalNamespace, enumMemberValue, member, values, fieldNames);
                                shouldAutoGenerate = false;
                            }
                        }

                        if (shouldAutoGenerate)
                        {
                            AddMember(builder, isGlobalNamespace, member.Name, member, values, fieldNames);
                        }
                    }

                    values.TrimExcess();
                    fieldNames.TrimExcess();

                    builder.AppendLine(isGlobalNamespace ? "    };" : "        };");
                    builder.AppendLine();
                    builder.Append(isGlobalNamespace
                        ? $$"""
                                public ReadOnlySpan<string> MemberNames => names;
                                
                                public ReadOnlySpan<{{targetNestedTypeName}}> MemberValues
                                    => new {{targetNestedTypeName}}[] {
                            """
                        : $$"""
                                    public ReadOnlySpan<string> MemberNames => names;
                                    
                                    public ReadOnlySpan<{{targetNestedTypeName}}> MemberValues
                                        => new {{targetNestedTypeName}}[] {
                            """
                    );
                    builder.Append(" ");

                    foreach (string fieldName in fieldNames)
                    {
                        builder.Append($"{targetNestedTypeName}.{fieldName}, ");
                    }
                    builder.AppendLine("};");

                    builder.AppendLine();
                    builder.AppendLine(isGlobalNamespace
                        ? $$"""
                                public ref readonly string Convert(in {{targetNestedTypeName}} value)
                                {
                                    switch (({{baseType}})value)
                                    {
                            """
                        : $$"""
                                    public ref readonly string Convert(in {{targetNestedTypeName}} value)
                                    {
                                        switch (({{baseType}})value)
                                        {
                            """
                    );

                    for (int i = 0; i < values.Count; i++)
                    {
                        builder.AppendLine(isGlobalNamespace
                            ? $"""
                                           case {values[i]}:
                                               return ref names[{i}];
                               """
                            : $"""
                                               case {values[i]}:
                                                   return ref names[{i}];
                               """
                        );
                    }

                    values.Clear();

                    builder.AppendLine(isGlobalNamespace
                        ? $$"""
                                        default:
                                            return ref string.Empty;
                                    }
                                }
                                
                                public {{targetNestedTypeName}} ReverseConvert(in string name)
                                {
                                    switch (Array.IndexOf(names, name))
                                    {
                            """
                        : $$"""
                                            default:
                                                return ref string.Empty;
                                        }
                                    }
                                    
                                    public {{targetNestedTypeName}} ReverseConvert(in string name)
                                    {
                                        switch (Array.IndexOf(names, name))
                                        {
                            """
                    );

                    for (int i = 0; i < fieldNames.Count; i++)
                    {
                        builder.AppendLine(isGlobalNamespace
                            ? $"""
                                           case {i}:
                                               return {targetNestedTypeName}.{fieldNames[i]};
                               """
                            : $"""
                                               case {i}:
                                                   return {targetNestedTypeName}.{fieldNames[i]};
                               """
                        );
                    }

                    fieldNames.Clear();

                    builder.Append(isGlobalNamespace
                        ? """
                                      default:
                                          throw new InvalidCastException(name);
                                  }
                              }
                          }
                          """
                        : """
                                          default:
                                              throw new InvalidCastException(name);
                                      }
                                  }
                              }
                          }
                          """);

                    context.AddSource($"{fullType}.StringConverter.g.cs", builder.ToString());
                }
            });

        static void AddMember(
            StringBuilder builder,
            bool isGlobalNamespace,
            string elementValue,
            ISymbol member,
            ICollection<string> values,
            ICollection<string> fieldNames)
        {
            builder.Append(isGlobalNamespace ? "        \"" : "            \"");
            builder.Append(elementValue);
            builder.AppendLine("\",");

            if (member is not IFieldSymbol fieldSymbol) return;
            string? value = fieldSymbol.ConstantValue?.ToString();
            if (value is not null)
            {
                values.Add(value);
            }
            fieldNames.Add(member.Name);
        }
    }
}