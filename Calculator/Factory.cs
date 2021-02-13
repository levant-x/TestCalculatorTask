using Calculator.Brackets;
using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Factory : IFactory
    {
        // сопоставляет, элемент какого типа можно добавить по символу
        private static Dictionary<char, Type> symbolTypeLookup;
        static readonly char keyForNumber = 'd';



        static Factory()
        {
            symbolTypeLookup = new Dictionary<char, Type>()
            {
                { '+', typeof(SumCommand) },
                { '-', typeof(SubtractCommand) },
                { '*', typeof(MultiplyCommand) },
                { '/', typeof(DivideCommand) },
                { '(', typeof(OpeningBracket) },
                { ')', typeof(ClosingBracket) },
                { keyForNumber, typeof(DynamicNumber) }
            };
        }
                

                
        public IExpressionElement CreateNaN(char symbol)
        {
            var type = ResolveType(symbol);
            return (IExpressionElement)Activator.CreateInstance(type);
        }

        public IDynamicNumber CreateNumber()
        {
            var type = ResolveType(keyForNumber);
            return (IDynamicNumber)Activator.CreateInstance(type);
        }

        public Type ResolveType(char symbol)
        {
            var lookup = symbolTypeLookup;
            if (lookup.ContainsKey(symbol)) return lookup[symbol];
            else return null;
        }
    }
}