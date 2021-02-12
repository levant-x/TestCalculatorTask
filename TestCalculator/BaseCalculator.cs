using CalculatorAPI;

namespace TestCalculator
{
    public class BaseCalculator : ICalculator
    {
        private ICalcValidator calcValidator;

        public BaseCalculator(ICalcValidator calcValidator)
        {
            this.calcValidator = calcValidator;
        }

        public bool TryCalculate(IMathExpression expression, out double result)
        {
            throw new System.NotImplementedException();
        }
    }
}