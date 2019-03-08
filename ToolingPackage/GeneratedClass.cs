using System;
using System.Collections.Generic;

namespace CustomCompiledObject
{
    public class GeneratedClass //: SomeClass, IGeneratedClass, ISomeClass
    {

        private IList<string> _property1;

        private string _property2;

        private string _property3;

        public IList<string> Property1 { get => this._property1; set => this._property1 = value; }

        protected string Property2 { get => this._property2; set => this._property2 = value; }

        private string Property3 { get => this._property3; set => this._property3 = value; }

        public GeneratedClass SetProperty1(IList<string> property1)
        {
            this._property1 = property1;
            return this;
        }

        protected GeneratedClass SetProperty2(string property2)
        {
            this._property2 = property2;
            return this;
        }


        public void GeneratedFunction1(int prop1, string prop2)
        {
            int x = prop1;
            Console.WriteLine("The value you set was: " + x + " and [" + prop2 + "]");
        }


        protected void GeneratedFunction2(int prop1, string prop2)
        {
            int x = prop1;
            Console.WriteLine("The value you set was: " + x + " and [" + prop2 + "]");
        }


        private void GeneratedFunction3(int prop1, string prop2)
        {
            int x = prop1;
            Console.WriteLine("The value you set was: " + x + " and [" + prop2 + "]");
        }


    }
}
