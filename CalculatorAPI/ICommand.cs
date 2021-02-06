namespace CalculatorAPI
{
    public interface ICommand
    {
        int Operands { get; }

        int Priority { get; set; }

        double Exec(double left, double right);
    }
}