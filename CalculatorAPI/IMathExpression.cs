using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IMathExpression: IAppendable
    {
        ICollection<IExpressionElement> GetCollection();

        bool Parse(string inputString);

        bool Calculate(out double result);
    }
}