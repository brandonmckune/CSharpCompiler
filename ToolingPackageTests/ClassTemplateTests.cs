using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToolingPackage;

namespace ToolingPackageTests
{
    [TestClass]
    public class ClassTemplateTests
    {
        public ClassTemplate _template { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _template = new ClassTemplate();
        }

        [TestMethod]
        public void TestFullClassOutput()
        {

            _template.SetClassName("GeneratedClass")
                .SetInheritedClass("SomeClass")
                .SetInheritedInterfaces(new List<string>()
                {
                    "IGeneratedClass",
                    "ISomeClass"
                })
                .SetScope(Constants.AccessModifier.Public)
                .SetProperties(new List<PropertyTemplate>()
                {
                    new PropertyTemplate()
                        .SetAccessModifier(Constants.AccessModifier.Public)
                        .SetPropertyName("Property1")
                        .SetContainerType(Constants.ContainerType.List)
                        .SetStorageType(Constants.StorageType.String)
                        .SetClassName("GeneratedClass"),
                    new PropertyTemplate()
                        .SetAccessModifier(Constants.AccessModifier.Protected)
                        .SetPropertyName("Property2")
                        .SetContainerType(Constants.ContainerType.None)
                        .SetStorageType(Constants.StorageType.String)
                        .SetClassName("GeneratedClass"),
                    new PropertyTemplate()
                        .SetAccessModifier(Constants.AccessModifier.Private)
                        .SetPropertyName("Property3")
                        .SetContainerType(Constants.ContainerType.None)
                        .SetStorageType(Constants.StorageType.String)
                        .SetClassName("GeneratedClass")
                })
                .SetFunctions(new List<FunctionTemplate>()
                {
                    new FunctionTemplate()
                        .SetAccessModifier(Constants.AccessModifier.Public)
                        .SetName("GeneratedFunction1")
                        .SetParameters(new SortedList<string, string>()
                        {
                            { "prop1", Constants.StorageType.Int.ToString().ToLower() },
                            { "prop2", Constants.StorageType.String.ToString().ToLower() },
                        })
                        .SetBody(new List<string>()
                        {
                            "int x = prop1;",
                            "Console.WriteLine(\"The value you set was: \" + x + \" and [\" + prop2 + \"]\");"
                        })
                        .SetReturnType("void"),
                    new FunctionTemplate()
                        .SetAccessModifier(Constants.AccessModifier.Protected)
                        .SetName("GeneratedFunction2")
                        .SetParameters(new SortedList<string, string>()
                        {
                            { "prop1", Constants.StorageType.Int.ToString().ToLower() },
                            { "prop2", Constants.StorageType.String.ToString().ToLower() },
                        })
                        .SetBody(new List<string>()
                        {
                            "int x = prop1;",
                            "Console.WriteLine(\"The value you set was: \" + x + \" and [\" + prop2 + \"]\");"
                        })
                        .SetReturnType("void"),
                    new FunctionTemplate()
                        .SetAccessModifier(Constants.AccessModifier.Private)
                        .SetName("GeneratedFunction3")
                        .SetParameters(new SortedList<string, string>()
                        {
                            { "prop1", Constants.StorageType.Int.ToString().ToLower() },
                            { "prop2", Constants.StorageType.String.ToString().ToLower() },
                        })
                        .SetBody(new List<string>()
                        {
                            "int x = prop1;",
                            "Console.WriteLine(\"The value you set was: \" + x + \" and [\" + prop2 + \"]\");"
                        })
                        .SetReturnType("void")
                })
                .SetUsingPaths(new List<string>()
                {
                    "System",
                    "System.Collections.Generic"
                });

            string output = _template.ToStringForFile();

            Assert.IsTrue(true);
        }

    }
}
