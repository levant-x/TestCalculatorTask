using CalculatorAPI;

namespace Calculator.Commands.Aryphmetic
{
    public class MultiplyCommand : BaseCommand
    {
        public MultiplyCommand()
            : base(2, 2) { }

        protected override double Calc(double left, double right)
        {
            return left * right;
        }
    }
}