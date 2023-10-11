// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/8/4
// Summary: Methods of Taylor expansion in Calculus class.
// References:  https://en.wikipedia.org/wiki/Taylor_series
// -----------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace TH.Utils
{
    public static partial class Calculus
    {
        private static readonly int s_maxOrder = 20;

        /// <summary>
        /// Calculate the nth-order Taylor expansion at around point a.
        /// </summary>
        /// <param name="e">Expression to compute Taylor expansion.</param>
        /// <param name="n">The order in which the series is calculated.</param>
        /// <param name="a">The point at which the Taylor series is calculated.</param>
        /// <returns>nth-order Taylor expansion at around point a.</returns>
        public static Expression<Func<double, double>> TaylorExpansion(this Expression<Func<double, double>> e, int n, double a)
        {
            if (e == null)
                throw new ArgumentNullException("Expression must be non-null.");

            if (e.Parameters.Count != 1)
                throw new ArgumentException("Incorrect number of parameters are included. Parameter count >> " + e.Parameters.Count);

            if (e.NodeType != ExpressionType.Lambda)
                throw new NotSupportedException("Unsupported NodeType is included. NodeType must be Lambda.");

            if (n > s_maxOrder)
                throw new NotSupportedException("The number of the order to calculate the Taylor expansion is too large. Please provide a value less than or equal to " + s_maxOrder);

            return TaylorSeries(e, n, a);
        }

        /// <summary>
        /// Calculate the nth-order Maclaurin expansion.
        /// It is equivalent to Taylor expansion around a = 0.
        /// </summary>
        /// <param name="e">Expression to compute Maclaurin expansion.</param>
        /// <param name="n">The order in which the series is calculated.</param>
        /// <returns>nth-order Maclaurin expansion at around point a.</returns>
        public static Expression<Func<double, double>> MaclaurinExpansion(this Expression<Func<double, double>> e, int n)
        {
            return TaylorExpansion(e, n, 0);
        }

        /// <summary>
        /// Calculate the nth-order Taylor series at around point a.
        /// sigma (f^(i)(a) / i! * (x-a)^i), where i = 0 to n.
        /// f^(i)(a) is the i-th derivative of f(x) at x = a.
        /// </summary>
        /// <param name="e">Expression to compute Taylor series.</param>
        /// <param name="n">The order in which the series is calculated.</param>
        /// <param name="a">The point at which the Taylor series is calculated.</param>
        /// <returns>nth-order Taylor series at around point a.</returns>
        private static Expression<Func<double, double>> TaylorSeries(this Expression<Func<double, double>> e, int n, double a)
        {
            // (x-a)
            Expression term = SimplifiedSub(e.Body, Expression.Constant(a));
            if (e.Body.NodeType == ExpressionType.Call)
            {
                MethodCallExpression me = (MethodCallExpression)e.Body;
                term = SimplifiedSub(me.Arguments[0], Expression.Constant(a));
            }

            // f(a) when n = 0
            double coeff = e.Compile()(a);
            Expression result = Expression.Constant(coeff).Simplify();

            for (int i = 1; i < n + 1; i++)
            {
                var d = e.SymbolicDerivative(n: i);
                double da = d.Compile()(a);

                // if f(a) is approximately zero, coeff = 0 else f^(n)(a) / n!
                coeff = MathExtentions.Approximately(da, 0) ? 0 : da / MathExtentions.Factorial(i);

                // f^(n)(a) / n! * (x-a)^n
                result = SimplifiedAdd(result, SimplifiedMul(Expression.Constant(coeff), OverloadSolvingMathCall("Pow", term, Expression.Constant((double)i))));
            }

            return Expression.Lambda<Func<double, double>>(result, e.Parameters[0]);
        }
    }
}
