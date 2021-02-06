namespace CalculatorAPI
{
    public interface ICommand : IExpressionElement
    {
        int Operands { get; }

        int Priority { get; set; }

        double Exec(double left, double right);
    }
}