using Calculator.Brackets;
using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public static class Helper
    {

        // сопоставляет, элемент какого типа можно добавить по символу
        private static Dictionary<char, Type> _symbol_elementType_map;
        static readonly char numberKey = 'd';

        public static char DecimalSeparator { get; set; }


        static Helper()
        {
            DecimalSeparator = CultureInfo.CurrentCulture.NumberFormat
                .NumberDecimalSeparator[0];

            _symbol_elementType_map = new Dictionary<char, Type>()
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
            var map = _symbol_elementType_map;

            if (map.ContainsKey(symbol)) return map[symbol];            
            else return map[numberKey];            
        }
    }
}
