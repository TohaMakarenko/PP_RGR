using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace RGR_first
{
    // a = mах(MB*MC+ ММ)
    class Data
    {
        private int size;

        private int[][] MB, MC, MM;
        private int a = int.MinValue;
        private object locker = new object();

        private int Max
        {
            set {
                lock (locker) {
                    if (value > a)
                        a = value;
                }
            }
            get {
                return a;
            }
        }

        private Semaphore S121 = new Semaphore(0, 1),
            S231 = new Semaphore(0, 1),
            S341 = new Semaphore(0, 1),
            S411 = new Semaphore(0, 1),
            S211 = new Semaphore(0, 1),
            S412 = new Semaphore(0, 1);

        public Data(int size)
        {
            this.size = size;
        }

        public void Task1()
        {
            Console.WriteLine("Task1 started");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            MM = Utils.GetMatrixOfOne(size);
            S411.WaitOne();
            S121.Release();

            Max = MBxMCplusMMmaxPart(MB, MC, MM, 0, MB.Length / 4);

            S412.WaitOne();
            S211.WaitOne();

            stopwatch.Stop();

            Console.WriteLine("Task1 finished");

            Console.WriteLine($"a = {Max}");
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms " +
                $"({stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds})");
        }

        public void Task2()
        {
            Console.WriteLine("Task2 started");
            MB = Utils.GetMatrixOfOne(size);
            S121.WaitOne();
            S231.Release();

            Max = MBxMCplusMMmaxPart(MB, MC, MM, MB.Length / 4, MB.Length / 2);
            S211.Release();
            Console.WriteLine("Task2 finished");
        }

        public void Task3()
        {
            Console.WriteLine("Task3 started");
            S231.WaitOne();

            Max = MBxMCplusMMmaxPart(MB, MC, MM, MB.Length / 2, 3 * MB.Length / 4 );
            S341.Release();
            Console.WriteLine("Task3 finished");
        }

        public void Task4()
        {
            Console.WriteLine("Task4 started");
            MC = Utils.GetMatrixOfOne(size);
            S411.Release();
            S341.WaitOne();

            Max = MBxMCplusMMmaxPart(MB, MC, MM, 3 * MB.Length / 4, MB.Length);

            S412.Release();
            Console.WriteLine("Task4 finished");
        }

        public int MBxMCplusMMmaxPart(int[][] MB, int[][] MC, int[][] MM, int from, int to)
        {
            int max = int.MinValue;
            int t1;
            for (int k = 0; k < MB.Length; k++) {
                for (var i = from; i < to; i++) {
                    int t = 0;
                    for (int j = 0; j < MB.Length; j++)
                        t += MB[i][j] * MC[j][k];
                    t1 = MM[i][k] + t;
                    if (t1 > max)
                        max = t1;
                }
            }
            return max;
        }

        
    }
}
