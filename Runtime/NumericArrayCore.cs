// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/19
// Summary: Utility class for numeric array.
// -----------------------------------------------------------------------

namespace TH.Utils
{
    public static class NumericArrayCore
    {
        public static int[] Diff(this int[] array, int order = 1)
        {
            int[] res = new int[array.Length - 1];
            for (int i = 0; i < array.Length - 1; i++)
                res[i] = array[i + 1] - array[i];

            if (order != 1)
                return Diff(res, order - 1);
            else
                return res;
        }

        public static float[] Diff(this float[] array, int order = 1)
        {
            float[] res = new float[array.Length - 1];
            for(int i = 0; i < array.Length - 1; i++)
                res[i] = array[i + 1] - array[i];

            if (order != 1)
                return Diff(res, order - 1);
            else
                return res;
        }
    }
}