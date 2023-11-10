// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/8/5
// Summary: A collection of extended System.Math functions.
// -----------------------------------------------------------------------

using System;

namespace TH.Utils
{
    // A collection of extended System.Math functions.
    public partial struct MathExtentions
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
        public static double MultiSin(double[] amp, double[] omega, double t)
        {
            if (amp.Length != omega.Length)
                throw new ArrayLengthMismatchException("amp and freq length does not match.");

            double val = 0;
            for (int i = 0; i < amp.Length; i++)
                val += amp[i] * Math.Sin(omega[i] * t);

            return val;
        }

        /// <summary>
        /// Return the product of all positive integers less than or equal to n.
        /// denoted by n!.
        /// n! = n * (n-1) * (n-2) * ... * 2 * 1
        /// </summary>
        /// <param name="n">Number to calculate the factorial.</param>
        /// <returns>Factorial of n.</returns>
        public static int Factorial(int n)
        {
            if (n == 0)
                return 1;
            return n * Factorial(n - 1);
        }

        /// <summary>
        /// Calculate the double factorial.
        /// denoted by n!!.
        /// NOT equal to (n!)!.
        /// </summary>
        /// <param name="n">Number to calculate the double factorial.</param>
        /// <returns>Double factorial of n.</returns>
        public static int DoubleFactorial(int n)
        {
            if (n == 0)
                return 1;
            if (n - 2 < 0)
                return 1;
            else
                return n * DoubleFactorial(n - 2);
        }

        /// <summary>
        /// Compute if the two values are approximately equal.
        /// Double points have about 16 significant digits, so
        /// 1.000000000000001 can be represented while 1.000000000000001 is rounded to zero,
        /// thus we could use an epsilon of 0.000000000000001 for comparing values close to 1.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool Approximately(double a, double b, double epsilon = 1e-15)
        {
            return Math.Abs(b - a) < Math.Max(0.000000000000001 * Math.Max(Math.Abs(a), Math.Abs(b)), epsilon);
        }

        /// <summary>
        /// Calculate the arithmetic progression.
        /// start <= n < stop
        /// </summary>
        /// <param name="start">Start value.</param>
        /// <param name="stop">End value.</param>
        /// <param name="step">Step of the arithmetic progression.</param>
        /// <returns></returns>
        public static double[] Arange(int start, int stop, int step)
        {
            int count = (int)Math.Ceiling((double)(stop - start) / step);
            double[] result = new double[count];

            for (int i = 0; i < count; i++)
                result[i] = start + i * step;

            return result;
        }

        public static double[] Arange(double start, double stop, int step)
        {
            int count = (int)Math.Ceiling((stop - start) / step);
            double[] result = new double[count];

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
        public static double[] Linspace(int start, int stop, int num, bool endPoint = true)
        {
            double[] result = new double[num];

            double step;
            if (endPoint)
                step = (double)(stop - start) / (num - 1);
            else
                step = (double)(stop - start) / num;

            for (int i = 0; i < num; i++)
                result[i] = start + i * step;

            return result;
        }

        public static double[] Linspace(double start, double stop, int num, bool endPoint = true)
        {
            double[] result = new double[num];

            double step;
            if (endPoint)
                step = (stop - start) / (num - 1);
            else
                step = (stop - start) / num;

            for (int i = 0; i < num; i++)
                result[i] = start + i * step;

            return result;
        }

        /// <summary>
        /// Convert any range the value to another range.
        /// </summary>
        /// <param name="val">Value to convert.</param>
        /// <param name="minFrom">Minimum value of the range of values before conversion.</param>
        /// <param name="maxFrom">Maximum value of the range of values before conversion.</param>
        /// <param name="minTo">Minimum of the range of converted values.</param>
        /// <param name="maxTo">Maximum of the range of converted values.</param>
        /// <returns>Value converted to an arbitrary range.</returns>
        public static double Remap(double val, double minFrom, double maxFrom, double minTo, double maxTo)
        {
            return (val - minFrom) / (maxFrom - minFrom) * (maxTo - minTo) + minTo;
        }
    }
}