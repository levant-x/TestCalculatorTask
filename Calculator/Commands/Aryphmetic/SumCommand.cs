using CalculatorAPI;

namespace Calculator.Commands.Aryphmetic
{
    public class SumCommand : BaseCommand
    {
        public SumCommand()
            : base(2, 1) { }

        protected override double Calc(double left, double right)
        {
            return left + right;
        }
    }
}