
namespace ToolingPackage
{
    public static class Constants
    {
        public const string BEGIN_CODE_BLOCK = "{";
        public const string END_CODE_BLOCK = "}";
        public const string CLASS = "class";
        public const string INTERFACE = "interface";
        public const string USING = "using";
        public const string COLON = " : ";
        public const string SEMICOLON = ";";
        public const string NAMESPACE = "namespace CustomCompiledObject";
        public const string COMMA = ", ";
        public const string EMPTY_PARENTHESIS = "( )";
        public const string OPEN_PARENTHESIS = "( ";
        public const string CLOSE_PARENTHESIS = " )";
        public const string SPACE = " ";
        public const string PROPERTY_BODY = " { get; set; }";
        public const string GET = "get";
        public const string SET = "set";
        public const string VALUE = "value";
        public const string ARROW = " => ";
        public const string EQUALS = " = ";
        public const string THIS = "this";
        public const string PERIOD = ".";
        public const string RETURN = "return ";

        public const string BEGIN_LIST = "IList<";
        public const string END_LIST = ">";
        public const string PRIVATE_MEMBER = "_";

        public enum AccessModifier
        {
            Public, Protected, Private
        }

        public enum StorageType
        {
            Bool, Byte, SByte, Char, Decimal, Double, Float, Int, UInt, Long, ULong, Object, Short, UShort, String, Enum, Class
        }

        public enum ContainerType
        {
            Dictionary, List, None
        }
    }
}
