
using System;
using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Hosting;
using ScriptCs.Engine.Mono;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using ScriptCs.Engine.Roslyn;

namespace DynamicHostExample
{
    class Program
    {
        static void Main(string[] args)
        {

            var console = (IConsole)new ScriptConsole();
            var logProvider = new ColoredConsoleLogProvider(LogLevel.Info, console);
            var builder = new ScriptServicesBuilder(console, logProvider);
            builder.ScriptHostFactory<DictionaryScriptHostFactory>();
            SetEngine(builder);
            var services = builder.Build();

            var executor = (ScriptExecutor)services.Executor;

            //add the Message reference and using so we can use the Message type.
            executor.AddReferences(typeof(Message).Assembly);
            executor.ImportNamespaces("DynamicHostExample");

            executor.Initialize(Enumerable.Empty<string>(), Enumerable.Empty<IScriptPack>());

            ExecuteLooseScript(executor);
            Console.ReadLine();
        }

        private static void ExecuteLooseScript(ScriptExecutor executor)
        {
            var code = new StringBuilder();
            code.AppendLine(@"Console.WriteLine(Foo);");
            code.AppendLine(@"Console.WriteLine(Message.Text);");
            code.AppendLine("#line hidden");
            //inject a Foo property
            code.AppendLine(@"String Foo {get {return (string) Data[""Foo""];}}");
            //inject the Message property
            code.AppendLine(@"Message Message {get {return (Message) Data[""Message""];}}");
            var result = executor.ExecuteScript(code.ToString());
        }

        private static void SetEngine(ScriptServicesBuilder builder)
        {
            var useMono = Type.GetType("Mono.Runtime") != null;
            if (useMono)
            {
                builder.ScriptEngine<MonoScriptEngine>();
            }
            else {
                builder.ScriptEngine<CSharpScriptEngine>();
            }
        }
    }
}

