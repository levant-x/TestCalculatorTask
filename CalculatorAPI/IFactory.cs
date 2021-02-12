using System;

namespace CalculatorAPI
{
    public interface IFactory
    {
        IExpressionElement CreateNaN(char symbol);

        IDynamicNumber CreateNumber();

        Type ResolveType(char symbol);
    }
}