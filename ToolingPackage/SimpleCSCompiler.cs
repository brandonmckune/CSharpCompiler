using System;
using System.Collections.Generic;
using System.Linq;
using System.CodeDom.Compiler;
using System.IO;

namespace ToolingPackage
{
    public class SimpleCSCompiler
    {
        private const string FILE_EXTENSION = "*.cs";
        private const string DOM_PROVIDER_LANG = "CSharp";
        private const string COMPILER_PARAMS_RECURSIVE = "/recurse:";
        private const string DEFAULT_ASSEMBLY_NAME = "generated.dll";

        private String _topSourceDirectory = string.Empty;
        private IList<String> _referencedAssemblies = null;
        private String _outputAssemblyName = DEFAULT_ASSEMBLY_NAME;
        private CompilerResults _compilerResults = null;
        private bool _didCompile = false;

        public String TopSourceDirectory
        { get => this._topSourceDirectory; protected set => this._topSourceDirectory = value; }

        public IList<String> ReferencedAssemblies
        { get => this._referencedAssemblies; protected set => this._referencedAssemblies = value; }

        public String OutputAssemblyName
        { get => this._outputAssemblyName; protected set => this._outputAssemblyName = value; }
        
        public CompilerResults Results
        { get => this._compilerResults; protected set => this._compilerResults = value; }

        public bool DidCompile
        { get => this._didCompile; protected set => this._didCompile = value; }

        protected DirectoryInfo Directory { get; set; }

        protected CompilerParameters CompilerParams { get; set; }

        public CodeDomProvider CodeDomProvider { get; set; }

        public SimpleCSCompiler() { }

        public SimpleCSCompiler(string topSourceDirectory)
        {
            this.SetTopSourceDirectory(topSourceDirectory);
        }

        public SimpleCSCompiler SetTopSourceDirectory(string directory)
        {
            this._topSourceDirectory = directory;
            this.SetDirectoryInfo(directory);
            return this;
        }

        public SimpleCSCompiler SetAssemblies(IList<String> assemblies)
        {
            assemblies = RemoveDuplicateAssemblyReferences(assemblies);
            this.ReferencedAssemblies = assemblies;
            return this;
        }

        public SimpleCSCompiler SetGeneratedAssemblyName(string generatedAssemblyName)
        {
            this._outputAssemblyName = generatedAssemblyName;
            return this;
        }

        public SimpleCSCompiler CompileAssembly()
        {
            this.DidCompile = false;

            if (!this.SetupCompiler())
            {
                return this;
            }

            //We are not passing files to this function because we are recursively compiling everything.
            this.Results = this.CodeDomProvider.CompileAssemblyFromFile(this.CompilerParams);
            this.DidCompile = this.Results.Errors.Count > 0 ? false : true;

            return this;
        }

        protected void SetDirectoryInfo(String directory = null)
        {
            directory = directory ?? this.TopSourceDirectory;
            this.Directory = new DirectoryInfo(directory);
        }

        protected IList<String> RemoveDuplicateAssemblyReferences(IList<String> assemblies)
        {
            IList<String> noDuplicateAssemblies = new List<String>();

            foreach(String assembly in assemblies)
            {
                if(!noDuplicateAssemblies.Contains(assembly))
                {
                    noDuplicateAssemblies.Add(assembly);
                }
            }

            return noDuplicateAssemblies;
        }

        protected bool SetupCompiler()
        {
            bool isSetup = true;

            this.CodeDomProvider = CodeDomProvider.CreateProvider(SimpleCSCompiler.DOM_PROVIDER_LANG);

            if (this.CodeDomProvider == null)
            {
                System.Console.WriteLine("CodeCompProvider returned null object. Compiling failed.");
                return !isSetup;
            }

            this.UpdateCompilerParameters();

            return isSetup;
        }

        protected void UpdateCompilerParameters()
        {
            this.CompilerParams = this.CompilerParams ?? new CompilerParameters();

            this.CompilerParams.GenerateExecutable = false; //dll is what we return
            this.CompilerParams.GenerateInMemory = false;
            this.CompilerParams.TreatWarningsAsErrors = false;
            this.CompilerParams.CompilerOptions +=
                SimpleCSCompiler.COMPILER_PARAMS_RECURSIVE +
                this.TopSourceDirectory +
                System.IO.Path.DirectorySeparatorChar +
                SimpleCSCompiler.FILE_EXTENSION;

            if( this.ReferencedAssemblies != null)
            {
                foreach (String assembly in this.ReferencedAssemblies)
                {
                    this.CompilerParams.ReferencedAssemblies.Add(assembly);
                }
            }

            this.CompilerParams.OutputAssembly =
                System.Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + this.OutputAssemblyName;
        }
    }
}
