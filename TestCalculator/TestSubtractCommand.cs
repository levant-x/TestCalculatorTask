using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCalculator
{
    public class TestSubtractCommand
    {
        ICommand sumCommand;

        public TestSubtractCommand()
        {
            sumCommand = new SumCommand();
        }


        [Fact]
        public void Exec_Input10and2_Returns8()
        {
            double result = sumCommand.Exec(10, 2);

            Assert.Equal(8, result);
        }
    }
}
