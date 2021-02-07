using System;
using System.Collections.Generic;
using CalculatorAPI;

namespace Calculator
{
    public class MathExpression : IMathExpression
    {
        Stack<IDynamicNumber> deferredNumbers;
        Stack<ICommand> deferredCommands;

        LinkedList<IExpressionElement> elements;
        private IExpressionBuilder expressionBuilder;


        public MathExpression(IExpressionBuilder expressionBuilder)
        {
            this.expressionBuilder = expressionBuilder;
            elements = new LinkedList<IExpressionElement>();

            deferredNumbers = new Stack<IDynamicNumber>();
            deferredCommands = new Stack<ICommand>();
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

        public bool Calculate(out double result)
        {
            result = default(double);

            if (!IsExpressionComplete()) return false;
            return true;
        }

        private bool IsExpressionComplete()
        {
            if (elements.Count == 0) return false;

            var lastElement = elements.Last.Value;
            return lastElement is IDynamicNumber || lastElement is IClosingBracket;
        }
    }
}