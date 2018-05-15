using System;
using System.Collections.Generic;
using System.Text;

namespace RGR_first
{
    static class Utils
    {
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
    }
}
