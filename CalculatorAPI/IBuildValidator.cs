using System;
using System.Collections.Generic;

namespace CalculatorAPI
{
    public interface IBuildValidator
    {
        bool CanInsertNumber(IMathExpression expression);

        bool CanInsertNaN(IMathExpression expression, Type elementType);

        bool CanAppendNumber(IDynamicNumber number, char symbol);
    }
}