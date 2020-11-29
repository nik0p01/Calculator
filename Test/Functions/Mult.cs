namespace Test.Functions
{
    public class Mult : IFunction
    {
        public Mult()
        {
            Key = "mult";
        }
        public string Key { get; private set; }

        public double DoCalculate(double first, double second)
        {
            return first * second;
        }
    }
}
