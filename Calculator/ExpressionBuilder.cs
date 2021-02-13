using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ExpressionBuilder : IExpressionBuilder
    {
        private IFactory factory;
        private IBuildValidator validator;

        IMathExpression expression;
        ICollection<IExpressionElement> elements;
        char symbol;



        public ExpressionBuilder(IFactory factory, IBuildValidator validator)
        {
            this.factory = factory;
            this.validator = validator;
        }


        
        public bool TryAppendElement(IMathExpression expression, char symbol)
        {
            this.expression = expression;
            this.symbol = symbol;
            elements = expression.GetCollection();
            return InsertedNaN() || InsertedNumber() || AppendedNumber();
        }

        public bool TryParse(IMathExpression expression, string inputString)
        {
            foreach (var symbol in inputString)
                if (!TryAppendElement(expression, symbol))
                    return false;
            return true;
        }

                
        
        bool InsertedNaN()
        {
            var newElemType = factory.ResolveType(symbol);
            if (newElemType == null ||
                !validator.CanInsertNaN(expression, newElemType))
                return false;

            var newElem = factory.CreateNaN(symbol);
            elements.Add(newElem);
            return true;
        }

        bool InsertedNumber()
        {
            if (!validator.CanInsertNumber(expression))
                return false;
                            
            if (!(elements.Count == 0 ||
                elements.Last() is IOpeningBracket) &&
                symbol == validator.Minus)
                return false;
            
            var newNumber = factory.CreateNumber();
            if (!validator.CanAppendNumber(newNumber, symbol))
                return false;

            newNumber.Append(symbol);
            elements.Add(newNumber);
            return true;
        }

        bool AppendedNumber()
        {
            if (elements.Count == 0 ||
                !(elements.Last() is IDynamicNumber))
                return false;

            var lastNumber = (IDynamicNumber)elements.Last();
            if (!validator.CanAppendNumber(lastNumber, symbol))
                return false;

            lastNumber.Append(symbol);
            return true;
        }
    }
}