using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CompilerTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filesToCompileDirectory = @"C:\development\codetocompile";

            //Get all the files in the directory
            DirectoryInfo directory = new DirectoryInfo(filesToCompileDirectory);
            FileInfo[] files = directory.GetFiles("*.cs");
            IList<String> fileNames = new List<String>();

            if (files.Length <= 0)
            {
                System.Console.WriteLine("No files to compile. Quitting.");
                return;
            }
            
            foreach (FileInfo fileObj in files)
            {
                fileNames.Add(filesToCompileDirectory + @"\" + fileObj.Name);
            }

            bool isCompiledWithoutErrors = false;
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            if (provider == null)
            {
                System.Console.WriteLine("CodeDomeProvider.CreateProvider returned null object.");
                return;
            }

            //define the parameters of the compiler.
            String dllName = String.Format(@"{0}\Generated.dll", System.Environment.CurrentDirectory);
            CompilerParameters compParams = new CompilerParameters();

            compParams.GenerateExecutable = false;
            compParams.OutputAssembly = String.Format(@"{0}\Generated.dll", System.Environment.CurrentDirectory);

            // Save the assembly as a physical file.
            compParams.GenerateInMemory = false;
            compParams.CompilerOptions += "/recurse:" + filesToCompileDirectory + System.IO.Path.DirectorySeparatorChar + "*.cs";

            // Set whether to treat all warnings as errors.
            compParams.TreatWarningsAsErrors = false;

            CompilerResults results = provider.CompileAssemblyFromFile(compParams, fileNames.ToArray());
            // Return the results of the compilation.
            isCompiledWithoutErrors = results.Errors.Count > 0 ? true : false;

            if (isCompiledWithoutErrors)
            {
                // Display compilation errors.
                System.Console.WriteLine("Errors in compiling source.");
                foreach (CompilerError ce in results.Errors)
                {
                    Console.WriteLine("  {0}", ce.ToString());
                    Console.WriteLine();
                }
            }
            else
            {
                // Display a successful compilation message.
                Console.WriteLine("Source built successfully");
            }

            System.Console.ReadKey();
        }
    }
}