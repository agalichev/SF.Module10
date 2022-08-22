using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Calculator : ICalculator
    {
        public decimal Addition(decimal a, decimal b) { return a + b; }

        public decimal Division(decimal a, decimal b) { return a / b; }

        public decimal Multiplication(decimal a, decimal b) { return a * b; }

        public decimal Subtraction(decimal a, decimal b) { return a - b; }

        public decimal Begin(decimal a, decimal b) { return 0; }

        ILogger Logger { get; }

        public Calculator(ILogger logger)
        {
            Logger = logger;
        }

        public decimal EnterValue()
        {
            string? inputstr = string.Empty;
            decimal number = 0;
            bool check;

            Logger.Event("Введите значение: ");
            inputstr = Console.ReadLine();
            check = !decimal.TryParse(inputstr, out number) | (inputstr == "");

            if (inputstr == "cl")
            {
                throw new Exception("Отмена");
                return 0;
            }
            else if (check || (number < decimal.MinValue) || (number > decimal.MaxValue))
            {
                Logger.Error("Введено некорректное значение");
                throw new NumberEnteredException();
                return 0;
            }
            return number;
        }
    }
}
