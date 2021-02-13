using System;
using System.Collections.Generic;
using System.Linq;
using CalculatorAPI;

namespace Calculator
{
    public class MathExpression : IMathExpression
    {      
        ICollection<IExpressionElement> elements;
               
        public MathExpression()
        {
            elements = new List<IExpressionElement>();
        }

        public void Clear()
        {
            elements.Clear();
        }

        public ICollection<IExpressionElement> GetCollection()
        {
            return elements;
        }    
    }
}