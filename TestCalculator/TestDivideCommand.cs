using Calculator.Commands.Aryphmetic;
using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestCalculator
{
    public class TestDivideCommand
    {
        ICommand divCommand;

        public TestDivideCommand()
        {
            divCommand = new DivideCommand();
        }


        [Fact]
        public void Exec_Input14and2_Returns7()
        {
            double result = divCommand.Exec(10, 2);

            Assert.Equal(8, result);
        }
    }
}
