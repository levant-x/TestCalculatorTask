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
        Mock<IExpressionBuilder> expressionBuilder;
        IMathExpression mathExpression;
        string inputString;


        public TestMathExpression()
        {
            expressionBuilder = new Mock<IExpressionBuilder>();
            mathExpression = new MathExpression(expressionBuilder.Object);
        }

        [Fact]
        public void Append_2Plus3_ReturnsTrue3Times()
        {
            inputString = "2+3";

            AppendElementsFromInputString();

            expressionBuilder.Verify(m => m.TryAppendElement(It.IsAny<ICollection<IExpressionElement>>(),
                It.IsAny<char>()), Times.Exactly(3));
        }

        [Fact]
        public void Append_ObOb2Plus3ClMul8CbDiv10_ReturnsTrue12Times()
        {
            inputString = "((2+3)*8)/10";

            AppendElementsFromInputString();

            expressionBuilder.Verify(m => m.TryAppendElement(It.IsAny<ICollection<IExpressionElement>>(),
                It.IsAny<char>()), Times.Exactly(12));
        }

        void AppendElementsFromInputString()
        {
            foreach (var symbol in inputString) mathExpression.Append(symbol);
        }
    }
}
