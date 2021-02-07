using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Brackets
{
    public class OpeningBracket : IOpeningBracket
    {
        public BracketType Type { get; }

        public OpeningBracket()
        {
            Type = BracketType.Opening;
        }
    }
}
