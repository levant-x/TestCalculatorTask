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
        // сопоставляет, элемент каких типов можно добавить в выражение следующим
        private static Dictionary<Type, Type[]> _lastElement_possibleTypesOfNext_map;
        static readonly Type typeToStartExpressionWith = typeof(char);


        static ExpressionBuilder()
        {
            _lastElement_possibleTypesOfNext_map = new Dictionary<Type, Type[]>()
            {
                { typeToStartExpressionWith, new Type[] { typeof(IDynamicNumber) } },
                { typeof(ICommand), new Type[] { typeof(IDynamicNumber) } },
                { typeof(IDynamicNumber), new Type[] { typeof(ICommand) } }
            };
        }

        public bool TryAppendElement(ICollection<IExpressionElement> expression, char elemKey)
        {
            var lastElem = expression.Count == 0 ? null : expression.Last();
            var elemToAdd = CreateNewElement(elemKey);
            var result = false;

            if (CanInsertElementOfType(lastElem, elemToAdd))
            {
                if (elemToAdd is IDynamicNumber)
                {
                    result = TryAppendNumber((IDynamicNumber)elemToAdd, elemKey);
                    if (!result) return false;
                }
                result = true;
                expression.Add(elemToAdd);
            }
            else if (lastElem is IDynamicNumber)
                result = TryAppendNumber((IDynamicNumber)lastElem, elemKey);
            return result;
        }

        IExpressionElement CreateNewElement(char symbol)
        {
            var elementType = Helper.ResolveElement(symbol);
            return (IExpressionElement)Activator.CreateInstance(elementType);
        }

        bool TryAppendNumber(IDynamicNumber number, char symbol)
        {
            return number.Append(symbol);
        }

        bool CanInsertElementOfType(IExpressionElement lastElem, IExpressionElement elemToAdd)
        {
            var map = _lastElement_possibleTypesOfNext_map;

            // интерфейс, реализуемый элементом и имеющийся в настройках
            var keyType = lastElem == null ? typeToStartExpressionWith :
                lastElem.GetType()
                .GetInterfaces()
                .First(i => map.ContainsKey(i)); 
            var possibleTypes = map[keyType];

            // имеется ли в настройках интерфейс, реализуемый элементом
            var result = possibleTypes
                .Any(t => elemToAdd.GetType()
                .GetInterfaces()
                .Contains(t)); 
            return result;
        }
    }
}