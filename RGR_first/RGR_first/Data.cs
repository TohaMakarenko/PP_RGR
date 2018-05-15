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

        private int[][] MB, mc, MM;
        private int a = int.MinValue;
        private object aLocker = new object();
        private object mcLocker = new object();

        private int[][] MC
        {
            get {
                lock (mcLocker) {
                    return mc;
                }
            }
            set {
                mc = value;
            }
        }

        private int A
        {
            set {
                lock (aLocker) {
                    if (value > a)
                        a = value;
                }
            }
            get {
                return a;
            }
        }

        private Semaphore inputMM = new Semaphore(0, 3),
            inputMB = new Semaphore(0, 3),
            inputMC = new Semaphore(0, 3),
            a2 = new Semaphore(0, 1),
            a3 = new Semaphore(0, 1),
            a4 = new Semaphore(0, 1);

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
            inputMM.Release(3);
            inputMC.WaitOne();
            inputMB.WaitOne();
            var MC1 = MC;
            A = MBxMCplusMMmaxPart(MB, MC1, MM, 0, MB.Length / 4);

            a2.WaitOne();
            a3.WaitOne();
            a4.WaitOne();

            stopwatch.Stop();

            Console.WriteLine("Task1 finished");

            Console.WriteLine($"a = {A}");
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms " +
                $"({stopwatch.Elapsed.Minutes}:{stopwatch.Elapsed.Seconds}.{stopwatch.Elapsed.Milliseconds})");
        }

        public void Task2()
        {
            Console.WriteLine("Task2 started");
            MB = Utils.GetMatrixOfOne(size);
            inputMB.Release(3);
            inputMC.WaitOne();
            inputMM.WaitOne();
            var MC2 = MC;
            A = MBxMCplusMMmaxPart(MB, MC2, MM, MB.Length / 4, MB.Length / 2);
            a2.Release();
            Console.WriteLine("Task2 finished");
        }

        public void Task3()
        {
            Console.WriteLine("Task3 started");
            inputMB.WaitOne();
            inputMC.WaitOne();
            inputMM.WaitOne();
            var MC3 = MC;
            A = MBxMCplusMMmaxPart(MB, MC3, MM, MB.Length / 2, 3 * MB.Length / 4);
            a3.Release();
            Console.WriteLine("Task3 finished");
        }

        public void Task4()
        {
            Console.WriteLine("Task4 started");
            MC = Utils.GetMatrixOfOne(size);
            inputMC.Release(3);
            inputMB.WaitOne();
            inputMM.WaitOne();
            var MC4 = MC;
            A = MBxMCplusMMmaxPart(MB, MC4, MM, 3 * MB.Length / 4, MB.Length);
            a4.Release();
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
