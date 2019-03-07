using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolingPackage
{
    public class ClassTemplate
    {
        private IList<string> _usingPaths;
        private string _name = string.Empty;
        private Constants.AccessModifier _accesModifier = Constants.AccessModifier.Public;
        private string _inheritedClass = string.Empty;
        private IList<string> _inheritedInterfaces;
        private IList<PropertyTemplate> _properties;
        private IList<FunctionTemplate> _functions;

        public IList<string> UsingPaths { get => this._usingPaths; }
        public string Name { get => this._name; }
        public Constants.AccessModifier AccessModifier { get => this._accesModifier; }
        public string InheritedClass { get => this._inheritedClass; }
        public IList<string> IneritedInterfaces { get => this._inheritedInterfaces; }
        public IList<PropertyTemplate> Properties { get => this._properties; }
        public IList<FunctionTemplate> Functions { get => this._functions; }

        public ClassTemplate() { }
        public ClassTemplate(string className)
        {
            this.SetClassName(className);
        }

        public ClassTemplate AddUsingPath(string path)
        {
            if (!this._usingPaths.Contains(path))
            {
                this._usingPaths.Add(path);
            }
            return this;
        }

        public ClassTemplate SetClassName(string className)
        {
            this._name = className;
            return this;
        }

        public ClassTemplate SetScope(Constants.AccessModifier scope)
        {
            this._accesModifier = scope;
            return this;
        }

        public ClassTemplate SetInheritedClass(string className)
        {
            this._inheritedClass = className;
            return this;
        }

        public ClassTemplate AddInheritedInterface(string inheritedInterface)
        {
            this._inheritedInterfaces = this._inheritedInterfaces ?? new List<string>();

            if (!this._inheritedInterfaces.Contains(inheritedInterface))
            {
                this._inheritedInterfaces.Add(inheritedInterface);
            }

            return this;
        }

        public ClassTemplate SetInheritedInterfaces(IList<string> inheritedInterfaces)
        {
            this._inheritedInterfaces = inheritedInterfaces;
            return this;
        }

        public ClassTemplate AddProperty(PropertyTemplate property)
        {
            if (!this._properties.Contains(property))
            {
                this._properties.Add(property);
            }

            return this;
        }

        public ClassTemplate AddProperties(IList<PropertyTemplate> properties)
        {
            foreach(PropertyTemplate property in properties)
            {
                this.AddProperty(property);
            }

            return this;
        }

        public ClassTemplate AddFunction(FunctionTemplate function)
        {
            if(!this._functions.Contains(function))
            {
                this._functions.Add(function);
            }

            return this;
        }

        public ClassTemplate AddFunctions(IList<FunctionTemplate> functions)
        {
            foreach(FunctionTemplate function in functions)
            {
                this.AddFunction(function);
            }

            return this;
        }

        public bool Equals(ClassTemplate template)
        {
            return string.Compare(this.Name.ToLower(), template.Name.ToLower()) == 0;
        }

        public string ToStringForFile()
        {
            StringBuilder sb = new StringBuilder();
            bool hasPreviousInheritance = false;

            foreach(string stmt in this.UsingPaths)
            {
                sb.AppendLine(Constants.USING + Constants.SPACE + stmt + Constants.SEMICOLON);
            }

            if (this.UsingPaths.Count > 0)
                sb.AppendLine(string.Empty);

            //Namespace Begin
            sb.AppendLine(Constants.NAMESPACE);
            sb.AppendLine(Constants.BEGIN_CODE_BLOCK);

            //Class Begin
            sb.Append(this.AccessModifier.ToString().ToLower() + Constants.SPACE + Constants.CLASS + Constants.SPACE + this.Name);

            if (this.InheritedClass.Length > 0)
            {
                hasPreviousInheritance = true;
                sb.Append(Constants.COLON + this.InheritedClass);
            }
            
            if(this.IneritedInterfaces.Count > 0)
            {
                sb.Append(hasPreviousInheritance ? Constants.COMMA : Constants.COLON);

                bool isFirstRun = true;
                foreach(string intrface in this.IneritedInterfaces)
                {
                    sb.Append(isFirstRun ? string.Empty : Constants.COMMA + intrface);
                    isFirstRun = false;
                }
            }

            sb.AppendLine(string.Empty);
            sb.AppendLine(Constants.BEGIN_CODE_BLOCK);
            sb.AppendLine(string.Empty);

            //Private Property Definitions
            foreach(PropertyTemplate property in this.Properties)
            {
                sb.AppendLine(property.PrintPrivateProperty());
            }

            foreach (PropertyTemplate property in this.Properties)
            {
                sb.AppendLine(property.PrintPropertyWithGetSet());
            }

            foreach (PropertyTemplate property in this.Properties)
            {
                sb.AppendLine(property.PrintSetterFunction(this.Name));
            }
            
            //Functions
            foreach(FunctionTemplate function in this.Functions)
            {
                sb.AppendLine(function.ToStringForClassFile());
                sb.AppendLine(string.Empty);
            }

            //Class End
            sb.AppendLine(Constants.END_CODE_BLOCK);
            //Namespace End
            sb.AppendLine(Constants.END_CODE_BLOCK);
            return sb.ToString();
        }
    }
}
