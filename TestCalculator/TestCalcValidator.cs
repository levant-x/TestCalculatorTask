using Calculator;
using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCalculator
{
    public class TestCalcValidator
    {
        IFactory factory;
        IBuildValidator bldValidator;
        ICalcValidator calcValidator;
        IExpressionBuilder builder;
        IMathExpression expression;
        string inputString;


        public TestCalcValidator()
        {
            factory = new Factory();
            expression = new MathExpression();
            bldValidator = new ExpressionValidator();
            builder = new ExpressionBuilder(factory, bldValidator);
            calcValidator = (ICalcValidator)bldValidator; 
        }

        [Fact]
        public void IsExpressionComplete_4Plus3Mul_False()
        {
            inputString = "4+3*";

            builder.TryParse(expression, inputString);
            var status = calcValidator.IsExpressionComplete(expression);

            Assert.False(status);
        }

        [Fact]
        public void IsExpressionComplete_Ob4Plus5_False()
        {
            inputString = "(4+5";

            builder.TryParse(expression, inputString);
            var status = calcValidator.IsExpressionComplete(expression);

            Assert.False(status);
        }

        [Fact]
        public void IsExpressionComplete_Ob8Mul2CbCb_False()
        {
            inputString = "(8*2))";

            builder.TryParse(expression, inputString);
            var status = calcValidator.IsExpressionComplete(expression);

            Assert.False(status);
        }
    }
}
