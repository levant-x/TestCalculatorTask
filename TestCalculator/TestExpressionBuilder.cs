﻿using Calculator;
using CalculatorAPI;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCalculator
{
    public class TestExpressionBuilder
    {
        [Fact]
        public void TryAppendElement_Input123_FillsWith123()
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
    }
}