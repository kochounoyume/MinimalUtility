using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using MinimalUtility.SourceGenerator.Internal;

namespace MinimalUtility.SourceGenerator;

[Generator(LanguageNames.CSharp)]
internal sealed class XEnumGenerator : IIncrementalGenerator
{
    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(RegisterBasisClass);
        var methods = context.CompilationProvider
            .Select(static (compilation, token) =>
            {
                token.ThrowIfCancellationRequested();
                return compilation.SelectInvocationInfo(token);
            })
            .SelectMany(static (method, token) =>
            {
                token.ThrowIfCancellationRequested();
                return method;
            })
            .Where(static info => info.method.Name is "GetNames" or "GetValues" or "GetName" or "IsDefined" or "Parse" or "TryParse")
            .Collect();
        context.RegisterSourceOutput(methods, RegisterCoreImplementation);
    }

    private static void RegisterBasisClass(IncrementalGeneratorPostInitializationContext context)
    {
        context.CancellationToken.ThrowIfCancellationRequested();
        context.AddSource("XEnum.g.cs", """
        using System;
        
        namespace MinimalUtility
        {
            internal static partial class XEnum
            {
                public static T[] GetValues<T>() where T : struct, Enum => Cache<T>.Default.GetValues();
                public static string[] GetNames<T>() where T : struct, Enum => Cache<T>.Default.GetNames();
                public static string GetName<T>(in T value) where T : struct, Enum => Cache<T>.Default.GetName(value);
                public static bool IsDefined<T, TValue>(in TValue value) where T : struct, Enum where TValue : struct => Cache<T>.Default.IsDefined(value);
                public static T Parse<T>(in string value) where T : struct, Enum => Cache<T>.Default.Parse(value);
                public static bool TryParse<T>(in string value, out T result) where T : struct, Enum => Cache<T>.Default.TryParse(value, out result);
            
                public static string ToXEnumString<T>(this T value) where T : struct, Enum => Cache<T>.Default.GetName(value);
            
                private abstract partial class Cache<T> where T : struct, Enum
                {
                    public static readonly Cache<T> Default;
                    public abstract T[] GetValues();
                    public abstract string[] GetNames();
                    public abstract string GetName(in T value);
                    public abstract bool IsDefined<TValue>(in TValue value) where TValue : struct;
                    public abstract T Parse(in string value);
                    public abstract bool TryParse(in string value, out T result);
                }
            }
        }
        """);
    }

    private static void RegisterCoreImplementation(SourceProductionContext context,
        ImmutableArray<(IMethodSymbol method, Location location)> metaDataArray)
    {
        context.CancellationToken.ThrowIfCancellationRequested();
        if (metaDataArray.IsEmpty) return;

        var mainSb = new StringBuilder(
        """
        using System;
        
        namespace MinimalUtility
        {
            internal static partial class XEnum
            {
        
        """);

        var ctorSb = new StringBuilder(
        """
        
                private abstract partial class Cache<T>
                {
                    static Cache()
                    {
                        
        """);

        var cacheSb = new StringBuilder();

        foreach (var (method, location) in metaDataArray)
        {
            if (method.ContainingType.ToDisplayString() == "MinimalUtility.XEnum.Cache<T>") continue;
            if (!method.IsGenericMethod) continue;
            var genericSymbol = method.TypeArguments[0];
            if (genericSymbol.DeclaredAccessibility != Accessibility.Public)
            {
                var diagnostic = Diagnostic.Create(DiagnosticDescriptors.ENUMGEN001, location, genericSymbol.GetFullTypeName());
                context.ReportDiagnostic(diagnostic);
                continue;
            }
            var typeName = genericSymbol.Name;
            var typeFullName = genericSymbol.GetFullTypeName();

            // private static readonly RuntimeTypeHandle HogeType = typeof(Hoge).TypeHandle;
            mainSb.Append("        private static readonly RuntimeTypeHandle ");
            mainSb.Append(typeName);
            mainSb.Append("Type = typeof(");
            mainSb.Append(typeFullName);
            mainSb.AppendLine(").TypeHandle;");

            // if (typeof(T).TypeHandle.Equals(HogeType))
            // {
            //     Default = new HogeCache() as Cache<T>;
            // } else
            ctorSb.Append("if (typeof(T).TypeHandle.Equals(");
            ctorSb.Append(typeName);
            ctorSb.Append("""
                          Type))
                                          {
                                              Default = new 
                          """);
            ctorSb.Append(typeName);
            ctorSb.Append("""
                              Cache() as Cache<T>;
                                              } else 
                              """);

            // private sealed class HogeCache : Cache<Hoge>
            // {
            //     public override Hoge[] GetValues() => new [] { Hoge.A, Hoge.B, Hoge.C };
            //     public override string[] GetNames() => new [] { nameof(Hoge.A), nameof(Hoge.B), nameof(Hoge.C) };
            //     public override string GetName(in Hoge value) => (int)value switch
            //     {
            //         0 => nameof(Hoge.A),
            //         1 => nameof(Hoge.B),
            //         2 => nameof(Hoge.C),
            //         _ => throw new ArgumentOutOfRangeException(nameof(value), value, default)
            //     };
            //     public override bool IsDefined<TValue>(in TValue value) => value is 0 or 1 or 2;
            //     public override Hoge Parse(in string value) => value switch
            //     {
            //         nameof(Hoge.A) => Hoge.A,
            //         nameof(Hoge.B) => Hoge.B,
            //         nameof(Hoge.C) => Hoge.C,
            //         _ => throw new ArgumentOutOfRangeException(nameof(value), value, default)
            //     };
            //     public override bool TryParse(in string value, out Hoge result)
            //     {
            //         switch (value)
            //         {
            //             case nameof(Hoge.A):
            //                 result = Hoge.A;
            //                 return true;
            //             case nameof(Hoge.B):
            //                 result = Hoge.B;
            //                 return true;
            //             case nameof(Hoge.C):
            //                 result = Hoge.C;
            //                 return true;
            //             default:
            //                 result = default;
            //                 return false;
            //         }
            //     }
            // }
            var valuesSb = new StringBuilder();
            var namesSb = new StringBuilder();
            var getNameSb = new StringBuilder();
            var isDefinedSb = new StringBuilder();
            var parseSb = new StringBuilder();
            var tryParseSb = new StringBuilder();

            foreach (var member in genericSymbol.GetMembers())
            {
                var current = member as IFieldSymbol;
                if (!current.TryGetNameAndValue(out var name, out var value)) continue;
                var fieldName = typeFullName + "." + name;

                valuesSb.Append(fieldName).Append(", ");
                namesSb.Append("nameof(").Append(fieldName).Append("), ");
                getNameSb.Append("                ")
                    .Append(value).Append(" => nameof(").Append(fieldName).AppendLine("),");
                isDefinedSb.Append(value).Append(" or ");
                parseSb.Append("                ")
                    .Append("nameof(").Append(fieldName).Append(") => ").Append(fieldName).Append(',').AppendLine();
                tryParseSb.Append("                    ")
                    .Append("case nameof(").Append(fieldName).AppendLine("):")
                    .Append("                        ")
                    .Append("result = ").Append(fieldName).Append(';').AppendLine()
                    .Append("                        ")
                    .AppendLine("return true;");
            }

            cacheSb.Append("        private sealed class ");
            cacheSb.Append(typeName);
            cacheSb.Append("Cache : Cache<");
            cacheSb.Append(typeFullName);
            cacheSb.Append("""
                           >
                                   {
                                       public override 
                           """);
            cacheSb.Append(typeFullName);
            cacheSb.Append("[] GetValues() => new [] { ");
            cacheSb.Append(valuesSb.Remove(valuesSb.Length - 2, 2));
            cacheSb.Append("""
                            };
                                       public override string[] GetNames() => new [] { 
                           """);
            cacheSb.Append(namesSb.Remove(namesSb.Length - 2, 2));
            cacheSb.Append("""
                            };
                                       public override string GetName(in 
                           """);
            cacheSb.Append(typeFullName);
            cacheSb.Append("""
                            value) => (int)value switch
                                       {
                           
                           """);
            cacheSb.Append(getNameSb);
            cacheSb.Append("""
                                           _ => throw new ArgumentOutOfRangeException(nameof(value), value, default)
                                       };
                                       public override bool IsDefined<TValue>(in TValue value) => value is 
                           """);
            cacheSb.Append(isDefinedSb.Remove(isDefinedSb.Length - 4, 4));
            cacheSb.Append("""
                           ;
                                       public override 
                           """);
            cacheSb.Append(typeFullName);
            cacheSb.Append("""
                            Parse(in string value) => value switch
                                       {
                           
                           """);
            cacheSb.Append(parseSb);
            cacheSb.Append("""
                                           _ => throw new ArgumentOutOfRangeException(nameof(value), value, default)
                                       };
                                       public override bool TryParse(in string value, out 
                           """);
            cacheSb.Append(typeFullName);
            cacheSb.Append("""
                            result)
                                       {
                                           switch (value)
                                           {
                           
                           """);
            cacheSb.Append(tryParseSb);
            cacheSb.AppendLine("""
                                               default:
                                                   result = default;
                                                   return false;
                                           }
                                       }
                                   }

                           """);
        }

        ctorSb.Remove(ctorSb.Length - 6, 6).AppendLine().Append(
            """
                        }
                    }


            """);

        mainSb.Append(ctorSb);
        mainSb.Append(cacheSb);
        mainSb.Append("""
                          }
                      }
                      """);

        context.AddSource("XEnum.Core.g.cs", mainSb.ToString());
    }
}