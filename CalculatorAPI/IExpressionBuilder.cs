using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IExpressionBuilder
    {
        bool TryAppendElement(IMathExpression expression, char symbol);

        bool TryParse(IMathExpression expression, string inputString);
    }
}