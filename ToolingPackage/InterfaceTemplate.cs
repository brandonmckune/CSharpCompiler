
using System.Collections.Generic;
using System.Text;

namespace ToolingPackage
{
    public class InterfaceTemplate
    {
        private string _name = string.Empty;
        private IList<string> _inheritedInterfaces;
        private SortedList<string, string> _properties;
        private IList<FunctionTemplate> _functions;

        public string Name { get => this._name; }
        public IList<string> IneritedInterfaces { get => this._inheritedInterfaces; }
        public SortedList<string, string> Properties { get => this._properties; }
        public IList<FunctionTemplate> Functions { get => this._functions; }

        public InterfaceTemplate() { }
        public InterfaceTemplate(string interfaceName)
        {
            this._name = interfaceName;
        }

        public InterfaceTemplate SetInterfaceName(string className)
        {
            this._name = className;
            return this;
        }

        public InterfaceTemplate AddInheritedInterface(string inheritedInterface)
        {
            this._inheritedInterfaces = this._inheritedInterfaces ?? new List<string>();

            if (!this._inheritedInterfaces.Contains(inheritedInterface))
            {
                this._inheritedInterfaces.Add(inheritedInterface);
            }

            return this;
        }

        public InterfaceTemplate SetInheritedInterfaces(IList<string> inheritedInterfaces)
        {
            this._inheritedInterfaces = inheritedInterfaces;
            return this;
        }


        public InterfaceTemplate AddProperty(string propertyName, string propertyType)
        {
            this._properties = this._properties ?? new SortedList<string, string>();

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

        public InterfaceTemplate AddProperties(SortedList<string, string> properties)
        {
            foreach (KeyValuePair<string, string> kvp in properties)
            {
                this.AddProperty(kvp.Key, kvp.Value);
            }

            return this;
        }

        public InterfaceTemplate AddFunction(FunctionTemplate function)
        {
            this._functions = this._functions ?? new List<FunctionTemplate>();

            if (!this._functions.Contains(function))
            {
                this._functions.Add(function);
            }

            return this;
        }

        public InterfaceTemplate AddFunctions(IList<FunctionTemplate> functions)
        {
            foreach (FunctionTemplate function in functions)
            {
                this.AddFunction(function);
            }

            return this;
        }

        public bool Equals(InterfaceTemplate template)
        {
            return string.Compare(this.Name.ToLower(), template.Name.ToLower()) == 0;
        }

        public string ToStringForFile()
        {
            StringBuilder sb = new StringBuilder();
            
            //Namespace Begin
            sb.AppendLine(Constants.NAMESPACE);
            sb.AppendLine(Constants.BEGIN_CODE_BLOCK);

            //INTERFACE Begin
            sb.Append(Constants.AccessModifier.Public.ToString().ToLower() + Constants.SPACE + Constants.INTERFACE + Constants.SPACE + this.Name);

            if (this.IneritedInterfaces.Count > 0)
            {
                bool isFirstRun = true;
                foreach (string intrface in this.IneritedInterfaces)
                {
                    sb.Append(isFirstRun ? Constants.COLON : Constants.COMMA + intrface);
                    isFirstRun = false;
                }
            }

            sb.AppendLine(string.Empty);
            sb.AppendLine(Constants.BEGIN_CODE_BLOCK);
            sb.AppendLine(string.Empty);

            //Basic Property definitions
            //TODO: Make this better with setters returning the object for chaining.
            foreach (KeyValuePair<string, string> property in this.Properties)
            {
                sb.AppendLine(
                    Constants.AccessModifier.Public.ToString().ToLower() + Constants.SPACE + property.Value + Constants.SPACE + property.Key + Constants.PROPERTY_BODY);

                sb.AppendLine(string.Empty);
            }

            //Functions
            foreach (FunctionTemplate function in this.Functions)
            {
                sb.AppendLine(function.ToStringForInterfaceFile());
                sb.AppendLine(string.Empty);
            }

            //Interface End
            sb.AppendLine(Constants.END_CODE_BLOCK);
            //Namespace End
            sb.AppendLine(Constants.END_CODE_BLOCK);

            return sb.ToString();
        }
    }
}
