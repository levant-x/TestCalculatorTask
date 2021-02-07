using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IDynamicNumber : IExpressionElement, IAppendable
    {
        double Value { get; set; }
    }
}