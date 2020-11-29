namespace Test.Functions
{
    public interface IFunction
    {
        string Key { get; }
        double DoCalculate(double first, double second);
    }
}
