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
        public void GetCollection_Empty()
        {
            var exprMock = new Mock<IMathExpression>();
            exprMock.Setup(m => m.GetCollection())
                .Returns(new List<IExpressionElement>());

            var result = exprMock.Object.GetCollection();

            Assert.Empty(result);
        }
    }
}
