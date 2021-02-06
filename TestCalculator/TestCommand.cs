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
        [Fact]
        public void Add_ExecWith2and3_Returns5()
        {
            ICommand sumCommand = new SumCommand();

            double sum = sumCommand.Exec(2, 3);

            Assert.Equal(5, sum);
        }


    }
}
