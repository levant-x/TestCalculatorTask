using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ExpressionBuilder : IExpressionBuilder
    {
        private static Dictionary<char, Type> _symbol_elementType_map;
        private static Dictionary<Type, Type[]> _lastElement_possibleTypesOfNext_map;
        static readonly char numberKey;


        static ExpressionBuilder()
        {
            numberKey = 'd';

            _symbol_elementType_map = new Dictionary<char, Type>()
            {
                { '+', typeof(SumCommand) },
                { numberKey, typeof(DynamicNumber) }
            };

            _lastElement_possibleTypesOfNext_map = new Dictionary<Type, Type[]>()
            {
                { null, new Type[] { typeof(IDynamicNumber) } },

                { typeof(IDynamicNumber), new Type[]
                { typeof(IDynamicNumber), typeof(ICommand) } },

                { typeof(ICommand), new Type[] { typeof(IDynamicNumber) } }
            };
        }

        public bool TryAppendElement(ICollection<IExpressionElement> expression, char elemKey)
        {
            var lastElem = expression.Last();
            var elemToAdd = CreateNewElement(elemKey);
            var result = false;

            if (CanInsertElementOfType(lastElem, elemToAdd))
            {
                if (elemToAdd is IDynamicNumber)
                {
                    result = TryAppendNumber((IDynamicNumber)elemToAdd, elemKey);
                    if (!result) return false;
                }
                expression.Add(elemToAdd);
            }
            else if (lastElem is IDynamicNumber)
                result = TryAppendNumber((IDynamicNumber)lastElem, elemKey);
            return result;
        }

        IExpressionElement CreateNewElement(char symbol)
        {
            if (_symbol_elementType_map.ContainsKey(symbol))
            {
                var commandType = _symbol_elementType_map[symbol];
                return CreateElement(commandType);
            }
            else
            {
                var numberType = _symbol_elementType_map[numberKey];
                return CreateElement(numberType);
            }
        }

        IExpressionElement CreateElement<T>(T type) 
        {
            return (IExpressionElement)Activator.CreateInstance(typeof(T));
        }

        bool TryAppendNumber(IDynamicNumber number, char symbol)
        {
            return number.Append(symbol);
        }

        bool CanInsertElementOfType<T>(IExpressionElement lastElem, T elemToAdd)
        {
            var possibleTypes = _lastElement_possibleTypesOfNext_map[typeof(T)];
            return possibleTypes.Contains(typeof(T));
        }
    }
}
