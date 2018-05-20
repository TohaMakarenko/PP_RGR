using RGR_first;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PerfomanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Directory.Exists("resul"))
                Directory.Delete("resul", true);
            Directory.CreateDirectory("resul");

            Console.WriteLine("WARM UP N: 1000 affinity: 1");
            RunAll(1000, 1, (IntPtr)15, 5);
            Console.WriteLine("N: 900 affinity: 1");
            RunAll(900, 1, (IntPtr)1, 5);
            Console.WriteLine("N: 900 affinity: 2");
            RunAll(900, 2, (IntPtr)3, 5);
            Console.WriteLine("N: 900 affinity: 3");
            RunAll(900, 3, (IntPtr)7, 5);
            Console.WriteLine("N: 900 affinity: 4");
            RunAll(900, 4, (IntPtr)15, 5);

            Console.WriteLine("N: 1800 affinity: 1");
            RunAll(1800, 1, (IntPtr)1);
            Console.WriteLine("N: 1800 affinity: 2");
            RunAll(1800, 2, (IntPtr)3);
            Console.WriteLine("N: 1800 affinity: 3");
            RunAll(1800, 3, (IntPtr)7);
            Console.WriteLine("N: 1800 affinity: 4");
            RunAll(1800, 4, (IntPtr)15);

            Console.WriteLine("N: 2400 affinity: 1");
            RunAll(2400, 1, (IntPtr)1);
            Console.WriteLine("N: 2400 affinity: 2");
            RunAll(2400, 2, (IntPtr)3);
            Console.WriteLine("N: 2400 affinity: 3");
            RunAll(2400, 3, (IntPtr)7);
            Console.WriteLine("N: 2400 affinity: 4");
            RunAll(2400, 4, (IntPtr)15);
        }

        static void RunAll(int size, int number, IntPtr affinity, int workNumber = 3, int warmUpNumber = 0)
        {
            FileStream fs = new FileStream($@".\resul\result{size}_{number}.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.AutoFlush = true;
            Process.GetCurrentProcess().ProcessorAffinity = affinity;
            Console.SetOut(sw);

            Console.WriteLine(new string('=', 30));
            Console.WriteLine($"started at {DateTime.Now}");
            Console.WriteLine(new string('=', 30));
            var stopwatch = new Stopwatch();
            Console.WriteLine("warm up started");
            for (int i = 0; i < warmUpNumber; i++) {
                RunOne(size);
            }
            Console.WriteLine("warm up finished");
            Console.WriteLine();
            Console.WriteLine("Work started");
            long avarage = 0;

            for (int i = 0; i < workNumber; i++) {
                Console.WriteLine($"work {i}");
                stopwatch.Reset();
                stopwatch.Start();
                RunOne(size);
                stopwatch.Stop();
                avarage += stopwatch.ElapsedMilliseconds;
                Console.WriteLine($"work #{i}: Elapsed time: {stopwatch.ElapsedMilliseconds} ms " +
                    $"({stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds})");
                Console.WriteLine();
            }
            Console.WriteLine(new string('=', 30));
            Console.WriteLine($"Elapsed avarage: {avarage / workNumber}");
            Console.WriteLine(new string('=', 30));

            sw.Close();
            ResetConsoleOut();
        }

        static void ResetConsoleOut()
        {
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
        }

        static void RunOne(int size)
        {
            Data data = new Data(size);
            var t1 = new Thread(data.Task1);
            var t2 = new Thread(data.Task2);
            var t3 = new Thread(data.Task3);
            var t4 = new Thread(data.Task4);
            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t1.Join();
        }
    }
}
