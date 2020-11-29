namespace Test.Functions
{
    public class Add : IFunction
    {
        public Add()
        {
            Key = "add";
        }

        public string Key { get; private set; }

        public double DoCalculate(double first, double second)
        {
            return first + second;
        }
    }
}
