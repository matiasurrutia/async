using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Responsivenes
{
    class Fibonacci
    {
        public int N { get; private set; }
        public int Result { get; private set; }

        // Constructor. 
        public Fibonacci(int n)
        {
            N = n;
            Result = 0;
        }

        public int Calculate()
        {
            return Calculate(N);
        }

        /// <summary>
        /// Recursive method that calculates the Nth Fibonacci number.  
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private int Calculate(int n)
        {
            if (n <= 1)
                return n;

            return Calculate(n - 1) + Calculate(n - 2);
        }
    }
}
