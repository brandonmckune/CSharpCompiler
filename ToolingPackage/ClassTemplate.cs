using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolingPackage
{
    public class ClassTemplate
    {
        private IList<string> _usingPaths;
        private string _name = string.Empty;
        private Extensions.AccessModifier _accesModifier = Extensions.AccessModifier.Public;
        private string _inheritedClass = string.Empty;
        private IList<string> _inheritedInterfaces;
        private SortedList<string, string> _properties;
        private IList<FunctionTemplate> _functions;

        public IList<string> UsingPaths { get => this._usingPaths; }
        public string Name { get => this._name; }
        public Extensions.AccessModifier AccessModifier { get => this._accesModifier; }
        public string InheritedClass { get => this._inheritedClass; }
        public IList<string> IneritedInterfaces { get => this._inheritedInterfaces; }
        public SortedList<string, string> Properties { get => this._properties; }
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
            this._name = Regex.Replace(className, @"\s+", string.Empty);
            return this;
        }

        public ClassTemplate SetScope(Extensions.AccessModifier scope)
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

        public ClassTemplate AddProperty(string propertyName, string propertyType)
        {
            if (this._properties.ContainsKey(propertyName))
            {
                this._properties[propertyName] = propertyType;
            }
            else
            {
                this._properties.Add(propertyName, propertyType);
            }

            return this;
        }

        public ClassTemplate AddProperties(SortedList<string, string> properties)
        {
            foreach(KeyValuePair<string, string> kvp in properties)
            {
                this.AddProperty(kvp.Key, kvp.Value);
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
            sb.Append(this.AccessModifier.ToLower() + Constants.SPACE + Constants.CLASS + Constants.SPACE + this.Name);

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

            //Basic Property definitions
            //TODO: Make this better with setters returning the object for chaining.
            foreach(KeyValuePair<string, string> property in this.Properties)
            {
                sb.AppendLine(
                    Extensions.AccessModifier.Public.ToLower() + Constants.SPACE + property.Value + Constants.SPACE + property.Key + Constants.PROPERTY_BODY);

                sb.AppendLine(string.Empty);
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
