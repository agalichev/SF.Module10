using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public interface ICalculator
    {
        decimal Addition(decimal a, decimal b);
        decimal Subtraction(decimal a, decimal b);
        decimal Multiplication(decimal a, decimal b);
        decimal Division(decimal a, decimal b);
        decimal Begin(decimal a, decimal b);
    }
}
