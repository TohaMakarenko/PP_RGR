using System;
using System.Collections.Generic;
using System.Text;

namespace RGR_first
{
    static class Utils
    {
        private static object printMatrixLocker = new object();
        public static int[] GetVectorOfOne(int size)
        {
            int[] result = new int[size];
            for (int i = 0; i < size; i++) {
                result[i] = 1;
            }
            return result;
        }

        public static int[][] GetMatrixOfOne(int size)
        {
            int[][] result = new int[size][];
            for (int i = 0; i < size; i++) {
                result[i] = GetVectorOfOne(size);
            }
            return result;
        }

        public static int[] GetVectorOfZero(int size)
        {
            int[] result = new int[size];
            for (int i = 0; i < size; i++) {
                result[i] = 0;
            }
            return result;
        }

        public static int[][] GetMatrixOfZero(int size)
        {
            int[][] result = new int[size][];
            for (int i = 0; i < size; i++) {
                result[i] = GetVectorOfOne(size);
            }
            return result;
        }

        public static int[] GetRandomVector(int size)
        {
            var random = new Random();
            int[] result = new int[size];
            for (int i = 0; i < size; i++) {
                result[i] = random.Next(0, 10);
            }
            return result;
        }

        public static int[][] GetRandomMatrix(int size)
        {
            int[][] result = new int[size][];
            for (int i = 0; i < size; i++) {
                result[i] = GetRandomVector(size);
            }
            return result;
        }

        public static void PrintVector(int[] vector)
        {
            var str = string.Join(", ", vector);
            Console.WriteLine(str);
        }

        public static void PrintMatrix(int[][] matrix, string matrixName)
        {
            lock (printMatrixLocker) {
                Console.WriteLine(matrixName);
                foreach (var i in matrix) {
                    PrintVector(i);
                }
            }
        }

    }
}
