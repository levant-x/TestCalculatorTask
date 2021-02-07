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
            set => this.value = value;
        }


        public bool Append(char symbol)
        {
            var result = false;

            if (symbol == Helper.DecimalSeparator)
                symbol = decSeparatorToParse;
            else if (!IsPossibleNumeric(symbol)) return result;

            var tryingToAddMoreThan1dot = symbol == decSeparatorToParse &&
                valueString.Contains(decSeparatorToParse.ToString());

            if (tryingToAddMoreThan1dot) return result;            
            else
            {
                result = true;
                valueString += symbol;
            }

            return result;
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