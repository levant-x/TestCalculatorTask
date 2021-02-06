using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using CalculatorAPI;
using Calculator;
using Calculator.Commands.Aryphmetic;

namespace TestCalculator
{
    public class TestCommand
    {
        ICommand sumCommand;

        public TestCommand()
        {
            sumCommand = new SumCommand();
        }


        [Fact]
        public void Add_ExecWith2and3_Returns5()
        {
            double sum = sumCommand.Exec(2, 3);

            Assert.Equal(5, sum);
        }


        [Fact]
        public void Add_Operands_Returns2()
        {
            int operands = sumCommand.Operands;

            Assert.Equal(2, operands);
        }
    }
}
