using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IMathExpression
    {
        ICollection<IExpressionElement> GetCollection();

        void Clear();
    }
}