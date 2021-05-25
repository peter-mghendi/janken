using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Janken.Shared.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using Engine = Janken.Shared.Attributes.EngineAttribute;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;

namespace Janken.Generators
{
    [Generator]
    public class EngineGenerator : ISourceGenerator
    {
        private const string EngineAttributeName = "Engine";

        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            var engineList = GetEngines(context.Compilation);
            var engineString = string.Join(", ", engineList.Select(engine => $"\"{engine.DisplayName}\""));

            var sourceBuilder = new StringBuilder($@"
using Janken.Core.Engines;
using Janken.Shared.Models;
using System;

namespace Janken.Core
{{
    /**
        Auto-generated at {DateTime.Now}.
    */
    public static partial class Meta
    {{
        public static readonly string[] Engines = {{ {engineString} }};

        public static IEngine SelectEngine(int i, IPlayer playerOne, IPlayer playerTwo) => i switch
        {{
            {string.Join(",\n\t\t\t", engineList.Select((engine, i) => $"{i} => new {engine.Identifier}(playerOne, playerTwo)")) + ","}
            _ => throw new Exception(""Unknown engine."")
        }};
    }}
}}");

            context.AddSource("Engines.Meta.cs", SourceText.From(sourceBuilder.ToString().Trim(), Encoding.UTF8));
        }

        private static List<(string Identifier, string DisplayName)> GetEngines(Compilation compilation)
        {
            IEnumerable<SyntaxNode> allNodes = compilation.SyntaxTrees.SelectMany(s => s.GetRoot().DescendantNodes());
            IEnumerable<ClassDeclarationSyntax> allClasses = allNodes
                .Where(d => d.IsKind(SyntaxKind.ClassDeclaration))
                .OfType<ClassDeclarationSyntax>();

            return allClasses.Select(component => TryGetEngine(compilation, component))
                .Where(engine => engine is not null)
                .Cast<(string, string)>()
                .ToList();
        }

        // TODO: Check for interface
        private static (string Identifier, string DisplayName)? TryGetEngine(Compilation compilation, ClassDeclarationSyntax component)
        {
            IEnumerable<ClassDeclarationSyntax> allClasses = compilation.SyntaxTrees
                .SelectMany(s => s.GetRoot().DescendantNodes())
                .Where(d => d.IsKind(SyntaxKind.ClassDeclaration))
                .OfType<ClassDeclarationSyntax>();

            Func<AttributeSyntax, bool> predicate = attr => attr.Name.ToString() == EngineAttributeName;

            var attributes = component.AttributeLists.SelectMany(x => x.Attributes)
                .Where(predicate)
                .ToList();

            var engineAttribute = attributes.FirstOrDefault(predicate);

            if (engineAttribute is null)
            {
                return null;
            }

            var identifier = component.Identifier.Text;
            (string Identifier, string DisplayName) engineMeta = (identifier, identifier);
            if (engineAttribute.ArgumentList?.Arguments.Count != 1) 
            {
                return engineMeta;
            }

            var semanticModel = compilation.GetSemanticModel(component.SyntaxTree);
            var typeInfo = semanticModel.GetTypeInfo(component);
            var engineNameArg = engineAttribute.ArgumentList.Arguments[0];
            engineMeta.DisplayName = semanticModel.GetConstantValue(engineNameArg.Expression).ToString();
            return engineMeta;
        }
    }
}