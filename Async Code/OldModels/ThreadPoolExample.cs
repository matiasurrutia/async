//src: http://msdn.microsoft.com/en-us/library/3dasc8as.aspx

using System;
using System.Threading;

namespace OldModels
{
    public class Fibonacci
    {
        private int n;
        private int fibOfN;
        private ManualResetEvent doneEvent;

        public int N { get { return n; } }
        public int FibOfN { get { return fibOfN; } }

        // Constructor. 
        public Fibonacci(int n, ManualResetEvent doneEvent)
        {
            this.n = n;
            this.doneEvent = doneEvent;
        }

        // Wrapper method for use with thread pool. 
        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            Console.WriteLine("thread {0} started...", threadIndex);
            fibOfN = Calculate(n);
            Console.WriteLine("thread {0} result calculated...", threadIndex);
            doneEvent.Set();
        }

        // Recursive method that calculates the Nth Fibonacci number. 
        public int Calculate(int n)
        {
            if (n <= 1)
            {
                return n;
            }

            return Calculate(n - 1) + Calculate(n - 2);
        }
    }

    public class ThreadPoolExample
    {
        public void Main()
        {
            const int FibonacciCalculations = 10;

            // One event is used for each Fibonacci object.
            var doneEvents = new ManualResetEvent[FibonacciCalculations];
            var fibArray = new Fibonacci[FibonacciCalculations];
            var r = new Random();

            // Configure and start threads using ThreadPool.
            Console.WriteLine("launching {0} tasks...", FibonacciCalculations);
            for (int i = 0; i < FibonacciCalculations; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                var fibonacci = new Fibonacci(r.Next(20, 40), doneEvents[i]);

                fibArray[i] = fibonacci;
                ThreadPool.QueueUserWorkItem(fibonacci.ThreadPoolCallback, i);
            }

            // Wait for all threads in pool to calculate.
            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("All calculations are complete.");

            // Display the results. 
            for (int i = 0; i < FibonacciCalculations; i++)
            {
                Fibonacci f = fibArray[i];

                Console.WriteLine("Fibonacci({0}) = {1}", f.N, f.FibOfN);
            }
        }
    }
}
