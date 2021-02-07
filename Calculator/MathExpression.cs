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
            return expressionBuilder.TryAppendElement(elements, symbol);
        }

        public ICollection<IExpressionElement> GetCollection()
        {
            return elements;
        }

        public bool Parse(string inputString)
        {
            foreach (var symbol in inputString)
            {
                if (Append(symbol)) continue;
                elements.Clear();
                return false;
            }
            return true;
        }

        public bool Calculate(out double result)
        {
            result = default(double);

            if (!IsExpressionComplete()) return false;
            if (!AreBracketsCorrect()) return false;
            return true;
        }

        private bool IsExpressionComplete()
        {
            if (elements.Count == 0) return false;

            var lastElement = elements.Last.Value;
            return lastElement is IDynamicNumber || lastElement is IClosingBracket;
        }

        private bool AreBracketsCorrect()
        {
            int bracketsLevel = 0;
            foreach (var elem in elements)
            {                
                if (!(elem is IBracket)) continue;

                IBracket bracket = (IBracket)elem;
                bracketsLevel += (int)bracket.Type;
                if (bracketsLevel < 0) return false;
            }
            return bracketsLevel == 0;
        }
    }
}