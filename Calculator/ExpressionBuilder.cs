﻿using Calculator.Commands.Aryphmetic;
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
        private static Dictionary<Type, Type[]> lastElement_possibleTypesOfNext_map;
        static readonly Type typeToStartExpressionWith = typeof(char);


        static ExpressionBuilder()
        {
            lastElement_possibleTypesOfNext_map = new Dictionary<Type, Type[]>()
            {
                { typeToStartExpressionWith, new Type[] 
                { typeof(IDynamicNumber), typeof(IOpeningBracket) } },

                { typeof(ICommand), new Type[]
                { typeof(IDynamicNumber), typeof(IOpeningBracket) } },

                { typeof(IDynamicNumber), new Type[] 
                { typeof(ICommand), typeof(IClosingBracket) } },

                { typeof(IOpeningBracket), new Type[]
                { typeof(IOpeningBracket), typeof(IDynamicNumber) } },

                { typeof(IClosingBracket), new Type[]
                { typeof(IClosingBracket), typeof(ICommand) } }
            };
        }

        public bool TryAppendElement(ICollection<IExpressionElement> expression, char elemKey)
        {
            var lastElem = expression.Count == 0 ? null : expression.Last();
            var elemToAdd = CreateNewElement(elemKey);
            var status = false;

            if (CanInsertElementOfType(lastElem, elemToAdd))
            {
                if (elemToAdd is IDynamicNumber)
                {
                    status = TryAppendNumber((IDynamicNumber)elemToAdd, elemKey);
                    if (!status) return false;
                }
                status = true;
                expression.Add(elemToAdd);
            }
            else if (lastElem is IDynamicNumber)
                status = TryAppendNumber((IDynamicNumber)lastElem, elemKey);
            return status;
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
            var map = lastElement_possibleTypesOfNext_map;

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