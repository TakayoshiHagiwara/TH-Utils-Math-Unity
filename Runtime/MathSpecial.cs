// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/8/11
// Summary: A collection of special function methods.
// References:  https://en.wikipedia.org/wiki/Gamma_function
//              https://en.wikipedia.org/wiki/Beta_function
// -----------------------------------------------------------------------

using System;

namespace TH.Utils
{
    public static class MathSpecial
    {
        /// <summary>
        /// Calculates value of the gamma function using Stirling's approximation.
        /// </summary>
        /// <param name="z">Real valued argument.</param>
        /// <returns>Value of the gamma function.</returns>
        public static double Gamma(double z)
        {
            if (z <= 0)
                throw new ArgumentException("Arguments should be defined with z > 0.");

            // For natural numbers, return the factorial of the z-1.
            if ((int)z == z)
                return MathExtentions.Factorial((int)z - 1);

            // The value of the gamma function for 1/2 is consistent with the result of the Gaussian integral.
            if (MathExtentions.Approximately(z, 0.5))
                return Math.Sqrt(Math.PI);

            double n = z - 1.0 / 2.0;
            if((int)n == n)
                return MathExtentions.DoubleFactorial(2 * (int)n - 1) / Math.Pow(2, n) * Math.Sqrt(Math.PI);

            return StirlingApproximation(z);
        }

        /// <summary>
        /// Calculate Stirling's approximation.
        /// Reference: Nemes Gergő, New asymptotic expansion for the Gamma function, Archiv der Mathematik, 95, 161-169, 2010.
        /// https://link.springer.com/article/10.1007/s00013-010-0146-9
        /// </summary>
        /// <param name="z">Real or complex valued argument.</param>
        /// <returns>Approximation of the gamma function.</returns>
        private static double StirlingApproximation(double z)
        {
            double coeff = Math.Sqrt((2 * Math.PI) / z);
            double term = (1 / Math.E) * (z + (1 / (12 * z - (1 / (10 * z)))));
            return coeff * Math.Pow(term, z);
        }

        /// <summary>
        /// Calculate value of the beta function.
        /// </summary>
        /// <param name="x">Real valued argument.</param>
        /// <param name="y">Real valued argument.</param>
        /// <returns>Value of the beta function.</returns>
        public static double Beta(double x, double y)
        {
            return Gamma(x) * Gamma(y) / Gamma(x + y);
        }
    }
}