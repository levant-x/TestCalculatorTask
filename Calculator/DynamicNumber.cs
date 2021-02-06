using CalculatorAPI;

namespace Calculator
{
    public class DynamicNumber : IDynamicNumber
    {
        readonly char decSeparatorToParse = ',';

        string _valueString = "";
        private double _value;

        public double Value
        {
            get
            {
                double.TryParse(_valueString, out _value);
                return _value;
            }
            set => _value = value;
        }


        public bool Append(char symbol)
        {
            var result = false;

            if (symbol == Helper.DecimalSeparator)
                symbol = decSeparatorToParse;
            else if (!IsPossibleNumeric(symbol)) return result;

            var tryingToAddMoreThan1dot = symbol == decSeparatorToParse &&
                _valueString.Contains(decSeparatorToParse.ToString());

            if (tryingToAddMoreThan1dot) return result;            
            else
            {
                result = true;
                _valueString += symbol;
            }

            return result;
        }

        private bool IsPossibleNumeric(char symbol)
        {
            return char.IsDigit(symbol);
        }


        public override string ToString()
        {
            return _valueString;
        }
    }
}