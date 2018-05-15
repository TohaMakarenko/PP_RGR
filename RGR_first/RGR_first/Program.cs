using System;
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

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t1.IsBackground = false;
            Console.ReadLine();
        }
    }
}
