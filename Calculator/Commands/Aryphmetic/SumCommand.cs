using CalculatorAPI;

namespace Calculator.Commands.Aryphmetic
{
    public class SumCommand : ICommand
    {
        public int Operands { get; }
        public int Priority { get; set; }

        public SumCommand()
        {
            Operands = 2;
            Priority = 1;
        }

        public double Exec(double left, double right)
        {
            return left + right;
        }
    }
}