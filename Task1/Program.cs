namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal a = 0;
            decimal b = 0;
            decimal? result;

            Calculator calculator = new Calculator();
            do
            {
                try
                {
                    Console.Write("Введите 1-е число: ");
                    a = GetNumber();

                    //do
                    //{
                    //    Console.Write("Введите 2-е число: ");
                    //    b = GetNumber();

                    //    try
                    //    {
                    //        Console.WriteLine("Выберете операцию:\n '+' - сложение\n '-' - вычитание\n '*' - умножение\n '/' - деление\n 'c' - очистить");

                    //        switch (Console.ReadLine())
                    //        {
                    //            case "+":
                    //                calculator.Addition(a, b);
                    //                break;

                    //            case "-":
                    //                calculator.Subtraction(a, b);

                    //        }
                    //        Console.Write("Введите 2-е число: ");
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                    //    finally
                    //    {
                    //        Console.WriteLine();
                    //        Console.WriteLine("Нажмите любую клавишу для продолжения");
                    //        Console.ReadKey();
                    //    }
                    //}
                    //while (true);

                }
                catch (NumberEnteredException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                }
                catch (Exception ex) when (ex.Message == "Завершение работы")
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
            while (true);   
        }

        static decimal GetNumber()
        {
            string? inputstr = string.Empty;
            decimal number = 0;
            bool check;

            inputstr = Console.ReadLine();
            check = !decimal.TryParse(inputstr, out number) || (inputstr == "");
            if (inputstr == "q")
            {
                throw new Exception("Завершение работы");
                return 0;
            }
            
            else if (!check && ((number < decimal.MinValue) || (number > decimal.MaxValue)))
            {
                throw new NumberEnteredException("Введено некорректное число");
                return 0;
            }
            return number;
        }

    }

    public interface ICalculator
    {
        decimal Addition(decimal a, decimal b);
        decimal Subtraction(decimal a, decimal b);
        decimal Multiplication(decimal a, decimal b);
        decimal Division(decimal a, decimal b);
    }

    public class Calculator : ICalculator
    {
        public decimal Addition(decimal a, decimal b) { return a + b; }

        public decimal Division(decimal a, decimal b) { return a / b; }

        public decimal Multiplication(decimal a, decimal b) { return a * b; }

        public decimal Subtraction(decimal a, decimal b) { return a - b; }

    }

    public class NumberEnteredException : FormatException
    {
        public NumberEnteredException(string message) : base(message) { }
    }
}