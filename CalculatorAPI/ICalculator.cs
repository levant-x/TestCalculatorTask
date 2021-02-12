namespace CalculatorAPI
{
    public interface ICalculator
    {
        bool TryCalculate(IMathExpression expression, out double result);
    }
}