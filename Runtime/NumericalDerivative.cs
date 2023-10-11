// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/17
// Summary: A collection of calculus methods.
// References:  https://en.wikipedia.org/wiki/Finite_difference
//              https://www1.doshisha.ac.jp/~bukka/lecture/computer/resume/chap07.pdf
//              https://fluid.mech.kogakuin.ac.jp/~iida/Lectures/seminar/java/document/cfd-t02.pdf
// Note: The higher the order, the larger the error and the worse the accuracy.
//       Accuracy is also worse for trigonometric functions and complex formulas.
// -----------------------------------------------------------------------

using System;
using UnityEngine;

namespace TH.Utils
{
    public static partial class Calculus
    {
        /// <summary>
        /// Return the derivative coefficient of a formula at an arbitrary value.
        /// Note that the calculation is approximate due to the use of finite differences.
        /// Also, the higher the order, the greater the error.
        /// It can be calculated with some accuracy up to ~4th order.
        /// For higher orders, please note the results.
        /// </summary>
        /// <param name="f">The Formula for derivative target.</param>
        /// <param name="n">Derivative order.</param>
        /// <param name="x">The coordinate value for which the derivative coefficient is calculated.</param>
        /// <returns>Derivative coefficient at x.</returns>
        public static double Derivative(Func<double, double> f, int n, double x)
        {
            return FiniteDifference(f, n, x);
        }

        /// <summary>
        /// The nth-order derivative coefficient at x.
        /// </summary>
        /// <param name="f">The Formula for derivative target.</param>
        /// <param name="n">Derivative order.</param>
        /// <param name="x">The coordinate value for which the derivative coefficient is calculated.</param>
        /// <returns>Derivative coefficient at x.</returns>
        private static double FiniteDifference(Func<double, double> f, int n, double x)
        {
            double h = 1.0e-5;

            if (n == 1)
                return CentralDifference1st(f, x, h);
            if (n == 2)
                return CentralDifference2nd(f, x, h);

            double multiplier = 1.0;
            for (int i = 0; i < n; i++)
            {
                if (i == 0)
                    continue;
                if (i % 2 == 0)
                    multiplier *= 10.0;
            }
            if (multiplier != 1)
                multiplier *= 10.0;

            h *= multiplier;

            double res;
            if(n % 2 == 0)
                res = CentralDifference(f, n, x, h) / Mathf.Pow((float)h, n);
            else
            {
                double forward = CentralDifference(f, n, (x - 1/2), h);
                double backward = CentralDifference(f, n, (x + 1/2), h);
                double averageCentralDiff = (forward + backward) / 2;
                res = averageCentralDiff / Mathf.Pow((float)h, n);
            }
            
            return res;
        }

        /// <summary>
        /// The nth-order derivative coefficient at x using central difference.
        /// </summary>
        /// <param name="f">The Formula for derivative target.</param>
        /// <param name="n">Derivative order.</param>
        /// <param name="x">The coordinate value for which the derivative coefficient is calculated.</param>
        /// <param name="h">A finite but sufficiently small value.</param>
        /// <returns>Derivative coefficient at x.</returns>
        private static double CentralDifference(Func<double, double> f, int n, double x, double h)
        {
            double coeff = 0;
            double halfN = (double)n / 2;

            for (int i = 0; i < n + 1; i++)
                coeff += Mathf.Pow(-1, i) * Combinatorics.BinomialCoefficients(n, i) * f(x + (halfN - i) * h);

            return coeff;
        }

        /// <summary>
        /// The 1st-order derivative coefficient of 4th-order accuracy using central difference.
        /// </summary>
        /// <param name="f">The Formula for derivative target.</param>
        /// <param name="x">The coordinate value for which the derivative coefficient is calculated.</param>
        /// <param name="h">A finite but sufficiently small value.</param>
        /// <returns>Derivative coefficient at x.</returns>
        private static double CentralDifference1st(Func<double, double> f, double x, double h)
        {
            return (f(x - 2.0 * h) - 8.0 * f(x - h) + 8.0 * f(x + h) - f(x + 2.0 * h)) / (12.0 * h);
        }

        /// <summary>
        /// The 2nd-order derivative coefficient of 2nd-order accuracy using central difference.
        /// </summary>
        /// <param name="f">The Formula for derivative target.</param>
        /// <param name="x">The coordinate value for which the derivative coefficient is calculated.</param>
        /// <param name="h">A finite but sufficiently small value.</param>
        /// <returns>Derivative coefficient at x.</returns>
        private static double CentralDifference2nd(Func<double, double> f, double x, double h)
        {
            return (f(x + h) - 2.0 * f(x) + f(x - h)) / MathF.Pow((float)h, 2);
        }
    }
}