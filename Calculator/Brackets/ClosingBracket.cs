using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Brackets
{
    public class ClosingBracket : IClosingBracket
    {
        public BracketType Type { get; }

        public ClosingBracket()
        {
            Type = BracketType.Closing;
        }
    }
}
