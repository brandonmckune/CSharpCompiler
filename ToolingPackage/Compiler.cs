using System;
using System.Collections.Generic;
using System.Linq;
using System.CodeDom.Compiler;
using System.IO;

namespace ToolingPackage
{
    public class Compiler
    {
        public String FileDirectory { get; set; }
        public System.IO.DirectoryInfo Directory { get; set; }
        public FileInfo[] Files { get; set; }
        public System.Collections.Generic.IList<String> FileNames { get; set; }
        public System.Collections.Generic.IList<String> ReferencedAssemblies { get; set; }
        public CodeDomProvider DomProvider { get; set; }
        public CompilerParameters CompilerParams { get; set; }
        public CompilerResults Results { get; set; }
        public bool DidCompile { get; set; }

        //compParams.OutputAssembly = String.Format(@"{0}\Generated.dll", System.Environment.CurrentDirectory);
        public String DLLOutputAssemblyName { get; set; }

        private const string FILE_EXTENSION = "*.cs";
        private const string DOM_PROVIDER_LANG = "CSharp";
        private const string COMPILER_PARAMS_RECURSIVE = "/recurse:";

        public Compiler() { }

        public Compiler(string directory)
        {
            this.FileDirectory = directory;
        }

        public Compiler WithCodeFileDirectory(string directory)
        {
            this.FileDirectory = directory;

            this.Directory = new DirectoryInfo(this.FileDirectory);
            this.Files = this.Directory.GetFiles(Compiler.FILE_EXTENSION);

            if (this.Files.Length <= 0)
            {
                System.Console.WriteLine("No files to compile. Quitting.");
                return this;
            }

            this.UpdateFileNames();

            return this;
        }

        public Compiler WithAssemblies(IList<String> assemblies)
        {
            this.ReferencedAssemblies = assemblies;

            this.CompilerParams = this.CompilerParams ?? new CompilerParameters();

            foreach (String assembly in this.ReferencedAssemblies)
            {
                this.CompilerParams.ReferencedAssemblies.Add(assembly);
            }

            return this;
        }

        public Compiler SetupCompiler(string assemblyName = "generated.dll")
        {
            this.DomProvider = CodeDomProvider.CreateProvider(Compiler.DOM_PROVIDER_LANG);

            this.DLLOutputAssemblyName = assemblyName;

            if (this.DomProvider == null)
            {
                System.Console.WriteLine("CodeCompProvider returned null object. Compiling failed.");
                return this;
            }

            this.UpdateCompilerParameters();

            return this;
        }

        public Compiler CompileBinaries()
        {
            this.DidCompile = false;

            this.Results = this.DomProvider.CompileAssemblyFromFile(this.CompilerParams);

            this.DidCompile = this.Results.Errors.Count > 0 ? false : true;

            return this;
        }

        protected void UpdateFileNames()
        {
            if (this.FileNames == null)
            {
                this.FileNames = new List<String>();
            }

            foreach (FileInfo fileObj in this.Files)
            {
                this.FileNames.Add(this.FileDirectory + System.IO.Path.DirectorySeparatorChar + fileObj.Name);
            }
        }

        protected void UpdateCompilerParameters()
        {
            this.CompilerParams = this.CompilerParams ?? new CompilerParameters();

            this.CompilerParams.GenerateExecutable = false; //dll is what we return
            this.CompilerParams.GenerateInMemory = false;
            this.CompilerParams.TreatWarningsAsErrors = false;
            this.CompilerParams.CompilerOptions +=
                Compiler.COMPILER_PARAMS_RECURSIVE +
                this.FileDirectory +
                System.IO.Path.DirectorySeparatorChar +
                Compiler.FILE_EXTENSION;

            this.CompilerParams.OutputAssembly =
                System.Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + this.DLLOutputAssemblyName;
        }
    }
}
