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
        // Ob - opening bracket, Cb - closing bracket для экономии длины
        public void Parse_ObOb2Plus3ClMul8CbDiv10_ReturnsTrue12Times()
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
                elem => It.IsAny<IClosingBracket>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>(),
            });
        }


        public void Parse_7Mul4ObOb2Sub8CbMul3Cb_ReturnsTheSame()
        {

        }

        void AppendElementsFromInputString()
        {
            foreach (var symbol in inputString) mathExpression.Append(symbol);
        }
    }
}
