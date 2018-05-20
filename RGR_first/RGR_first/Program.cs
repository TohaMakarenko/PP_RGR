using System;
using System.Diagnostics;
using System.Threading;

namespace RGR_first
{
    class Program
    {
        // a = mах(MB*MC+ ММ)
        static void Main(string[] args)
        {
            Console.WriteLine("a = mах(MB*MC+ ММ)");

            Data data = new Data(100);

            var t1 = new Thread(data.Task1);
            var t2 = new Thread(data.Task2);
            var t3 = new Thread(data.Task3);
            var t4 = new Thread(data.Task4);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t1.Join();
            stopwatch.Stop();
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms " +
                $"({stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds})");
            Console.ReadLine();
        }
    }
}
