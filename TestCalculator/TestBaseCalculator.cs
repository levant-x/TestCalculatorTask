using Calculator;
using CalculatorAPI;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCalculator
{
    public class TestBaseCalculator
    {
        ICalculator calculator;
        IFactory factory;
        IBuildValidator bldValidator;
        ICalcValidator calcValidator;
        IExpressionBuilder builder;
        IMathExpression expression;
        string inputString;


        public TestBaseCalculator()
        {
            factory = new Factory();
            expression = new MathExpression();
            bldValidator = new ExpressionValidator();
            builder = new ExpressionBuilder(factory, bldValidator);

            var clcValidatorMock = new Mock<ICalcValidator>();
            clcValidatorMock
                .Setup(m => m.IsExpressionComplete(expression))
                .Returns(true);                

            calcValidator = clcValidatorMock.Object;
            calculator = new BaseCalculator(factory, calcValidator);
        }

        [Fact]
        public void Calculate_ObOb2Plus3ClMul8CbDiv10_4()
        {
            inputString = "((2+3)*8)/10";

            builder.TryParse(expression, inputString);
            calculator.TryCalculate(expression, out double result);

            Assert.Equal(4, result);
        }

        [Fact]
        public void Calculate_Input31Plus2Mul7_45()
        {
            inputString = "31+2*7";

            builder.TryParse(expression, inputString);
            calculator.TryCalculate(expression, out double result);

            Assert.Equal(45, result);
        }

        [Fact]
        public void Calculate_Input2Plus3Mul4Sub15Div3_9()
        {
            inputString = "2+3*4-15/3";

            builder.TryParse(expression, inputString);
            calculator.TryCalculate(expression, out double result);

            Assert.Equal(9, result);
        }

        [Fact]
        public void Calculate_1DivOb3Sub3Cb_False()
        {
            inputString = "1/(3-3)";

            builder.TryParse(expression, inputString);
            var status = calculator.TryCalculate(expression, 
                out double result);

            Assert.False(status);
        }

        //[Fact]
        //public void Calculate_ExpressionEndsWithCommand_False()
        //{
        //    inputString = "4+3*";
        //    mathExpression.Parse(inputString);
        //    var status = mathExpression.TryCalculate(out double result);

        //    Assert.False(status);
        //}

        //[Fact]
        //public void Calculate_BracketsUnclosed_ReturnsFalse()
        //{
        //    inputString = "(4+5";
        //    mathExpression.Parse(inputString);
        //    var status = mathExpression.TryCalculate(out double result);

        //    Assert.False(status);
        //}
    }
}
