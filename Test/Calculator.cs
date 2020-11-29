using System;
using System.Collections.Generic;
using System.Linq;
using Test.Functions;

namespace Test
{
    public class Calculator
    {
        private string _functionName;
        public Calculator(string function)
        {
            _functionName = function;
        }

        public double TestFunction(double first, double second)
        {


            List<IFunction> Functions = GetFunctions();
            IFunction function;

            function = GetFunctionObject(_functionName, Functions);

            return function.DoCalculate(first, second);


        }


        private static IFunction GetFunctionObject(string functionName, List<IFunction> Functions)
        {
            foreach (var derivedFunctionType in Functions)
            {
                if (derivedFunctionType.Key == functionName)
                {
                    return derivedFunctionType;
                }
            }
            throw new ArgumentException("There is no that function");
        }

        private static List<IFunction> GetFunctions()
        {
            Type functionType = typeof(IFunction);
            var derivedFunctionTypes = functionType.Assembly.ExportedTypes.Where(t => (functionType.IsAssignableFrom(t) && functionType != t)).ToList();
            List<IFunction> Functions = new List<IFunction>();

            foreach (Type itm in derivedFunctionTypes)
            {
                Functions.Add((IFunction)Activator.CreateInstance(itm));
            }

            return Functions;
        }
    }
}

