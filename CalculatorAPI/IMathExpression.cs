using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IMathExpression
    {
        bool Append(char symbol);
        ICollection<IExpressionElement> GetCollection();
    }
}