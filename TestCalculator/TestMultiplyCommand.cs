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
        ICommand multiplyCommand;

        public TestMultiplyCommand()
        {
            multiplyCommand = new MultiplyCommand();
        }


        [Fact]
        public void Exec_Input4and5_Returns20()
        {    
            double result = multiplyCommand.Exec(4, 5);

            Assert.Equal(20, result);
        }


        [Fact]
        public void Priority_MulPriorityHighterThanSumPriority_True()
        {
            ICommand sumCommand = new SumCommand();

            bool result = sumCommand.Priority > sumCommand.Priority;

            Assert.True(result);
        }
    }
}
