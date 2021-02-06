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
    public class TestSumCommand
    {
        ICommand sumCommand;

        public TestSumCommand()
        {
            sumCommand = new SumCommand();
        }


        [Fact]
        public void Exec_Input2and3_Returns5()
        {
            double sum = sumCommand.Exec(2, 3);

            Assert.Equal(5, sum);
        }


        [Fact]
        public void Operands_Returns2()
        {
            int operands = sumCommand.Operands;

            Assert.Equal(2, operands);
        }

        [Fact]
        public void Priority_Returns1()
        {
            int priority = sumCommand.Priority;

            Assert.Equal(1, priority);
        }
    }
}
