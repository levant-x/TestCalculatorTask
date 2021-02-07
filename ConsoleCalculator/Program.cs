using System;
using Calculator;
using CalculatorAPI;
using Ninject;

namespace ConsoleCalculator
{
    class Program
    {
        static IMathExpression calculator;
        static double result;

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            Register(kernel);

            calculator = kernel.Get<IMathExpression>();

            while (true) RunMainLoop();
        }

        static void RunMainLoop()
        {
            Console.Clear();
            Console.WriteLine("Введите выражение. " +
                "Нажмите Enter чтобы вычислить\n");

            var expression = Console.ReadLine();
            calculator.Parse(expression);

            var msg = calculator.TryCalculate(out result) ?
                $"Результат = {result}" :
                "Выражение некорректно либо привело к ошибке";
            ShowResultPending(msg);
        }

        static void ShowResultPending(string msg)
        {
            Console.WriteLine($"\n{msg}");
            Console.Write("Нажмите клавишу...");
            Console.ReadKey();
        }

        static void ApplyUserInput(ConsoleKey inputKey)
        {

        }

        public static void Register(IKernel kernel)
        {
            kernel.Bind<IMathExpression>().To<MathExpression>();
            kernel.Bind<IExpressionBuilder>().To<ExpressionBuilder>();
        }
    }
}
