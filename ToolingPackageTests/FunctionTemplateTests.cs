using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolingPackage;

namespace ToolingPackageTests
{
    [TestClass]
    public class FunctionTemplateTests
    {
        public FunctionTemplate _template { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _template = new FunctionTemplate();
        }

        [TestMethod]
        public void TestSetNameRemovesSpaces()
        {
            _template.SetName("Hello There");
            Assert.IsFalse(String.Compare(_template.Name, "HelloThere") == 0);
        }

        [TestMethod]
        public void TestAddParameterDoesNotAddTwice()
        {
            const string name = "thing1";
            const string param1 = "int";
            const string param2 = "string";

            _template.AddParameter(name, param1).AddParameter(name, param2);

            if (!_template.Parameters.ContainsKey(name))
                Assert.Fail("Failed to insert parameter " + name);

            Assert.IsTrue(String.Compare(_template.Parameters[name], param2) == 0);
        }

        [TestMethod]
        public void TestFunctionTemplateEqualsDefinition()
        {
            _template.SetName("Name1")
                .SetAccessModifier(Constants.AccessModifier.Public)
                .SetReturnType("string");

            SortedList<string, string> parameters = new SortedList<string, string>();
            parameters.Add("param1", "string");

            _template.SetParameters(parameters);

            FunctionTemplate template = new FunctionTemplate("Name1")
                .SetAccessModifier(Constants.AccessModifier.Public)
                .SetReturnType("string")
                .SetParameters(parameters);

            Assert.IsTrue(_template.Equals(template));

            template.SetName("Name");
            Assert.IsFalse(_template.Equals(template));

            parameters.Add("param2", "int");
            template.SetName("Name1").SetParameters(parameters);
            Assert.IsFalse(_template.Equals(template));
        }

        [TestMethod]
        public void TestToStringForClassFileFunctionDefinition()
        {
            SortedList<string, string> parameters = new SortedList<string, string>();
            parameters.Add("name", "string");

            IList<string> bodyStatements = new List<string>()
            {
                "int x = 5;",
                "Console.WriteLine(\"Hello world\");"
            };
            _template.SetBody(bodyStatements);

            _template.SetName("TestFunction")
                .SetAccessModifier(Constants.AccessModifier.Public)
                .SetReturnType("void")
                .SetParameters(parameters);

            string output = _template.ToStringForClassFile();
        }
    }
}
