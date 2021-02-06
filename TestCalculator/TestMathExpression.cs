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
        public void Append_2Plus3_ReturnsNumSumNum()
        {
            var mathExpressionBuilder = new Mock<IExpressionBuilder>();
            IMathExpression mathExpression = new MathExpression(mathExpressionBuilder.Object);
            var expressionString = "2+3";

            foreach (var symbol in expressionString)
                mathExpression.Append(symbol);
            IEnumerable<IExpressionElement> result = mathExpression.GetCollection();

            Assert.Collection(result, new Action<IExpressionElement>[]
            {
                elem => It.IsAny<IDynamicNumber>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>()
            });
        }
    }
}
