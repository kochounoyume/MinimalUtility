﻿using System.Text;
using Microsoft.CodeAnalysis;

namespace MinimalUtility.SourceGenerator;

[Generator(LanguageNames.CSharp)]
public sealed class EnumStringConverterGenerator : IIncrementalGenerator
{
    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        // 属性クラスの出力
        context.RegisterPostInitializationOutput(static context =>
        {
            context.AddSource("GenerateStringConverterAttribute.g.cs", """
            using System;

            namespace MinimalUtility.SourceGenerator
            {
                [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
                internal sealed class GenerateStringConverterAttribute : Attribute
                {
                }
            }
            """);
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

                bool isGlobalNamespace = typeSymbol.ContainingNamespace.IsGlobalNamespace;

                List<string> values = new ();
                List<string> fieldNames = new ();
                StringBuilder builder = new ();

                builder.Append(isGlobalNamespace ?
                    $$"""
                    using System;
                    
                    public partial class {{targetTypeName}}StringConverter
                    {
                        private readonly string[] values = new string[]
                        {
                    """ :
                    $$"""
                    using System;
                    
                    namespace {{typeSymbol.ContainingNamespace}}
                    {
                        public partial class {{targetTypeName}}StringConverter
                        {
                            private readonly string[] values = new string[]
                            {
                    """);
                builder.AppendLine();

                foreach (ISymbol member in typeSymbol.GetMembers())
                {
                    foreach (AttributeData attribute in member.GetAttributes())
                    {
                        if (attribute.AttributeClass?.Name != nameof(System.Runtime.Serialization.EnumMemberAttribute)) continue;
                        foreach (KeyValuePair<string, TypedConstant> argument in attribute.NamedArguments)
                        {
                            if (argument.Key != "Value") continue;
                            string? enumMemberValue = argument.Value.Value?.ToString();
                            if (enumMemberValue is null) continue;
                            builder.Append(isGlobalNamespace ? "        \"" : "            \"");
                            builder.Append(enumMemberValue);
                            builder.AppendLine("\",");

                            if (member is not IFieldSymbol fieldSymbol) continue;
                            string? value = fieldSymbol.ConstantValue?.ToString();
                            if (value is not null)
                            {
                                values.Add(value);
                            }
                            fieldNames.Add(member.Name);
                        }
                    }
                }

                values.TrimExcess();
                fieldNames.TrimExcess();

                builder.AppendLine(isGlobalNamespace ? "    };" : "        };");
                builder.AppendLine();
                builder.AppendLine(isGlobalNamespace ? $$"""
                        public ReadOnlySpan<string> MemberValues => values;
                    
                        public ref readonly string Convert(in {{targetTypeName}} value)
                        {
                            switch (({{baseType}})value)
                            {
                    """ :
                    $$"""
                            public ReadOnlySpan<string> MemberValues => values;
                    
                            public ref readonly string Convert(in {{targetTypeName}} value)
                            {
                                switch (({{baseType}})value)
                                {
                    """);

                for (int i = 0; i < values.Count; i++)
                {
                    builder.AppendLine(isGlobalNamespace ? $"""
                                    case {values[i]}:
                                        return ref values[{i}];
                        """ :
                        $"""
                                        case {values[i]}:
                                            return ref values[{i}];
                        """);
                }

                values.Clear();

                builder.AppendLine(isGlobalNamespace ? $$"""
                                default:
                                    return ref string.Empty;
                            }
                        }
                        
                        public {{targetTypeName}} ReverseConvert(in string str)
                        {
                            switch (Array.IndexOf(values, str))
                            {
                    """ :
                    $$"""
                                    default:
                                        return ref string.Empty;
                                }
                            }
                            
                            public {{targetTypeName}} ReverseConvert(in string str)
                            {
                                switch (Array.IndexOf(values, str))
                                {
                    """);

                for (int i = 0; i < fieldNames.Count; i++)
                {
                    builder.AppendLine(isGlobalNamespace ? $"""
                                    case {i}:
                                        return {targetTypeName}.{fieldNames[i]};
                        """ :
                        $"""
                                        case {i}:
                                            return {targetTypeName}.{fieldNames[i]};
                        """);
                }

                fieldNames.Clear();

                builder.Append(isGlobalNamespace ? """
                                default:
                                    throw new InvalidCastException(str);
                            }
                        }
                    }
                    """ :
                    """
                                    default:
                                        throw new InvalidCastException(str);
                                }
                            }
                        }
                    }
                    """);

                context.AddSource($"{fullType}.StringConverter.g.cs", builder.ToString());
            });
    }
}