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
        public void Exec_Input1and42_Returns0dot25()
        {
            double result = divCommand.Exec(1, 4);

            Assert.Equal(0.25, result);
        }

        [Fact]
        public void Priority_MulPriorityEqualsDivPriority_True()
        {
            ICommand mulCommand = new MultiplyCommand();

            Assert.Equal(mulCommand.Priority, divCommand.Priority);
        }

        [Fact]
        public void Exec_Input100and0_ThrowsDivideByZeroExc()
        {
            Assert.Throws<DivideByZeroException>(() => divCommand.Exec(100, 0));
        }
    }
}
