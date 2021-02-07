using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IMathExpression: IAppendable
    {
        ICollection<IExpressionElement> GetCollection();
    }
}