using Calculator;
using Calculator.Commands.Aryphmetic;
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
        Mock<IDynamicNumber> numberMock;

        public TestExpressionValidator()
        {
            validator = new ExpressionValidator();
            exprMock = new Mock<IMathExpression>();
            numberMock = new Mock<IDynamicNumber>();
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

        [Fact]
        public void CanInsertNaN_ExprEmptyInMul_False()
        {
            exprMock.Setup(m => m.GetCollection())
                .Returns(new List<IExpressionElement>());
            var type = typeof(MultiplyCommand);

            bool result = validator.CanInsertNaN(exprMock.Object, type);

            Assert.False(result);
        }

        [Fact]
        public void CanInsertNaN_ExprNumInCb_True()
        {
            exprMock.Setup(m => m.GetCollection())
                .Returns(new List<IExpressionElement>()
                { new Mock<IDynamicNumber>().Object });
            var type = typeof(IClosingBracket);

            bool result = validator.CanInsertNaN(exprMock.Object, type);

            Assert.True(result);
        }

        [Fact]
        public void CanAppendNumber_NumEmptyInMin_True()
        {
            numberMock.Setup(m => m.StringValue)
                .Returns("");
            var symbol = '-';

            bool result = validator.CanAppendNumber(numberMock.Object, symbol);

            Assert.True(result);
        }

        [Fact]
        public void CanAppendNumber_Num3InMin_False()
        {
            numberMock.Setup(m => m.StringValue)
                .Returns("3");
            var symbol = '-';

            bool result = validator.CanAppendNumber(numberMock.Object, symbol);

            Assert.False(result);
        }

        [Fact]
        public void CanAppendNumber_Num4Dot5InMin_False()
        {
            numberMock.Setup(m => m.StringValue)
                .Returns("4,5");
            var symbol = '-';

            bool result = validator.CanAppendNumber(numberMock.Object, symbol);

            Assert.False(result);
        }

        [Fact]
        public void CanAppendNumber_Num4Dot5InDot_False()
        {
            numberMock.Setup(m => m.StringValue)
                .Returns("4,5");
            var symbol = ',';

            bool result = validator.CanAppendNumber(numberMock.Object, symbol);

            Assert.False(result);
        }

        [Fact]
        public void CanAppendNumber_Num5DotIn7_True()
        {
            numberMock.Setup(m => m.StringValue)
                .Returns("5,");
            var symbol = '7';

            bool result = validator.CanAppendNumber(numberMock.Object, symbol);

            Assert.True(result);
        }
    }
}
