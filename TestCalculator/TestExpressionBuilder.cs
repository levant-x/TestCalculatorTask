using Calculator;
using Calculator.Commands.Aryphmetic;
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
        Mock<IMathExpression> exprMock;
        IExpressionBuilder expressionBuilder;
        ICollection<IExpressionElement> expressionBody;
        string inputString;

        public TestExpressionBuilder()
        {
            exprMock = new Mock<IMathExpression>();
            expressionBody = new List<IExpressionElement>();
            expressionBuilder = new ExpressionBuilder();
        }

        [Fact]
        public void TryAppendElement_Input123_FillsWithNum()
        {
            inputString = "123";

            AppendElementsFromInputString();

            Assert.Collection(expressionBody, new Action<IExpressionElement>[]
            {
                elem => It.IsAny<IDynamicNumber>()
            });
        }

        [Fact]
        public void TryAppendElement_Input123_FillsWith123()
        {
            inputString = "123";

            AppendElementsFromInputString();
            var firstElem = (IDynamicNumber)expressionBody.First();

            Assert.Equal(123, firstElem.Value);
        }

        [Fact]
        public void TryAppendElement_Input2Plus3Mul7_FillsWithNumComNumComNum()
        {
            inputString = "2+3*7";

            AppendElementsFromInputString();

            Assert.Collection(expressionBody, new Action<IExpressionElement>[]
            {
                elem => It.IsAny<IDynamicNumber>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IDynamicNumber>()
            });
        }

        [Fact]
        public void TryAppendElement_Input25Plus3dot14Mul70_Returns25Plus3dot14Mul70()
        {
            inputString = "25+3,14*70";

            AppendElementsFromInputString();

            Assert.Collection(expressionBody, new Action<IExpressionElement>[]
            {
                elem => Equals(GetDynamicValue(elem), 25),
                elem => It.IsAny<SumCommand>(),
                elem => Equals(GetDynamicValue(elem), 3.14),
                elem => It.IsAny<MultiplyCommand>(),
                elem => Equals(GetDynamicValue(elem), 70)
            });
        }

        [Fact]
        public void TryAppendElement_InputBrackets_FillsWithBrackets()
        {
            inputString = "(2+3)";

            AppendElementsFromInputString();

            Assert.Collection(expressionBody, new Action<IExpressionElement>[]
            {
                elem => It.IsAny<IOpeningBracket>(),
                elem => It.IsAny<IExpressionElement>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IExpressionElement>(),
                elem => It.IsAny<IClosingBracket>(),
            });
        }

        [Fact]    
        // Ob - opening bracket, Cb - closing bracket для экономии длины
        public void TryAppendElement_ObOb2Plus3ClMul8CbDiv10_FillsWithSame()
        {
            inputString = "((2+3)*8)/10";

            AppendElementsFromInputString();

            Assert.Collection(expressionBody, new Action<IExpressionElement>[]
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

        void AppendElementsFromInputString()
        {
            foreach (var symbol in inputString)
            {
                expressionBuilder.TryAppendElement(expressionBody, symbol);
            }
        }

        double GetDynamicValue(IExpressionElement element)
        {
            return ((IDynamicNumber)element).Value;
        }
    }
}
