﻿using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IDynamicNumber : IExpressionElement
    {
        double Value { get; set; }

        bool Append(char symbol);
    }
}