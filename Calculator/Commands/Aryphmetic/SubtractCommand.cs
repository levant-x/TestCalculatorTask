using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Commands.Aryphmetic
{
    public class SubtractCommand : BaseCommand
    {
        public SubtractCommand()
            : base(2, 1)
        { }

        protected override double Calc(double left, double right)
        {
            return left - right;
        }
    }
}
