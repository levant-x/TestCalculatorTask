namespace CalculatorAPI
{
    public interface ICalcValidator
    {
        bool IsExpressionComplete(IMathExpression expression);
    }
}