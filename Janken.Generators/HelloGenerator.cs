using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Janken.Generators
{
    [Generator]
    public class HelloGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            //
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // begin creating the source we'll inject into the users compilation
            var sourceBuilder = new StringBuilder(@"
using System;
namespace Janken.Core
{
    public class Hello
    {
        public static void SayHello() 
        {
            Console.WriteLine(""Hello from generated code!"");
");
            sourceBuilder.Append(@"
        }
    }
}");
            // inject the created source into the users compilation
            context.AddSource("helloGenerator", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }
    }
}