namespace Task1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            decimal a = 0;
            decimal b = 0;
            decimal? result;
            bool shutdown;
            int x = 0;
            int y = 0;

            Calculator calculator = new Calculator();
            Func<decimal, decimal, decimal> operationDelegate;

            do
            {
                try
                {
                    Console.WriteLine(" Отмена операции или завершение работы калькулятора [ c ]\n");
                    Console.Write("Введите 1-е число: ");
                    a = EnterValue();
          
                    do
                    {
                        Console.WriteLine();
                        try
                        {
                            Console.WriteLine(" '+' - сложение\n '-' - вычитание\n '*' - умножение\n '/' - деление\n\n 'r' - сброс\n");
                            Console.Write($"Выберете операцию: ");

                            switch (Console.ReadLine())
                            {
                                case "+":
                                    operationDelegate = calculator.Addition;
                                    break;

                                case "-":
                                    operationDelegate = calculator.Subtraction;
                                    break;

                                case "*":
                                    operationDelegate = calculator.Multiplication;
                                    break;

                                case "/":
                                    operationDelegate = calculator.Division;
                                    break;

                                case "r":
                                    operationDelegate = null;
                                    throw new Exception("Cброс");
                                    break;

                                default:
                                    Console.WriteLine("Несуществующая операция!");
                                    continue;
                            }

                            Console.WriteLine();
                            Console.Write("Введите 2-е число: ");

                            b = EnterValue();
                            result = operationDelegate?.Invoke(a, b);
                            a = result ?? a;
                        }
                        catch (NumberEnteredException ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine();
                            Console.WriteLine("Нажмите любую клавишу для продолжения");
                            Console.ReadKey();
                        }
                        catch (DivideByZeroException ex)
                        {
                            Console.WriteLine($"Деление на ноль недопутимо. Ошибка: {ex.Message}");
                            Console.WriteLine(ex);
                        }
                        catch (Exception ex) when (ex.Message == "Отмена")
                        {
                            Console.WriteLine("Отмена операции");
                        }
                        catch (Exception ex) when (ex.Message == "Cброс")
                        {
                            Console.WriteLine(ex.Message);
                            break;
                        }
                        finally
                        {
                            Console.WriteLine($"\n= {a}");
                        }
                    }
                    while (true);
                    Console.Clear();
                }
                catch (NumberEnteredException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                    Console.WriteLine("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                }
                catch (Exception ex) when (ex.Message == "Отмена")
                {
                    //Console.WriteLine(ex.Message);
                    break;
                }
            }
            while (true);
            
            Console.WriteLine("Завершение работы");
            Console.ReadKey();
        }

        static decimal EnterValue()
        {
            string? inputstr = string.Empty;
            decimal number = 0;
            bool check;

            inputstr = Console.ReadLine();
            check = !decimal.TryParse(inputstr, out number) | (inputstr == "");

            if (inputstr == "c")
            {
                throw new Exception("Отмена");
                return 0;
            }       
            else if (check || (number < decimal.MinValue) || (number > decimal.MaxValue))
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
        decimal Begin(decimal a, decimal b);
    }

    public class Calculator : ICalculator
    {
        public decimal Addition(decimal a, decimal b) { return a + b; }

        public decimal Division(decimal a, decimal b) { return a / b; }

        public decimal Multiplication(decimal a, decimal b) { return a * b; }

        public decimal Subtraction(decimal a, decimal b) { return a - b; }
        public decimal Begin(decimal a, decimal b) { return 0; }

    }

    public class NumberEnteredException : FormatException
    {
        public NumberEnteredException(string message) : base(message) { }
    }
}