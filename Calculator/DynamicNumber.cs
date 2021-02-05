using CalculatorAPI;

namespace Calculator
{
    public class DynamicNumber : IDynamicNumber
    {
        public double Value { get; set; }

        public bool Append(char symbol)
        {
            throw new System.NotImplementedException();
        }
    }
}