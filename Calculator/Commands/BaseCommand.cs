using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public int Operands { get; }

        public int Priority { get; set; }


        public BaseCommand(int operands, int priority)
        {
            Operands = operands;
            Priority = priority;
        }

        public double Exec(double left, double right)
        {
            // общие проверки при необходимости
            return Calc(left, right);
        }

        protected abstract double Calc(double left, double right);
    }
}
