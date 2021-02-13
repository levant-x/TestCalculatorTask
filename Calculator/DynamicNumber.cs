using CalculatorAPI;

namespace Calculator
{
    public class DynamicNumber : IDynamicNumber
    {
        private double value;

        public double Value
        {
            get
            {
                double.TryParse(StringValue, out value);
                return value;
            }
            set
            {
                StringValue = value.ToString();
                this.value = value;
            }
        }

        public string StringValue
        {
            get;
            private set;
        } 



        public DynamicNumber()
        {
            StringValue = string.Empty;
        }



        public void Append(char symbol)
        {
            StringValue += symbol;
        }
    }
}