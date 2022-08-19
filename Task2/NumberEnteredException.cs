using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class NumberEnteredException : FormatException
    {
        public NumberEnteredException() { }
        public NumberEnteredException(string message) : base(message) { }
    }
}
