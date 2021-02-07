using System;
using System.Collections.Generic;
using System.Text;
using Calculator;
using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using Moq;
using Xunit;

namespace TestCalculator
{
    public class TestMathExpression
    {
        IExpressionBuilder expressionBuilder;
        IMathExpression mathExpression;
        string inputString;


        public TestMathExpression()
        {
            expressionBuilder = new ExpressionBuilder();
            mathExpression = new MathExpression(expressionBuilder);
        }
        
        [Fact]
        // Ob - opening bracket, Cb - closing bracket для экономии длины
        public void Parse_ObOb2Plus3ClMul8CbDiv10_ReturnsTheSame()
        {
            inputString = "((2+3)*8)/10";

            mathExpression.Parse(inputString);
            var elements = mathExpression.GetCollection();

            Assert.Collection(elements, new Action<IExpressionElement>[]
            {
                elem => It.IsAny<IOpeningBracket>(),
                elem => It.IsAny<IOpeningBracket>(),
                elem => It.IsAny<IDynamicNumber>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>(),
                elem => It.IsAny<IClosingBracket>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>(),
                elem => It.IsAny<IClosingBracket>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>(),
            });
        }

        [Fact]
        public void Calc_Input31Plus2Mul7_Returns45()
        {
            inputString = "31+2*7";
            mathExpression.Parse(inputString);
            double result = mathExpression.Calculate();

            Assert.Equal(45, result);
        }

        void AppendElementsFromInputString()
        {
            foreach (var symbol in inputString) mathExpression.Append(symbol);
        }
    }
}
