using Calculator.Brackets;
using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Calculator
{
    public static class Helper
    {

        // сопоставляет, элемент какого типа можно добавить по символу
        private static Dictionary<char, Type> symbol_elementType_map;
        static readonly char numberKey = 'd';

        public static char DecimalSeparator { get; set; }


        static Helper()
        {
            DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat
                .NumberDecimalSeparator[0];

            symbol_elementType_map = new Dictionary<char, Type>()
            {
                { '+', typeof(SumCommand) },
                { '-', typeof(SubtractCommand) },
                { '*', typeof(MultiplyCommand) },
                { '/', typeof(DivideCommand) },
                { '(', typeof(OpeningBracket) },
                { ')', typeof(ClosingBracket) },
                { numberKey, typeof(DynamicNumber) }
            };
        }

        public static Type ResolveElement(char symbol)
        {
            var map = symbol_elementType_map;

            if (map.ContainsKey(symbol)) return map[symbol];            
            else return map[numberKey];            
        }

        public static bool AreBracketsCorrect(ICollection<IExpressionElement> elements)
        {
            int bracketsLevel = 0;
            foreach (var elem in elements)
            {
                if (!(elem is IBracket)) continue;
                IBracket bracket = (IBracket)elem;

                bracketsLevel += (int)bracket.Type;
                if (bracketsLevel < 0) return false;
            }
            return bracketsLevel == 0;
        }

        public static void AdjustPrioritiesInBrackets(ICollection<IExpressionElement> elements)
        {
            int bracketsLevel = 0;
            // Команды могут быть разными по всему выражению, а скобки всегда важнее
            int maxPresentPriority = elements
                .Where(e => e is ICommand)
                .Select(e => ((ICommand)e).Priority)
                .Max();

            foreach (var elem in elements)
            {
                if (elem is ICommand)
                    ((ICommand)elem).Priority += bracketsLevel * maxPresentPriority;
                else if (elem is IBracket) bracketsLevel += (int)((IBracket)elem).Type;
            }
        }
    }
}
