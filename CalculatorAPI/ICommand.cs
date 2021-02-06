namespace CalculatorAPI
{
    public interface ICommand
    {
        int Operands { get; }

        double Exec(double left, double right);
    }
}