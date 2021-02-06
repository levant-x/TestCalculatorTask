using System.Collections.Generic;
using CalculatorAPI;

namespace Calculator
{
    public class MathExpression : IMathExpression
    {
        private IExpressionBuilder mathExpressionBuilder;

        public MathExpression(IExpressionBuilder mathExpressionBuilder)
        {
            this.mathExpressionBuilder = mathExpressionBuilder;
        }


        public bool Append(char symbol)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<IExpressionElement> GetCollection()
        {
            throw new System.NotImplementedException();
        }
    }
}