using CalculatorAPI;

namespace Calculator.Commands.Aryphmetic
{
    public class MultiplyCommand : ICommand
    {
        public int Operands => throw new System.NotImplementedException();

        public int Priority { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public double Exec(double left, double right)
        {
            return left * right;
        }
    }
}