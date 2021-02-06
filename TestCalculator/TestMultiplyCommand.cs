using Calculator;
using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCalculator
{
    public class TestMultiplyCommand
    {
        [Fact]
        public void Exec_Input4and5_Returns20()
        {
            ICommand multiplyCommand = new MultiplyCommand();

            double result = multiplyCommand.Exec(4, 5);

            Assert.Equal(20, result);
        }
    }
}
