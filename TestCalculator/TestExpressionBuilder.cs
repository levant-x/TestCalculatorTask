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
        IMathExpression expression;
        IExpressionBuilder expressionBuilder;
        ICollection<IExpressionElement> exprElems;
        IFactory factory;
        IExpressionValidator validator;
        string inputString;



        public TestExpressionBuilder()
        {
            exprMock = new Mock<IMathExpression>();
            exprElems = new List<IExpressionElement>();
            validator = new ExpressionValidator();
            factory = new Factory();
            expressionBuilder = new ExpressionBuilder(factory, validator);
            expression = new MathExpression(expressionBuilder);
        }



        [Fact]
        public void TryAppendElement_ExprEmptyInput5_True()
        {
            SetupExprMock();

            var result = ExecuteBuilderWith('5');

            Assert.True(result);
        }

        [Fact]
        public void TryAppendElement_Expr12Input3_True()
        {
            exprElems.Add(GetMockedNumber("12"));
            SetupExprMock();
                        
            var result = ExecuteBuilderWith('3');

            Assert.True(result);
        }

        [Fact]
        public void TryAppendElement_ExprEmptyInputMin_True()
        {
            SetupExprMock();

            var result = ExecuteBuilderWith('-');

            Assert.True(result);
        }

        [Fact]
        public void TryAppendElement_Expr2Cmd3CmdInput7_True()
        {
            exprElems.Add(GetMockedNumber("2"));
            exprElems.Add(GetMockedCommand());
            exprElems.Add(GetMockedNumber("3"));
            exprElems.Add(GetMockedCommand());
            SetupExprMock();

            var result = ExecuteBuilderWith('7');

            Assert.True(result);
        }

        [Fact]
        public void TryAppendElement_In25Plus3dot14Mul70_FillsTheSame()
        {
            inputString = "25+3,14*70";

            AppendElementsToConcreteExpression();
            exprElems = expression.GetCollection();

            Assert.Collection(exprElems, new Action<IExpressionElement>[]
            {
                elem => Equals(GetDynamicValue(elem), 25),
                elem => It.IsAny<SumCommand>(),
                elem => Equals(GetDynamicValue(elem), 3.14),
                elem => It.IsAny<MultiplyCommand>(),
                elem => Equals(GetDynamicValue(elem), 70)
            });
        }

        [Fact]
        public void TryAppendElement_InOb2Plus3Cb_FillsTheSame()
        {
            inputString = "(2+3)";

            AppendElementsToConcreteExpression();
            exprElems = expression.GetCollection();

            Assert.Collection(exprElems, new Action<IExpressionElement>[]
            {
                elem => It.IsAny<IOpeningBracket>(),
                elem => It.IsAny<IExpressionElement>(),
                elem => It.IsAny<ICommand>(),
                elem => It.IsAny<IExpressionElement>(),
                elem => It.IsAny<IClosingBracket>(),
            });
        }

        [Fact]   
        public void TryAppendElement_ObOb2Plus3ClMul8CbDiv10_FillsTheSame()
        {
            inputString = "((2+3)*8)/10";

            AppendElementsToConcreteExpression();
            exprElems = expression.GetCollection();

            Assert.Collection(exprElems, new Action<IExpressionElement>[]
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

        void AppendElementsToConcreteExpression()
        {
            foreach (var symbol in inputString)
                expressionBuilder.TryAppendElement(expression,
                    symbol);
        }

        void SetupExprMock()
        {
            exprMock.Setup(m => m.GetCollection())
                .Returns(exprElems);
        }

        IDynamicNumber GetMockedNumber(string value)
        {
            var numberMock = new Mock<IDynamicNumber>();
            numberMock.Setup(m => m.StringValue).Returns(value);
            return numberMock.Object;
        }

        ICommand GetMockedCommand()
        {
            return new Mock<ICommand>().Object;
        }

        bool ExecuteBuilderWith(char value)
        {
            return expressionBuilder.TryAppendElement(
                exprMock.Object, value);
        }

        double GetDynamicValue(IExpressionElement element)
        {
            return ((IDynamicNumber)element).Value;
        }
    }
}
