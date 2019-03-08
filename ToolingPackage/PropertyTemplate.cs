using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolingPackage
{
    public class PropertyTemplate
    {
        private Constants.AccessModifier _accessModifier = Constants.AccessModifier.Public;
        private Constants.ContainerType _containerType = Constants.ContainerType.None;
        private Constants.StorageType _storageType = Constants.StorageType.Object;
        private string _className;
        private string _name;

        public Constants.AccessModifier AccessModifier { get => this._accessModifier; }
        public Constants.ContainerType ContainerType { get => this._containerType; }
        public Constants.StorageType StorageType { get => this._storageType; }
        public string ClassName { get => this._className; }
        public string Name { get => this._name; }

        public PropertyTemplate() { }
        public PropertyTemplate(string propertyName)
        {
            this.SetPropertyName(propertyName);
        }

        public PropertyTemplate SetAccessModifier(Constants.AccessModifier accessModifier)
        {
            this._accessModifier = accessModifier;
            return this;
        }
        public PropertyTemplate SetContainerType(Constants.ContainerType containerType)
        {
            this._containerType = containerType;
            return this;
        }
        public PropertyTemplate SetStorageType(Constants.StorageType storageType)
        {
            this._storageType = storageType;
            return this;
        }

        public PropertyTemplate SetClassName(string className)
        {
            this._className = className;
            return this;
        }

        public PropertyTemplate SetPropertyName(string propertyName)
        {
            this._name = propertyName;
            return this;
        }

        public bool Equals(PropertyTemplate template)
        {
            return String.Compare(this.Name.ToLower(), template.Name.ToLower()) == 0;
        }

        public string PrintSetterFunction(string className)
        {
            StringBuilder sb = new StringBuilder();

            //Do not print a set function if that property is private.
            if(this.AccessModifier == Constants.AccessModifier.Private)
                return sb.ToString();

            sb.Append(this.AccessModifier.ToString().ToLower() + Constants.SPACE + className + Constants.SPACE + Constants.SET.FirstCharToUpper() + this.Name);
            sb.Append(Constants.OPEN_PARENTHESIS + Constants.SPACE);
            
            switch (this.ContainerType)
            {
                case Constants.ContainerType.Dictionary:
                    throw new NotSupportedException("Currently not supporting Dictionary");
                case Constants.ContainerType.List:
                    sb.Append(Constants.BEGIN_LIST);
                    switch (this.StorageType)
                    {
                        case Constants.StorageType.Class:
                            sb.Append(this.ClassName);
                            break;
                        default:
                            sb.Append(this.StorageType.ToString().ToLower());
                            break;
                    }
                    sb.Append(Constants.END_LIST);
                    break;
                case Constants.ContainerType.None:
                default:
                    switch (this.StorageType)
                    {
                        case Constants.StorageType.Class:
                            sb.Append(this.ClassName);
                            break;
                        default:
                            sb.Append(this.StorageType.ToString().ToLower());
                            break;
                    }
                    break;
            }

            sb.Append(Constants.SPACE + this.Name.ToLower());
            sb.AppendLine(Constants.SPACE + Constants.CLOSE_PARENTHESIS + Constants.BEGIN_CODE_BLOCK);

            //Function internals
            sb.AppendLine(Constants.THIS + Constants.PERIOD + Constants.PRIVATE_MEMBER + this.Name.ToLower() + Constants.EQUALS + this.Name.ToLower() + Constants.SEMICOLON);
            sb.AppendLine(Constants.RETURN + Constants.THIS + Constants.SEMICOLON);
            sb.AppendLine(Constants.END_CODE_BLOCK);
            return sb.ToString();
        }

        public string PrintPrivateProperty()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.DoProperty(Constants.AccessModifier.Private, Constants.PRIVATE_MEMBER + this.Name.ToLower()));
            sb.AppendLine(Constants.SEMICOLON);

            return sb.ToString();
        }

        public string PrintPropertyWithGetOnly()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.DoProperty(this.AccessModifier, this.Name));
            sb.Append(Constants.BEGIN_CODE_BLOCK + Constants.SPACE);

            string privatePropertyName = Constants.PRIVATE_MEMBER + this.Name.ToLower();

            sb.Append(Constants.GET + Constants.SPACE + Constants.ARROW + Constants.SPACE + Constants.THIS + Constants.PERIOD + privatePropertyName + Constants.SEMICOLON);
            sb.AppendLine(Constants.SPACE + Constants.END_CODE_BLOCK);

            return sb.ToString();
        }

        public string PrintPropertyWithGetSet()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.DoProperty(this.AccessModifier, this.Name));
            sb.Append(Constants.BEGIN_CODE_BLOCK + Constants.SPACE);

            string privatePropertyName = Constants.PRIVATE_MEMBER + this.Name.ToLower();

            sb.Append(Constants.GET + Constants.SPACE + Constants.ARROW + Constants.SPACE + Constants.THIS + Constants.PERIOD + privatePropertyName + Constants.SEMICOLON);
            sb.Append(Constants.SET + Constants.SPACE + Constants.ARROW + Constants.SPACE + Constants.THIS + Constants.PERIOD + privatePropertyName + Constants.EQUALS + Constants.VALUE + Constants.SEMICOLON);
            sb.AppendLine(Constants.SPACE + Constants.END_CODE_BLOCK);

            return sb.ToString();
        }

        private string DoProperty(Constants.AccessModifier accessModifier, string propertyName)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(accessModifier.ToString().ToLower());
            sb.Append(Constants.SPACE);

            switch (this.ContainerType)
            {
                case Constants.ContainerType.Dictionary:
                    throw new NotSupportedException("Currently not supporting Dictionary");
                case Constants.ContainerType.List:
                    sb.Append(Constants.BEGIN_LIST);
                    switch (this.StorageType)
                    {
                        case Constants.StorageType.Class:
                            sb.Append(this.ClassName);
                            break;
                        default:
                            sb.Append(this.StorageType.ToString().ToLower());
                            break;
                    }
                    sb.Append(Constants.END_LIST);
                    break;
                case Constants.ContainerType.None:
                default:
                    switch (this.StorageType)
                    {
                        case Constants.StorageType.Class:
                            sb.Append(this.ClassName);
                            break;
                        default:
                            sb.Append(this.StorageType.ToString().ToLower());
                            break;
                    }
                    break;
            }

            sb.Append(Constants.SPACE);
            sb.Append(propertyName);

            return sb.ToString();
        }
    }
}
