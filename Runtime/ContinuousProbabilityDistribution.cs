// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/8/7
// Summary: A collection of continuous probability distribution.
// References: https://en.wikipedia.org/wiki/Normal_distribution
// -----------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace TH.Utils
{
    public static class ContinuousProbabilityDistribution
    {
        private static readonly float s_minX = -5;
        private static readonly float s_maxX = 5;
        private static readonly int s_num = 50;

        /// <summary>
        /// Calculate standard normal distribution.
        /// </summary>
        /// <returns>Standard normal distribution.</returns>
        public static List<float> StandardNormal()
        {
            return Normal(s_minX, s_maxX, s_num, 0, 1);
        }

        /// <summary>
        /// Calculate standard normal distribution.
        /// </summary>
        /// <param name="minX">Min range of x-axis.</param>
        /// <param name="maxX">Max range of x-axis.</param>
        /// <param name="num">Number of samples.</param>
        /// <returns>Standard normal distribution.</returns>
        public static List<float> StandardNormal(float minX, float maxX, int num)
        {
            return Normal(minX, maxX, num, 0, 1);
        }

        /// <summary>
        /// Calculate normal distribution.
        /// </summary>
        /// <param name="mean">Mean.</param>
        /// <param name="var">Variance.</param>
        /// <returns>Normal distribution.</returns>
        public static List<float> Normal(float mean, float var)
        {
            return Normal(s_minX, s_maxX, s_num, mean, var);
        }

        /// <summary>
        /// Calculate normal distribution.
        /// </summary>
        /// <param name="minX">Min range of x-axis.</param>
        /// <param name="maxX">Max range of x-axis.</param>
        /// <param name="num">Number of samples.</param>
        /// <param name="mean">Mean.</param>
        /// <param name="var">Variance.</param>
        /// <returns>Normal distribution.</returns>
        public static List<float> Normal(float minX, float maxX, int num, float mean, float var)
        {
            List<float> result = new List<float>(num);
            float[] x = MathfExtentions.Linspace(minX, maxX, num);

            float coeff = 1.0f / (Mathf.Sqrt(2 * Mathf.PI * var));

            for(int i = 0; i < num; i++)
                result.Add(coeff * Mathf.Exp(-Mathf.Pow((x[i] - mean), 2) / (2 * var)));
    
            return result;
        }
    }
}