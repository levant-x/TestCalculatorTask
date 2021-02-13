using System;
using Calculator;
using CalculatorAPI;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace ConsoleCalculator
{
    class Program
    {
        static ICalculator calculator;
        static IMathExpression expression;
        static IExpressionBuilder expressionBuilder;

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            Register(kernel);

            calculator = kernel.Get<ICalculator>();
            expression = kernel.Get<IMathExpression>();
            expressionBuilder = kernel.Get<IExpressionBuilder>();
            while (true) RunMainLoop();
        }

        static void RunMainLoop()
        {
            Console.Clear();
            Console.WriteLine("Введите выражение. Нажмите Enter чтобы вычислить\n");
            
            expression.Clear();
            if (!GetParseUserInput()) return;
            TryCalculateUserInput();            
        }

        static bool GetParseUserInput()
        {
            var inputString = Console.ReadLine();
            if (!expressionBuilder.TryParse(expression, inputString))
            {
                ShowResultPending("Выражение некорректно");
                return false;
            }
            return true;
        }

        static void TryCalculateUserInput()
        {
            var msg = calculator.TryCalculate(expression, 
                out double result) ? $"Результат = {result}" :
                "Выражение некорректно либо привело к ошибке";
            ShowResultPending(msg);
        }

        static void ShowResultPending(string msg)
        {
            Console.WriteLine($"\n{msg}");
            Console.Write("Нажмите клавишу...");
            Console.ReadKey();
        }

        static void Register(IKernel kernel)
        {
            kernel.Bind<IFactory>().To<Factory>();
            kernel.Bind<ICalculator>().To<SimpleCalculator>();
            kernel.Bind<IExpressionBuilder>().To<ExpressionBuilder>();
            kernel.Bind<IBuildValidator>().To<ExpressionValidator>();
            kernel.Bind<ICalcValidator>().To<ExpressionValidator>();
            kernel.Bind<IMathExpression>().To<MathExpression>()
                .InSingletonScope();
        }
    }
}
