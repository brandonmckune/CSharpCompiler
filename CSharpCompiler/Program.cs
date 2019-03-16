using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CompilerTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filesToCompileDirectory = @"C:\development\codetocompile";
            
            ToolingPackage.SimpleCSCompiler compiler =
                new ToolingPackage.SimpleCSCompiler()
                .SetTopSourceDirectory(filesToCompileDirectory)
                .SetAssemblies(new List<String>() { "System.dll" })
                .CompileAssembly();

            if (compiler.DidCompile)
            {
                System.Console.WriteLine("Compilation completed.");
                System.Console.WriteLine("Created: " + compiler.OutputAssemblyName);
            }
            else
            {
                System.Console.WriteLine("Errors in compiling source.");
                foreach (var error in compiler.Results.Errors)
                {
                    Console.WriteLine("  {0}", error.ToString());
                    Console.WriteLine();
                }
            }
            System.Console.ReadKey();


            var DLL = Assembly.LoadFile(System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + compiler.OutputAssemblyName);

            foreach (Type type in DLL.GetExportedTypes())
            {
                var c = Activator.CreateInstance(type);
                type.InvokeMember("MyFunction", BindingFlags.InvokeMethod, null, c, null);
            }

            Console.ReadLine();
        }
    }
}