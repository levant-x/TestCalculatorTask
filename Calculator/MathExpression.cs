using System.Collections.Generic;
using CalculatorAPI;

namespace Calculator
{
    public class MathExpression : IMathExpression
    {
        ICollection<IExpressionElement> elements;
        private IExpressionBuilder expressionBuilder;

        public MathExpression(IExpressionBuilder expressionBuilder)
        {
            this.expressionBuilder = expressionBuilder;
            elements = new List<IExpressionElement>();
        }


        public bool Append(char symbol)
        {
            var result = expressionBuilder.TryAppendElement(
                elements, symbol);
            return result;
        }

        public ICollection<IExpressionElement> GetCollection()
        {
            return elements;
        }

        public bool Parse(string inputString)
        {
            foreach (var symbol in inputString)
                if (!Append(symbol))
                {
                    elements.Clear();
                    return false;
                }
            return true;
        }

        public double Calculate()
        {
            throw new System.NotImplementedException();
        }
    }
}