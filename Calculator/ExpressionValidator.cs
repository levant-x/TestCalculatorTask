using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Calculator
{
    public class ExpressionValidator : IBuildValidator, ICalcValidator
    {
        // сопоставляет, элемент каких типов можно добавить в выражение следующим
        protected Dictionary<Type, Type[]> lastTypeToNextPossibleLookup;
        protected static readonly Type emptyExprType = typeof(char);

        public readonly char DecimalSeparatorToAppend;
        public char DecimalSeparator
        {
            get;
            set;
        }

        public char Minus
        {
            get;
        }


        public ExpressionValidator()
        {
            Minus = '-';
            DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat
                .NumberDecimalSeparator[0];
            DecimalSeparatorToAppend = ',';

            lastTypeToNextPossibleLookup = new Dictionary<Type, Type[]>()
            {
                { emptyExprType, new Type[]
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
        


        public virtual bool CanInsertNumber(IMathExpression expression)
        {
            var lastElemType = GetLastElemType(expression);
            return CanTypeFollowGiven(typeof(IDynamicNumber), lastElemType);
        }

        public virtual bool CanInsertNaN(IMathExpression expression, Type elementType)
        {
            var lastElemType = GetLastElemType(expression);
            return CanTypeFollowGiven(elementType, lastElemType);
        }

        public virtual bool CanAppendNumber(IDynamicNumber number, char symbol)
        {
            if (symbol == Minus)
                return number.StringValue == string.Empty;
            else if (char.IsDigit(symbol)) return true;

            if (symbol == DecimalSeparator)
            {
                var separator = DecimalSeparatorToAppend.ToString();
                if (number.StringValue.Contains(separator)) return false;
            }
            return true;
        }

        public bool IsExpressionComplete(IMathExpression expression)
        {
            var elements = expression.GetCollection();
            return IsEndingValid(elements) && AreBracketsClosed(elements);            
        }



        protected virtual bool IsEndingValid(ICollection<IExpressionElement> elems)
        {
            if (elems.Count == 0) return false;
            var lastElem = elems.Last();
            return lastElem is IClosingBracket || lastElem is IDynamicNumber;
        }

        protected virtual bool AreBracketsClosed(ICollection<IExpressionElement> elems)
        {
            int bracketsLevel = 0;
            foreach (var elem in elems)
                if (elem is IBracket bracket)
                {
                    bracketsLevel += (int)bracket.Type;
                    if (bracketsLevel < 0) return false;
                }
            return bracketsLevel == 0;
        }

        Type GetLastElemType(IMathExpression expression)
        {
            var exprElems = expression.GetCollection();
            return exprElems.Count > 0 ? exprElems.Last().GetType() : emptyExprType;
        }

        Type[] GetTypesFollowingGiven(Type keyType)
        {
            var map = lastTypeToNextPossibleLookup;
            if (map.ContainsKey(keyType)) return map[keyType];

            // интерфейс, реализуемый элементом и имеющийся в настройках
            keyType = keyType.GetInterfaces()
                .First(i => map.ContainsKey(i));
            return map[keyType];
        }

        bool CanTypeFollowGiven(Type nextType, Type prevType)
        {
            var possibleTypes = GetTypesFollowingGiven(prevType);

            // имеется ли в настройках интерфейс, реализуемый(?) элементом
            var result = possibleTypes.Contains(nextType) ||
                possibleTypes
                .Any(t => nextType.GetInterfaces()
                .Contains(t));
            return result;
        }
    }
}