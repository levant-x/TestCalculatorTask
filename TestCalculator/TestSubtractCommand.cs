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
        ICommand subtCommand;

        public TestSubtractCommand()
        {
            subtCommand = new SubtractCommand();
        }


        [Fact]
        public void Exec_Input10and2_Returns8()
        {
            double result = subtCommand.Exec(10, 2);

            Assert.Equal(8, result);
        }


        [Fact]
        public void Priority_MulPriorityHighterThanSumPriority_True()
        {
            ICommand sumCommand = new SumCommand();
                        
            Assert.Equal(subtCommand.Priority, sumCommand.Priority);
        }
    }
}
