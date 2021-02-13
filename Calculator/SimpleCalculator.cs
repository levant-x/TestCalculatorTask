using CalculatorAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class SimpleCalculator : ICalculator
    {
        ICollection<IExpressionElement> elements;
        Stack<IDynamicNumber> deferredNumbers;
        Stack<ICommand> deferredCommands;

        private ICalcValidator calcValidator;
        private IFactory factory;



        public SimpleCalculator(IFactory factory, ICalcValidator calcValidator)
        {
            this.factory = factory;
            this.calcValidator = calcValidator;
            deferredNumbers = new Stack<IDynamicNumber>();
            deferredCommands = new Stack<ICommand>();
        }



        public virtual bool TryCalculate(IMathExpression expression, out double result)
        {
            deferredNumbers.Clear();
            deferredCommands.Clear();
            elements = new List<IExpressionElement>(expression.GetCollection());
            result = default(double);

            if (!calcValidator.IsExpressionComplete(expression))
                return false;
            
            AdjustPrioritiesInBrackets();
            if (!TryDoCalculation()) return false;

            result = deferredNumbers.Pop().Value;
            return true;
        }



        bool TryDoCalculation()
        {
            while (elements.Count > 0 || deferredCommands.Count > 0)
            {
                var elem = elements.Count > 0 ?
                    elements.First() : null;

                if (elem is ICommand && !ApplyCommand((ICommand)elem) ||
                    elem == null && !TryExecLastCommand())
                    return false;

                else if (elem is IDynamicNumber)
                    DeferNumber((IDynamicNumber)elem);
                elements.Remove(elem);
            }
            return true;
        }

        void AdjustPrioritiesInBrackets()
        {
            int bracketsLevel = 0;
            // Команды могут быть разного приоритета по всему выражению,
            // а скобки всегда важнее
            int maxPresentPriority = elements
                .Where(e => e is ICommand)
                .Select(e => ((ICommand)e).Priority)
                .Max();

            foreach (var elem in elements)
            {
                if (elem is ICommand)
                    ((ICommand)elem).Priority += bracketsLevel * maxPresentPriority;
                else if (elem is IBracket) bracketsLevel += (int)((IBracket)elem).Type;
            }
        }

        private void DeferNumber(IDynamicNumber number)
        {
            deferredNumbers.Push(number);
        }

        protected virtual bool ApplyCommand(ICommand command)
        {
            // т.к. обход выражения выполняем слева направо, то сначала
            // операции откладываем на случай, если после них есть более
            // приоритетные; иначе порциями выполняем до текущей
            while (deferredCommands.Count > 0)
            {
                var lastCommand = deferredCommands.Peek();
                if (lastCommand.Priority < command.Priority) break;
                else if (!TryExecLastCommand()) return false;
            }
            if (elements.Count > 0) deferredCommands.Push(command);
            return true;
        }

        protected virtual bool TryExecLastCommand()
        {
            var lastCommand = deferredCommands.Pop();
            var right = deferredNumbers.Pop().Value;
            var left = deferredNumbers.Pop().Value;
            double result = 0;
            try
            {
                result = lastCommand.Exec(left, right);
            }
            catch (Exception)
            {
                return false;
            }
            SaveLastCalcResult(result);
            return true;
        }

        private void SaveLastCalcResult(double result)
        {
            var resultDynamNumber = factory.CreateNumber();
            resultDynamNumber.Value = result;
            DeferNumber(resultDynamNumber);
        }
    }
}