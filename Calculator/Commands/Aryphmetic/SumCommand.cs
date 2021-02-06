using CalculatorAPI;

namespace Calculator.Commands.Aryphmetic
{
    public class SumCommand : ICommand
    {
        public int Operands { get; }


        public SumCommand()
        {
            Operands = 2;
        }

        public double Exec(double left, double right)
        {
            return left + right;
        }
    }
}