namespace Task2
{
    internal class Program
    {
        static ILogger Logger { get; set; }
        static void Main(string[] args)
        {
            Logger = new Logger();
            decimal a = 0;
            decimal b = 0;
            decimal? result;

            Calculator calculator = new Calculator(Logger);
            Func<decimal, decimal, decimal> operationDelegate;

            do
            {
                try
                {
                    Logger.Info("Отмена операции или завершение работы калькулятора [ cl ]\n");
                    a = calculator.EnterValue();

                    do
                    {
                        Console.WriteLine();
                        try
                        {
                            Logger.Info(" '+' - сложение\n '-' - вычитание\n '*' - умножение\n '/' - деление\n\n 'r' - сброс\n");
                            Logger.Event($"Выберете операцию: ");

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
                                    throw new Exception();
                                    break;

                                default:
                                    Logger.Error("Несуществующая операция!");
                                    continue;
                            }

                            Console.WriteLine();

                            b = calculator.EnterValue();
                            result = operationDelegate?.Invoke(a, b);
                            a = result ?? a;
                        }
                        catch (NumberEnteredException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (DivideByZeroException ex)
                        {
                            Logger.Error($"Деление на ноль недопутимо.\nОшибка: {ex}");
                        }
                        catch (Exception ex) when (ex.Message == "Отмена")
                        {
                            Logger.Event("Отмена операции");
                        }
                        catch (Exception)
                        {
                            Logger.Event("Происходит сброс, подождите\n");
                            Thread.Sleep(1000);
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
                    Console.WriteLine($"{ex.Message}\n");
                }
                catch (Exception ex) when (ex.Message == "Отмена")
                {
                    break;
                }
            }
            while (true);

            Logger.Event("Завершение работы");
            Console.ReadKey();
        }
    }
}