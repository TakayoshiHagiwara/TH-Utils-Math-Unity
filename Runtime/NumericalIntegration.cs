// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/8/10
// Summary: A collection of numerical integration methods.
// References:  https://en.wikipedia.org/wiki/Trapezoidal_rule
// -----------------------------------------------------------------------

using System;

namespace TH.Utils
{
    public static partial class Calculus
    {
        /// <summary>
        /// Calculate numerical integral using the trapezoidal rule.
        /// Note that the result will be an approximation.
        /// </summary>
        /// <param name="f">The Formula for integral target.</param>
        /// <param name="a">Start point of integral interval.</param>
        /// <param name="b">End point of integral interval.</param>
        /// <param name="n">Number of divisions of the integral interval.</param>
        /// <returns>Result of integral.</returns>
        public static double Integral(Func<double, double> f, double a, double b, int n = 1000)
        {
            return TrapezoidalRule(f, a, b, n);
        }

        /// <summary>
        /// Trapezoidal rule for numerical integration.
        /// </summary>
        /// <param name="f">The Formula for integral target.</param>
        /// <param name="a">Start point of integral interval.</param>
        /// <param name="b">End point of integral interval.</param>
        /// <param name="n">Number of divisions of the integral interval.</param>
        /// <returns>Result of integral using trapezoidal rule.</returns>
        private static double TrapezoidalRule(Func<double, double> f, double a, double b, int n)
        {
            double coeff = (b - a) / n;
            double term1 = (f(a) + f(b)) / 2;

            double res = term1;
            for(int i = 1; i < n; i++)
                res += f(a + (i * ((b - a) / n)));

            return coeff * res;
        }
    }
}