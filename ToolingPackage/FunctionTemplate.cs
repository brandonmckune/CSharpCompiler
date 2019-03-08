using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ToolingPackage
{
    public class FunctionTemplate
    {
        private Constants.AccessModifier _accessModifier = Constants.AccessModifier.Public;
        private string _name = string.Empty;
        private string _returnType = string.Empty;
        private SortedList<string, string> _parameters;
        private IList<string> _bodyStatements;

        public Constants.AccessModifier AccessModifier { get => this._accessModifier; }
        public string Name { get => this._name; }
        public string ReturnType { get => this._returnType; }
        public SortedList<string, string> Parameters { get => this._parameters; }
        public IList<string> Body { get => this._bodyStatements; }

        public FunctionTemplate() { }
        public FunctionTemplate(string name)
        {
            this.SetName(name);
        }

        public FunctionTemplate SetAccessModifier(Constants.AccessModifier accessModifier)
        {
            this._accessModifier = accessModifier;
            return this;
        }

        public FunctionTemplate SetName(string name)
        {
            this._name = name;
            return this;
        }

        public FunctionTemplate SetReturnType(string returnType)
        {
            this._returnType = returnType;
            return this;
        }

        public FunctionTemplate AddParameter(string name, string type)
        {
            this._parameters = this._parameters ?? new SortedList<string, string>();

            if (this._parameters.ContainsKey(name))
            {
                this._parameters[name] = type;
            }
            else
            {
                this._parameters.Add(name, type);
            }

            return this;
        }

        public FunctionTemplate SetParameters(SortedList<string, string> parameters)
        {
            foreach(KeyValuePair<string, string> kvp in parameters)
            {
                this.AddParameter(kvp.Key, kvp.Value);
            }

            return this;
        }

        public FunctionTemplate AddStatementToBody(string statement)
        {
            this._bodyStatements = this._bodyStatements ?? new List<string>();

            this._bodyStatements.Add(statement);
            return this;
        }

        public FunctionTemplate SetBody(IList<string> bodyStatements)
        {
            foreach(string statement in bodyStatements)
            {
                this.AddStatementToBody(statement);
            }

            return this;
        }

        public bool Equals(FunctionTemplate template)
        {
            if (template == null)
                return false;

            //If the name isn't the same nor the parameter list length the same - short circuit
            if( String.Compare(this.Name.ToLower(), template.Name.ToLower()) != 0 ||
                    this.Parameters.Count != template.Parameters.Count)
            {
                return false;
            }
            
            foreach(KeyValuePair<string, string> kvp in this.Parameters)
            {
                if (!template.Parameters.ContainsValue(kvp.Value))
                {
                    return false;
                }
            }

            return true;
        }

        public string ToStringForClassFile()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.AccessModifier.ToString().ToLower());
            sb.Append(Constants.SPACE);
            sb.Append(this.ReturnType);
            sb.Append(Constants.SPACE);
            sb.Append(this.Name);
            sb.Append(Constants.OPEN_PARENTHESIS);

            bool firstTimeThrough = true;
            foreach(KeyValuePair<string, string> kvp in this.Parameters)
            {
                sb.Append(firstTimeThrough ? string.Empty : Constants.COMMA);
                sb.Append(kvp.Value + Constants.SPACE + kvp.Key);
                firstTimeThrough = false;
            }
            sb.AppendLine(Constants.CLOSE_PARENTHESIS + Constants.BEGIN_CODE_BLOCK);

            foreach(string statement in this.Body)
            {
                sb.AppendLine(statement);
            }

            sb.AppendLine(Constants.END_CODE_BLOCK);

            return sb.ToString();
        }

        public string ToStringForInterfaceFile()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.ReturnType);
            sb.Append(Constants.SPACE);
            sb.Append(this.Name);
            sb.Append(Constants.OPEN_PARENTHESIS);

            bool firstTimeThrough = true;
            foreach (KeyValuePair<string, string> kvp in this.Parameters)
            {
                sb.Append(firstTimeThrough ? string.Empty : Constants.COMMA);
                sb.Append(kvp.Value + Constants.SPACE + kvp.Key);
            }
            sb.AppendLine(Constants.CLOSE_PARENTHESIS + Constants.SEMICOLON);

            return sb.ToString();
        }
    }
}
