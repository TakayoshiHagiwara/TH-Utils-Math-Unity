// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/17
// Summary: A collection of combinatorics methods.
// References:  https://en.wikipedia.org/wiki/Combinatorics
//              https://en.wikipedia.org/wiki/Binomial_coefficient
// -----------------------------------------------------------------------

using System;

namespace TH.Utils
{
    public static partial class Combinatorics
    {
        /// <summary>
        /// Return the binomial coefficient.
        /// k objects can be chosen from among n objects
        /// (more formally, the number of k-element subsets (or k-combinations) of an n-element set.).
        /// The result is expressed by the following equation.
        /// 
        /// nCk = C(n,k) = (n k)^T = Го (n+1-i) / i
        /// (when i = 1,2...k)
        /// Го is the finite product, it is equivalent to the following equation.
        /// Го (n+1-i) / i = n * ((n-1) / 2) * ... * ((n+i-k) / k)
        /// 
        /// </summary>
        /// <param name="n">Total number of selections available.</param>
        /// <param name="k">Number of selection.</param>
        /// <returns>Result of the binomial coefficient.</returns>
        public static double BinomialCoefficients(int n, int k)
        {
            double res = 1;
            if (k > n)
                throw new ArgumentException("The arguments n and k must satisfy k<=n.");
            if (k == 0)
                return res;

            for(int i = 1; i < k + 1; i++)
                res *= (double)(n + 1 - i) / i;

            return res;
        }
    }
}