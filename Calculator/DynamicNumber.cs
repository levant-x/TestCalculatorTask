using CalculatorAPI;

namespace Calculator
{
    public class DynamicNumber : IDynamicNumber
    {
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

            if (!IsPossibleNumeric(symbol)) return result;
            else if (symbol == '.' && _valueString.Contains(".")) return result;
            
            else
            {
                result = true;
                _valueString += symbol;
            }

            return result;
        }

        private bool IsPossibleNumeric(char symbol)
        {
            return char.IsDigit(symbol) || symbol == '.';
        }
    }
}