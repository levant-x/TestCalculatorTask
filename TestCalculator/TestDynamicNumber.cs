using Calculator;
using CalculatorAPI;
using Moq;
using System;
using Xunit;

namespace TestCalculator
{
    public class TestDynamicNumber
    {
        readonly IDynamicNumber number;

        public TestDynamicNumber()
        {
            number = new DynamicNumber();
        }


        [Fact]
        public void Append_Input123_Returns123()
        {
            var inputString = "123";

            foreach (var symbol in inputString) number.Append(symbol);

            Assert.Equal(123, number.Value);
        }        
    }
}
