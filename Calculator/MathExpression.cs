using System;
using System.Collections.Generic;
using System.Linq;
using CalculatorAPI;

namespace Calculator
{
    public class MathExpression : IMathExpression
    {
        Stack<IDynamicNumber> deferredNumbers;
        Stack<ICommand> deferredCommands;
        double thisReslt;

        ICollection<IExpressionElement> elements;
        private IExpressionBuilder expressionBuilder;


        public MathExpression(IExpressionBuilder expressionBuilder)
        {
            this.expressionBuilder = expressionBuilder;
            elements = new List<IExpressionElement>();

            deferredNumbers = new Stack<IDynamicNumber>();
            deferredCommands = new Stack<ICommand>();
        }
        
        public bool Append(char symbol)
        {
            return expressionBuilder.TryAppendElement(elements, symbol);
        }

        public ICollection<IExpressionElement> GetCollection()
        {
            return elements;
        }

        public bool Parse(string inputString)
        {
            foreach (var symbol in inputString)
            {
                if (Append(symbol)) continue;
                elements.Clear();
                return false;
            }
            return true;
        }

        public bool TryCalculate(out double result)
        {
            result = thisReslt;
            
            if (!IsExpressionComplete()) return false;
            if (!Helper.AreBracketsCorrect(elements)) return false;

            Helper.AdjustPrioritiesInBrackets(elements);
            while (elements.Count > 0 || deferredCommands.Count > 0)
            {                
                var elem = elements.Count > 0 ? 
                    elements.First() : null;
                                
                if (elem is ICommand && !ApplyCommand((ICommand)elem) ||
                    elem == null && !TryExecLastCommand())
                {
                    FinalizeCalculation();
                    return false;
                }
                else if (elem is IDynamicNumber) DeferNumber((IDynamicNumber)elem);
                elements.Remove(elem);
            }
            FinalizeCalculation();
            result = thisReslt;
            return true;
        }

        private bool IsExpressionComplete()
        {
            if (elements.Count == 0) return false;

            var lastElement = elements.Last();
            return lastElement is IDynamicNumber || lastElement is IClosingBracket;
        }        
        
        private void DeferNumber(IDynamicNumber number)
        {
            deferredNumbers.Push(number);
        }

        private bool ApplyCommand(ICommand command)
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

        private bool TryExecLastCommand()
        {
            var lastCommand = deferredCommands.Pop();
            var right = deferredNumbers.Pop().Value;
            var left = deferredNumbers.Pop().Value;

            try
            {
                // Локализация одной только опасной операции
                thisReslt = lastCommand.Exec(left, right);
            }
            catch (Exception)
            {
                return false;
            }
            SaveLastCalcResult();
            return true;
        }

        private void SaveLastCalcResult()
        {
            var dynamNumberType = Helper.ResolveElement('0');
            var resultDynamNumber = 
                (IDynamicNumber)Activator.CreateInstance(dynamNumberType);
            resultDynamNumber.Value = thisReslt;
            DeferNumber(resultDynamNumber);
        }

        private void FinalizeCalculation()
        {
            elements.Clear();
            deferredNumbers.Clear();
            deferredCommands.Clear();
        }
    }
}