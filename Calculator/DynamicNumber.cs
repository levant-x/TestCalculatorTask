using CalculatorAPI;

namespace Calculator
{
    public class DynamicNumber : IDynamicNumber
    {
        readonly char decSeparatorToParse = ',';

        string valueString = "";
        private double value;

        public double Value
        {
            get
            {
                double.TryParse(valueString, out value);
                return value;
            }
            set
            {
                valueString = value.ToString();
                this.value = value;
            }
        }


        public bool Append(char symbol)
        {
            var status = false;

            if (symbol == Helper.DecimalSeparator)
                symbol = decSeparatorToParse;
            else if (!IsPossibleNumeric(symbol)) return status;

            var tryingToAddMoreThan1dot = symbol == decSeparatorToParse &&
                valueString.Contains(decSeparatorToParse.ToString());

            if (tryingToAddMoreThan1dot) return status;            
            else
            {
                status = true;
                valueString += symbol;
            }

            return status;
        }

        private bool IsPossibleNumeric(char symbol)
        {
            return char.IsDigit(symbol);
        }


        public override string ToString()
        {
            return valueString;
        }
    }
}