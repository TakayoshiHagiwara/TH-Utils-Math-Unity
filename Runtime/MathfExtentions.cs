// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/10
// Summary: A collection of extended UnityEngine.Mathf functions.
// -----------------------------------------------------------------------

using UnityEngine;

namespace TH.Utils
{
    // A collection of extended math functions.
    public partial struct MathfExtentions
    {
        /// <summary>
        /// Return the multi-sin.
        /// Returns the value of the sum of the following functions: 
        /// amp[i] * Sin (omega[i] * t)
        /// </summary>
        /// <param name="amp">Amplitudes.</param>
        /// <param name="omega">Angular frequencies.</param>
        /// <param name="t">Time.</param>
        /// <returns>Value of the sum of the sin function.</returns>
        public static float MultiSin(float[] amp, float[] omega, float t)
        {
            if (amp.Length != omega.Length)
                throw new ArrayLengthMismatchException("amp and freq length does not match.");

            float val = 0;
            for (int i = 0; i < amp.Length; i++)
                val += amp[i] * Mathf.Sin(omega[i] * t);

            return val;
        }

        /// <summary>
        /// Calculate the arithmetic progression.
        /// start <= n < stop
        /// </summary>
        /// <param name="start">Start value.</param>
        /// <param name="stop">End value.</param>
        /// <param name="step">Step of the arithmetic progression.</param>
        /// <returns></returns>
        public static float[] Arange(float start, float stop, int step)
        {
            int count = Mathf.CeilToInt((stop - start) / step);
            float[] result = new float[count];

            for (int i = 0; i < count; i++)
                result[i] = start + i * step;

            return result;
        }

        /// <summary>
        /// Return evenly spaced numbers over a specified interval.
        /// </summary>
        /// <param name="start">Start value.</param>
        /// <param name="stop">End valur.</param>
        /// <param name="num">Number of samples.</param>
        /// <param name="endPoint">If true, stop is the last sample. If false, it is not included.</param>
        /// <returns></returns>
        public static float[] Linspace(float start, float stop, int num, bool endPoint = true)
        {
            float[] result = new float[num];

            float step;
            if (endPoint)
                step = (stop - start) / (num - 1);
            else
                step = (stop - start) / num;

            for (int i = 0; i < num; i++)
                result[i] = start + i * step;

            return result;
        }
    }
}