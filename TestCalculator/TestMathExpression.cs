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
        [Fact]
        public void Append_2Plus3_ReturnsTrue3Times()
        {
            var expressionBuilder = new Mock<IExpressionBuilder>();            
            IMathExpression mathExpression = new MathExpression(expressionBuilder.Object);
            var collection = new List<IExpressionElement>();
            var expressionString = "2+3";

            foreach (var symbol in expressionString)
                mathExpression.Append(symbol);

            expressionBuilder.Verify(m => m.TryAppendElement(It.IsAny<ICollection<IExpressionElement>>(),
                It.IsAny<char>()), Times.Exactly(3));
        }
    }
}
