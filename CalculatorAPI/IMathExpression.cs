using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IMathExpression
    {
        ICollection<IExpressionElement> GetCollection();
        
        bool TryCalculate(out double result);
    }
}