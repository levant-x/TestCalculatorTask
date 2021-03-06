﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Commands.Aryphmetic
{
    public class DivideCommand : BaseCommand
    {
        public DivideCommand()
            : base(2, 2) { }

        protected override double Calc(double left, double right)
        {
            if (right == 0) throw new DivideByZeroException("Получен нулевой делитель");
            return left / right;
        }
    }
}
