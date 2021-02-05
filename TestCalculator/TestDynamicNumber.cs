using Calculator;
using CalculatorAPI;
using Moq;
using System;
using Xunit;

namespace TestCalculator
{
    public class TestDynamicNumber
    {
        [Fact]
        public void Append_Input002Dot71_Returns2Dot71()
        {
            IDynamicNumber number = new DynamicNumber();
            var inputString = "002.71";

            foreach (var symbol in inputString)
                number.Append(symbol);

            Assert.Equal(2.71, number.Value);
        }
    }
}
