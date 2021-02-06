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

            AppendCharsToDynamicNumber(inputString);

            Assert.Equal(123, number.Value);
        }     
        
        [Fact]
        public void Append_Input2dot7dot1_Returns2dot71()
        {
            var inputString = "2.7.1";

            AppendCharsToDynamicNumber(inputString);

            Assert.Equal(2.71, number.Value);
        }


        private void AppendCharsToDynamicNumber(string inputString)
        {
            foreach (var symbol in inputString) number.Append(symbol);
        }
    }
}
