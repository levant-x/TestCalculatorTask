using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IExpressionBuilder
    {
        bool TryAppendElement(ICollection<IExpressionElement> expression, char elemKey);
    }
}