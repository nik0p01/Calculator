namespace Test.Functions
{
    public class Sub : IFunction
    {
        public Sub()
        {
            Key = "sub";
        }
        public string Key { get; private set; }

        public double DoCalculate(double first, double second)
        {
            return first - second;
        }
    }
}

