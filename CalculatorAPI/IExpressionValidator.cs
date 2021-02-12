using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IExpressionValidator
    {
        bool CanInsertNumber(IMathExpression expression);
    }
}