using Calculator;
using CalculatorAPI;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCalculator
{
    public class TestExpressionValidator
    {
        IExpressionValidator validator;
        Mock<IMathExpression> exprMock;

        public TestExpressionValidator()
        {
            validator = new ExpressionValidator();
            exprMock = new Mock<IMathExpression>();
        }

        [Fact]
        public void CanInsertNumber_ExprEmpty_True()
        {
            exprMock.Setup(m => m.GetCollection())
                .Returns(new List<IExpressionElement>());
            
            bool result = validator.CanInsertNumber(exprMock.Object);

            Assert.True(result);
        }

        [Fact]
        public void CanInsertNumber_ExprCb_False()
        {
            exprMock.Setup(m => m.GetCollection())
                .Returns(new List<IExpressionElement>()
                { new Mock<IClosingBracket>().Object });

            bool result = validator.CanInsertNumber(exprMock.Object);

            Assert.False(result);
        }

        [Fact]
        public void CanInsertNumber_ExprCmd_True()
        {
            exprMock.Setup(m => m.GetCollection())
                .Returns(new List<IExpressionElement>()
                { new Mock<ICommand>().Object });

            bool result = validator.CanInsertNumber(exprMock.Object);

            Assert.True(result);
        }
    }
}
