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
    public class TestFactory
    {
        IFactory factory;

        public TestFactory()
        {
            factory = new Factory();
        }


        [Fact]
        public void Create_Sum_SumCommand()
        {
            char symbol = '+';
         
            IExpressionElement result = factory.CreateNaN(symbol);

            Assert.IsType<SumCommand>(result);
        }

        [Fact]
        public void ResolveType_Div_TypeOfDiv()
        {
            char symbol = '/';

            Type result = factory.ResolveType(symbol);

            Assert.True(result == typeof(DivideCommand));
        }

        [Fact]
        public void CreateNumber_Number()
        {
            var result = factory.CreateNumber();

            Assert.IsType<DynamicNumber>(result);
        }
    }
}
