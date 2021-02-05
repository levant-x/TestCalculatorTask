using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IDynamicNumber
    {
        double Value { get; set; }

        bool Append(char symbol);
    }
}