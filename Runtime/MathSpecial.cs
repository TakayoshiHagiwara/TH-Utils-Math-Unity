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
        private readonly static int s_besselSeriesN = 10;
        private readonly static int s_seriesN = 10;
        private readonly static double s_eulerGamma = 0.57721566490153286;

        /// <summary>
        /// Calculates value of the gamma function.
        /// </summary>
        /// <param name="z">Real valued argument.</param>
        /// <returns>Value of the gamma function.</returns>
        public static double Gamma(double z)
        {
            if (z == 0)
                throw new ArgumentException("The argument must be non-zero.");

            if(z < 0 && (int)z == z)
                throw new ArgumentException("Negative integers are not supported.");

            if (z < 0)
                return InternalGamma(z);

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
        /// Calculates value of the gamma function for negative and non-integer value.
        /// If z < 0 and non-negative integer, return following
        /// gamma(z) = gamma(z+n) / {(z+n-1)(z+n-2)...z}, when z+n > 0.
        /// </summary>
        /// <param name="z">Real valued argument.</param>
        /// <returns>Value of the gamma function.</returns>
        private static double InternalGamma(double z)
        {
            int n = 0;
            for (int i = 1; i < 100; i++)
            {
                if (z + i > 0)
                    n = i;
            }
            if (n == 0)
                throw new ArgumentException("Argument value is too large.");

            double gamma = Gamma(z + n);
            double denom = 1;
            for (int i = 1; i <= n; i++)
            {
                denom *= z + n - i;
            }

            return gamma / denom;
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

        /// <summary>
        /// Calculate value of the Bessel functions of the first kind.
        /// </summary>
        /// <param name="v">Order.</param>
        /// <param name="z">Argument.</param>
        /// <returns>Value of the Bessel functions of the first kind.</returns>
        public static double BesselJ(double v, double z)
        {
            if (v == 0 && z == 0)
                return 1;

            double sign = 1;
            if (v < 0 && (int)v == v)
            {
                // If v is a negative integer
                sign = Math.Pow(-1, Math.Abs(v));
                v = Math.Abs(v);
            }
            else if(v < 0)
            {
                // If v is a negative decimal
                return Hyp0f1(v + 1, -(z * z) / 4) / Gamma(v + 1) / Math.Pow(z / 2, Math.Abs(v));
            }

            double res = 0;
            for (int k = 0; k < s_besselSeriesN; k++)
                res += Math.Pow(-1, k) / (MathExtentions.Factorial(k) * Gamma(k + v + 1)) * Math.Pow(z / 2, 2 * k + v);

            return sign * res;
        }

        /// <summary>
        /// Calculate value of the Bessel functions of the second kind.
        /// </summary>
        /// <remarks>
        /// CAUTION: Currently does not return an exact value.
        /// </remarks>
        /// <param name="v">Order.</param>
        /// <param name="z">Argument.</param>
        /// <returns>Value of the Bessel functions of the second kind.</returns>
        private static double BesselY(double v, double z)
        {
            if (v == 0 && z == 0)
                return double.NegativeInfinity;
            if (z == 0)
                return double.PositiveInfinity;

            if ((int)v == v)
            {
                double term1 = (2 / Math.PI) * BesselJ(v, z) * Math.Log(z / 2);
                double term2 = 0;
                for(int i = 0; i < v; i++)
                    term2 += MathExtentions.Factorial((int)v - i - 1) / MathExtentions.Factorial(i) * Math.Pow(z / 2, 2 * i - v);
                term2 *= -(1 / Math.PI);

                double term3 = 0;
                for(int i = 0; i < s_seriesN; i++)
                    term3 += Math.Pow(-1, i) * (Digamma(i + 1) + Digamma(i + v + 1)) / (MathExtentions.Factorial(i) * MathExtentions.Factorial((int)v + i)) * Math.Pow(z / 2, 2 * i + v);
                term3 *= -(1 / Math.PI);

                return term1 + term2 + term3;
            }
            else
            {
                return (BesselJ(v, z) * Math.Cos(v * Math.PI) - BesselJ(-v, z)) / Math.Sin(v * Math.PI);
            }
        }

        /// <summary>
        /// Calculate value of the Modified Bessel functions of the first kind.
        /// </summary>
        /// <param name="v">Order.</param>
        /// <param name="z">Argument.</param>
        /// <returns>Value of the Modified Bessel functions of the first kind.</returns>
        public static double BesselI(double v, double z)
        {
            if (v < 0 && (int)v == v)
                v = Math.Abs(v);
            if (v < 0)
                return Hyp0f1(v + 1, z * z / 4) / Gamma(v + 1) * Math.Pow(z / 2, v);

            double res = 0;
            for (int i = 0; i < s_besselSeriesN; i++)
                res += (1 / (MathExtentions.Factorial(i) * Gamma(i + v + 1))) * Math.Pow(z / 2, 2 * i + v);

            return res;
        }

        /// <summary>
        /// Calculate value of the Modified Bessel functions of the second kind.
        /// </summary>
        /// <remarks>
        /// CAUTION: Currently does not return an exact value.
        /// </remarks>
        /// <param name="v">Order.</param>
        /// <param name="z">Argument.</param>
        /// <returns>Value of the Modified Bessel functions of the second kind.</returns>
        private static double BesselK(double v, double z)
        {
            //return (Math.PI / 2) * (BesselI(-v, z) - BesselI(v, z)) / Math.Sin(v * Math.PI);

            double term1 = 0;
            for (int i = 0; i < Math.Abs(v) - 1; i++)
                term1 += Math.Pow(-1, i) * Gamma(Math.Abs(v) - i) / MathExtentions.Factorial(i) * Math.Pow(z / 2, 2 * i);
            term1 *= (1 / 2) * Math.Pow(z / 2, -Math.Abs(v));

            double term2 = 0;
            for (int i = 0; i < s_seriesN; i++)
                term2 += Math.Pow(z / 2, 2 * i) / MathExtentions.Factorial(i) * Gamma(i + Math.Abs(v) + 1);
            term2 *= Math.Pow(-1, v - 1) * Math.Log(z / 2) * Math.Pow(z / 2, Math.Abs(v));

            double term3 = 0;
            for (int i = 0; i < s_seriesN; i++)
                term3 += (Digamma(i + 1) + Digamma(i + Math.Abs(v) + 1)) / (MathExtentions.Factorial(i) * Gamma(i + Math.Abs(v) + 1)) * Math.Pow(z / 2, 2 * i);
            term3 *= Math.Pow(-1, v) / 2 * Math.Pow(z / 2, Math.Abs(v));
            
            return term1 + term2 + term3;
        }

        /// <summary>
        /// Calculate the approximate value of the hypergeometric function.
        /// </summary>
        /// <param name="a">Argument.</param>
        /// <param name="z">Argument.</param>
        /// <returns>Approximate value.</returns>
        public static double Hyp0f1(double a, double z)
        {
            double res = 0;
            for(int i = 0; i < s_seriesN; i++)
                res += (1 / MathExtentions.RisingFactorial(a, i)) * (Math.Pow(z, i) / MathExtentions.Factorial(i));

            return res;
        }

        /// <summary>
        /// Calculate the approximate value of the digamma function.
        /// </summary>
        /// <param name="z">Argument.</param>
        /// <returns>Approximate value.</returns>
        public static double Digamma(double z)
        {
            if (z == 1)
                return -s_eulerGamma;
            if (z == 0.5)
                return -s_eulerGamma - 2 * Math.Log(2);
            if ((int)z == z)
                return -s_eulerGamma + HarmonicNumber((int)z - 1);

            double res = 0;

            for (int i = 0; i < 10000; i++)
                res += 1 / (i * i + (z + 1) * i + z);

            res *= z - 1;

            return -s_eulerGamma + res;
        }

        /// <summary>
        /// Calculate the harmonic number.
        /// </summary>
        /// <param name="n">Order.</param>
        /// <returns>Harmonic number.</returns>
        public static double HarmonicNumber(int n)
        {
            double res = 0;
            for (int i = 1; i < n + 1; i++)
                res += 1 / i;

            return res;
        }
    }
}