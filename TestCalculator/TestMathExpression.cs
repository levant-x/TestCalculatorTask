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
        IExpressionBuilder expressionBuilder;
        IMathExpression mathExpression;
        string inputString;


        //public TestMathExpression()
        //{
        //    expressionBuilder = new ExpressionBuilder();
        //    mathExpression = new MathExpression(expressionBuilder);
        //}
        
        //[Fact]
        //// Ob - opening bracket, Cb - closing bracket для экономии длины
        //public void Parse_ObOb2Plus3ClMul8CbDiv10_ReturnsTheSame()
        //{
        //    inputString = "((2+3)*8)/10";

        //    mathExpression.Parse(inputString);
        //    var elements = mathExpression.GetCollection();

        //    Assert.Collection(elements, new Action<IExpressionElement>[]
        //    {
        //        elem => It.IsAny<IOpeningBracket>(),
        //        elem => It.IsAny<IOpeningBracket>(),
        //        elem => It.IsAny<IDynamicNumber>(),
        //        elem => It.IsAny<ICommand>(),
        //        elem => It.IsAny<IDynamicNumber>(),
        //        elem => It.IsAny<IClosingBracket>(),
        //        elem => It.IsAny<ICommand>(),
        //        elem => It.IsAny<IDynamicNumber>(),
        //        elem => It.IsAny<IClosingBracket>(),
        //        elem => It.IsAny<ICommand>(),
        //        elem => It.IsAny<IDynamicNumber>(),
        //    });
        //}

        //[Fact]
        //public void Calc_Input31Plus2Mul7_Returns45()
        //{
        //    inputString = "31+2*7"; 

        //    mathExpression.Parse(inputString);
        //    mathExpression.TryCalculate(out double result);

        //    Assert.Equal(45, result);
        //}

        //[Fact]
        //public void Calc_Input2Plus3Mul4Sub15Div3_Returns9()
        //{
        //    inputString = "2+3*4-15/3";

        //    mathExpression.Parse(inputString);
        //    mathExpression.TryCalculate(out double result);

        //    Assert.Equal(9, result);
        //}

        //[Fact]
        //public void Calc_ObOb2Plus3ClMul8CbDiv10_Returns4()
        //{
        //    inputString = "((2+3)*8)/10";

        //    mathExpression.Parse(inputString);
        //    mathExpression.TryCalculate(out double result);

        //    Assert.Equal(4, result);
        //}
               
        //[Fact]
        //public void Calc_1DivOb3Sub3Cb_ReturnsFalse()
        //{
        //    inputString = "1/(3-3)";

        //    mathExpression.Parse(inputString);
        //    var status = mathExpression.TryCalculate(out double result);

        //    Assert.False(status);
        //}

        //[Fact]
        //public void Calc_ExpressionEndsWithCommand_ReturnsFalse()
        //{
        //    inputString = "4+3*";
        //    mathExpression.Parse(inputString);
        //    var status = mathExpression.TryCalculate(out double result);

        //    Assert.False(status);
        //}

        //[Fact]
        //public void Calc_BracketsUnclosed_ReturnsFalse()
        //{
        //    inputString = "(4+5";
        //    mathExpression.Parse(inputString);
        //    var status = mathExpression.TryCalculate(out double result);

        //    Assert.False(status);
        //}

        //void AppendElementsFromInputString()
        //{
        //    foreach (var symbol in inputString) mathExpression.Append(symbol);
        //}
    }
}
