using Calculator;
using CalculatorAPI;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace TestCalculator
{
    public class TestExpressionBuilder
    {
        [Fact]
        public void TryAppendElement_Input123_FillsWithNum()
        {
            IExpressionBuilder expressionBuilder = new ExpressionBuilder();
            var expressionBody = new List<IExpressionElement>();
            string inputString = "123";

            foreach (var symbol in inputString)
                expressionBuilder.TryAppendElement(expressionBody, symbol);

            Assert.Collection(expressionBody, new Action<IExpressionElement>[]
            {
                elem => It.IsAny<IDynamicNumber>()
            });
        }

        [Fact]
        public void TryAppendElement_Input123_FillsWith123()
        {
            IExpressionBuilder expressionBuilder = new ExpressionBuilder();
            var expressionBody = new List<IExpressionElement>();
            string inputString = "123";

            foreach (var symbol in inputString)
                expressionBuilder.TryAppendElement(expressionBody, symbol);
            var firstElem = (IDynamicNumber)expressionBody.First();

            Assert.Equal(123, firstElem.Value);
        }

        [Fact]
        public void TryAppendElement_Input2Plus3Mul7_FillsWithNumSumNumMulNum()
        {
            IExpressionBuilder expressionBuilder = new ExpressionBuilder();
            var expressionBody = new List<IExpressionElement>();
            string inputString = "2+3*7";

            foreach (var symbol in inputString)
                expressionBuilder.TryAppendElement(expressionBody, symbol);

            Assert.Collection(expressionBody, new Action<IExpressionElement>[]
            {
                elem => It.IsAny<IDynamicNumber>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>()
            });
        }
    }
}
