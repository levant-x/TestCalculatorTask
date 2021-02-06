namespace CalculatorAPI
{
    public interface ICommand
    {
        double Exec(double left, double right);
    }
}