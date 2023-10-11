// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/31
// Summary: A class of symbolic derivative.
// References:  https://ufcpp.net/study/csharp/sp3_expression.html
//              https://ufcpp.net/study/csharp/sp3_expressionsample.html
// Note: This class was created based on the program published in the above reference.
//       Most of it is used as is.
//       Corrected errors related to some math functions and added some math functions.
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TH.Utils
{
    public static partial class Calculus
    {
        /// <summary>
		/// express a term as style such like { constant, body }.
		/// 2 * x * 3 * y -> { 6, x * y }.
		/// </summary>
		class Term
        {
            public double Constant { get; set; }
            public Expression Body { get; set; }
            public Term(double c) { this.Constant = c; this.Body = null; }
            public Term(Expression b) { this.Constant = 1.0; this.Body = b; }
            public Term(double c, Expression b) { this.Constant = c; this.Body = b; }

            public Expression ToExpression()
            {
                if (this.Constant == 0)
                    return Expression.Constant(0.0);
                if (this.Body == null)
                    return Expression.Constant(this.Constant);
                if (this.Constant == 1)
                    return this.Body;

                return Expression.Multiply(
                    Expression.Constant(this.Constant),
                    this.Body);
            }
        }

        /// <summary>
		/// Calculate symbolically a total derivative of an Expression e.
		/// </summary>
		/// <typeparam name="T">Type of Function.</typeparam>
		/// <param name="e">Expression to be derivative.</param>
		/// <returns>Total derivative of e.</returns>
        public static Expression<T> SymbolicDerivative<T>(this Expression<T> e)
        {
            if (e == null)
                throw new ArgumentNullException("Expression must be non-null.");

            if (e.Parameters.Count != 1)
                throw new ArgumentException("Incorrect number of parameters are included. Parameter count >> " + e.Parameters.Count);

            if (e.NodeType != ExpressionType.Lambda)
                throw new NotSupportedException("Unsupported NodeType is included. NodeType must be Lambda.");

            return Expression.Lambda<T>(e.Body.Simplify().SymbolicDerivative(e.Parameters[0].Name).Simplify(), e.Parameters);
        }

        /// <summary>
        /// Calculate symbolically a total derivative of an Expression e.
        /// </summary>
        /// <typeparam name="T">Type of Function.</typeparam>
        /// <param name="e">Expression to be derivative.</param>
        /// <param name="paramName">Name of the parameter. e.g. "x".</param>
        /// <returns>Total derivative of e.</returns>
        public static Expression<T> SymbolicDerivative<T>(this Expression<T> e, string paramName)
        {
            if (e == null)
                throw new ArgumentNullException("Expression must be non-null.");

            if (e.NodeType != ExpressionType.Lambda)
                throw new NotSupportedException("Unsupported NodeType is included. NodeType must be Lambda.");

            if (e.Parameters.Count(p => p.Name == paramName) == 0)
                return Expression.Lambda<T>(Expression.Constant(0.0), e.Parameters);

            return Expression.Lambda<T>(e.Body.Simplify().SymbolicDerivative(paramName).Simplify(), e.Parameters);
        }

        /// <summary>
        /// Calculate symbolically a total n-th derivative of an Expression e.
        /// </summary>
        /// <typeparam name="T">Type of Function.</typeparam>
        /// <param name="e">Expression to be derivative.</param>
        /// <param name="n">Derivative order.</param>
        /// <returns>Total n-th derivative of e.</returns>
        public static Expression<T> SymbolicDerivative<T>(this Expression<T> e, int n)
        {
            if (e == null)
                throw new ArgumentNullException("Expression must be non-null.");

            if (e.Parameters.Count != 1)
                throw new ArgumentException("Incorrect number of parameters are included. Parameter count >> " + e.Parameters.Count);

            if (e.NodeType != ExpressionType.Lambda)
                throw new NotSupportedException("Unsupported NodeType is included. NodeType must be Lambda.");

            if (n == 0)
                return e;

            Expression<T> de = Expression.Lambda<T>(e.Body.Simplify().SymbolicDerivative(e.Parameters[0].Name).Simplify(), e.Parameters);
            return SymbolicDerivative(de, n - 1);
        }

        /// <summary>
        /// Calculate symbolically an partial derivative of an Expression e with respect to paramName.
        /// </summary>
        /// <param name="e">Expression to be derivative.</param>
        /// <param name="paramName">Name of the parameter. e.g. "x".</param>
        /// <returns>Total derivative of e.</returns>
        private static Expression SymbolicDerivative(this Expression e, string paramName)
        {
            switch (e.NodeType)
            {
                case ExpressionType.Constant:
                    return Expression.Constant(0.0);

                case ExpressionType.Parameter:
                    if (((ParameterExpression)e).Name == paramName)
                        return Expression.Constant(1.0);
                    else
                        return Expression.Constant(0.0);

                case ExpressionType.Negate:
                    {
                        Expression op = ((UnaryExpression)e).Operand;
                        Expression d = op.SymbolicDerivative(paramName);
                        return SimplifiedNegate(d);
                    }

                case ExpressionType.Add:
                    {
                        Expression dLeft = ((BinaryExpression)e).Left.SymbolicDerivative(paramName);
                        Expression dRight = ((BinaryExpression)e).Right.SymbolicDerivative(paramName);
                        return SimplifiedAdd(dLeft, dRight);
                    }


                case ExpressionType.Subtract:
                    {
                        Expression dLeft = ((BinaryExpression)e).Left.SymbolicDerivative(paramName);
                        Expression dRight = ((BinaryExpression)e).Right.SymbolicDerivative(paramName);
                        return SimplifiedSub(dLeft, dRight);
                    }


                case ExpressionType.Multiply:
                    {
                        Expression left = ((BinaryExpression)e).Left;
                        Expression right = ((BinaryExpression)e).Right;
                        Expression dLeft = left.SymbolicDerivative(paramName);
                        Expression dRight = right.SymbolicDerivative(paramName);
                        return SimplifiedAdd(SimplifiedMul(left, dRight), SimplifiedMul(dLeft, right));
                    }

                case ExpressionType.Divide:
                    {
                        Expression left = ((BinaryExpression)e).Left;
                        Expression right = ((BinaryExpression)e).Right;
                        Expression dLeft = left.SymbolicDerivative(paramName);
                        Expression dRight = right.SymbolicDerivative(paramName);
                        return SimplifiedDiv(SimplifiedSub(SimplifiedMul(dLeft, right), SimplifiedMul(left, dRight)), SimplifiedMul(right, right));
                    }

                case ExpressionType.Call:
                    {
                        MethodCallExpression me = (MethodCallExpression)e;
                        return me.SymbolicDerivative(paramName);
                    }

                default:
                    throw new ExpressionNodeTypeException("Operations with the provided NodeType are not supported. NodeType >> " + e.NodeType.ToString());
            }
        }

        /// <summary>
        /// Partial derivation for a System.Math method call.
        /// </summary>
        /// <param name="me">Expression to be derivative.</param>
        /// <param name="paramName">Name of the parameter. e.g. "x".</param>
        /// <returns>Partial derivative of e.</returns>
        private static Expression SymbolicDerivative(this MethodCallExpression me, string paramName)
        {
            MethodInfo mi = me.Method;
            if (!mi.IsStatic || mi.DeclaringType.FullName != "System.Math")
                throw new NotSupportedException("Unsupported math method is included. Function name >> " + mi.DeclaringType + "." + mi.Name);

            Expression d = me.Arguments[0].SymbolicDerivative(paramName).Reduce();

            switch (mi.Name)
            {
                case "Sin":
                    return SimplifiedMul(d, MathCall("Cos", me.Arguments));
                case "Cos":
                    return SimplifiedMul(d, Expression.Negate(MathCall("Sin", me.Arguments)));
                case "Tan":
                    {
                        Expression cos = MathCall("Cos", me.Arguments);
                        return Expression.Divide(d, Expression.Multiply(cos, cos));
                    }
                case "Exp":
                    return SimplifiedMul(d, me);
                case "Log":
                    if (me.Arguments.Count == 1)
                        return SimplifiedDiv(d, me.Arguments[0]);
                    else if (me.Arguments.Count == 2)
                    {
                        // d/dx (log_a(x)) = 1 / xlog(a)
                        return SimplifiedDiv(d, Expression.Multiply(me.Arguments[0], OverloadSolvingMathCall("Log", me.Arguments[1])));
                    }
                    else
                        throw new NotSupportedException("Unsupported math method is included. Function name >> " + mi.Name);
                case "Log10":
                        return SimplifiedDiv(d, Expression.Multiply(me.Arguments[0], OverloadSolvingMathCall("Log", Expression.Constant(10.0))));

                case "Pow":
                    {
                        Expression dx = me.Arguments[0].SymbolicDerivative(paramName).Reduce();
                        Expression dy = me.Arguments[1].SymbolicDerivative(paramName).Reduce();

                        // a^f(x) (a does not contain x)
                        if (dx.IsZero())
                            return SimplifiedMul(me, OverloadSolvingMathCall("Log", me.Arguments[0]));

                        // f(x)^a (a does not contain x)
                        if (dy.IsZero())
                        {
                            double exponent = (double)((ConstantExpression)me.Arguments[1]).Value; 
                            return SimplifiedMul(SimplifiedMul(Expression.Constant(exponent), d), OverloadSolvingMathCall("Pow", me.Arguments[0], SimplifiedSub(me.Arguments[1], Expression.Constant(1.0))));
                        }

                        // f(x)^g(x) is not supported. Its derivative is so complex.
                        throw new NotSupportedException("Unsupported math method is included. Function name >> " + mi.Name);
                    }

                case "Sqrt":
                    {
                        // sqrt(x) = x^(1/2)
                        Expression e = OverloadSolvingMathCall("Pow", me.Arguments[0], Expression.Constant(1.0 / 2.0));
                        return SymbolicDerivative(e, paramName);
                    }
                case "Cbrt":
                    {
                        // cbrt(x) = x^(1/3)
                        Expression e = OverloadSolvingMathCall("Pow", me.Arguments[0], Expression.Constant(1.0 / 3.0));
                        return SymbolicDerivative(e, paramName);
                    }

                case "Asin":
                    {
                        // d/dx(arcsin(x)) = 1/sqrt(1-x^2)
                        // More generally, d/du(arcsin(u)) = d/du(u)/sqrt(1-u^2)
                        // e.g. where u = x+1, d/du(arcsin(u)) = d/dx(x+1)/sqrt(1-(x+1)^2) = 1/sqrt(-x(x+2))
                        
                        Expression poweredArg0 = OverloadSolvingMathCall("Pow", me.Arguments[0], Expression.Constant(2.0));
                        Expression denominator = OverloadSolvingMathCall("Sqrt", Expression.Subtract(Expression.Constant(1.0), poweredArg0));
                        return SimplifiedDiv(d, denominator);
                    }
                case "Acos":
                    {
                        // d/dx(arccos(x)) = -1/sqrt(1-x^2)
                        // More generally, d/du(arcsin(u)) = -d/du(u)/sqrt(1-u^2)

                        Expression e = SymbolicDerivative(OverloadSolvingMathCall("Asin", me.Arguments[0]), paramName);
                        return SimplifiedMul(Expression.Constant(-1.0), e);
                    }
                case "Atan":
                    {
                        // d/dx(arctan(x)) = 1/(1+x^2)
                        // More generally, d/du(arctan(u)) = d/du(u)/(1+u^2)

                        Expression poweredArg0 = OverloadSolvingMathCall("Pow", me.Arguments[0], Expression.Constant(2.0));
                        Expression denominator = Expression.Add(Expression.Constant(1.0), poweredArg0);
                        return SimplifiedDiv(d, denominator);
                    }

                case "Sinh":
                    return SimplifiedMul(d, OverloadSolvingMathCall("Cosh", me.Arguments[0]));
                case "Cosh":
                    return SimplifiedMul(d, OverloadSolvingMathCall("Sinh", me.Arguments[0]));
                case "Tanh":
                    {
                        // d/dx(tanh(x)) = 1/cosh^2(x)
                        // More generally, d/du(tanh(u)) = d/du(u)/cosh^2(u)

                        Expression denominator = OverloadSolvingMathCall("Pow", OverloadSolvingMathCall("Cosh", me.Arguments[0]), Expression.Constant(2.0));
                        return SimplifiedDiv(d, denominator);
                    }

                case "Asinh":
                    {
                        // d/dx(arcsinh(x)) = 1/sqrt(1+x^2)
                        // More generally, d/du(arcsinh(u)) = d/du(u)/sqrt(1+u^2)

                        Expression poweredArg0 = OverloadSolvingMathCall("Pow", me.Arguments[0], Expression.Constant(2.0));
                        Expression denominator = OverloadSolvingMathCall("Sqrt", SimplifiedAdd(Expression.Constant(1.0), poweredArg0));
                        return SimplifiedDiv(d, denominator);
                    }
                case "Acosh":
                    {
                        // d/dx(arccosh(x)) = 1/sqrt(x^2-1)
                        // More generally, d/du(arccosh(u)) = d/du(u)/sqrt(u^2-1)

                        Expression poweredArg0 = OverloadSolvingMathCall("Pow", me.Arguments[0], Expression.Constant(2.0));
                        Expression denominator = OverloadSolvingMathCall("Sqrt", SimplifiedSub(poweredArg0, Expression.Constant(1.0)));
                        return SimplifiedDiv(d, denominator);
                    }
                case "Atanh":
                    {
                        // d/dx(arctanh(x)) = 1/(1-x^2)
                        // More generally, d/du(arctanh(u)) = d/du(u)/(1-u^2)

                        Expression poweredArg0 = OverloadSolvingMathCall("Pow", me.Arguments[0], Expression.Constant(2.0));
                        Expression denominator = SimplifiedSub(Expression.Constant(1.0), poweredArg0);
                        return SimplifiedDiv(d, denominator);
                    }
                default:
                    throw new NotSupportedException("Unsupported math method is included. Function name >> " + mi.Name);
            }
        }

        /// <summary>
        /// Create an expression which contains System.Math method call.
        /// </summary>
        /// <param name="methodName">Name of the method in the System.Math.</param>
        /// <param name="arguments">Arguments of the method.</param>
        /// <returns></returns>
        private static Expression MathCall(string methodName, IEnumerable<Expression> arguments)
        {
            return Expression.Call(null, typeof(System.Math).GetMethod(methodName), arguments);
        }

        /// <summary>
        /// Create an expression which contains System.Math method call.
        /// </summary>
        /// <param name="methodName">Name of the method in the System.Math.</param>
        /// <param name="arguments">Arguments of the method.</param>
        /// <returns></returns>
        private static Expression MathCall(string methodName, params Expression[] arguments)
        {
            return MathCall(methodName, arguments);
        }

        /// <summary>
        /// Create an expression which contains System.Math method call.
        /// This method uniquely identifies a function with multiple overloads, such as Log, since it would be ambiguous which function is which if GetMethod is used.
        /// Judged only by the number of arguments.
        /// </summary>
        /// <param name="methodName">Name of the method in the System.Math.</param>
        /// <param name="arguments">Arguments of the method.</param>
        /// <returns></returns>
        internal static Expression OverloadSolvingMathCall(string methodName, params Expression[] arguments)
        {
            var argLen = arguments.Length;
            var attr = BindingFlags.Static | BindingFlags.Public;
            var methods = typeof(System.Math).GetMethods(attr);
            var method = methods.FirstOrDefault(c => { return c.Name == methodName && c.GetParameters().Length == argLen; });

            return Expression.Call(null, method, arguments);
        }

        /// <summary>
		/// Simplify an Expression by reducing a common denominator and etc..
		/// </summary>
		/// <typeparam name="T">Type of Function.</typeparam>
		/// <param name="e">Expression to be reduced.</param>
		/// <returns>Reduced result.</returns>
        public static Expression<T> Simplify<T>(this Expression<T> e)
        {
            // check not null expression
            if (e == null)
                throw new NullReferenceException("Expression must be non-null");

            // check right node type (maybe not necessary)
            if (e.NodeType != ExpressionType.Lambda)
                throw new ExpressionNodeTypeException("Unsupported NodeType is included. NodeType must be Lambda.");

            // reduce
            return Expression.Lambda<T>(
                e.Body.Simplify(),
                e.Parameters);
        }

        /// <summary>
        /// Simplify an Expression by reducing a common denominator and etc..
        /// </summary>
        /// <param name="e">Expression to be reduced.</param>
        /// <returns>Reduced result.</returns>
        private static Expression Simplify(this Expression e)
        {
            switch (e.NodeType)
            {
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                case ExpressionType.Negate:
                    return e.Cancel();

                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                    return e.Reduce();

                case ExpressionType.Call:
                    {
                        MethodCallExpression me = (MethodCallExpression)e;
                        List<Expression> args = new List<Expression>();

                        foreach (var arg in me.Arguments)
                        {
                            args.Add(arg.Simplify());
                        }

                        return Expression.Call(null, me.Method, args);
                    }

                default:
                    return e;
            }
        }

        /// <summary>
		/// Cancel common terms in sum.
		/// </summary>
		/// <param name="e">Target</param>
		/// <returns>Result</returns>
		private static Expression Cancel(this Expression e)
        {
            List<Term> terms = new List<Term>();

            DeconstructSum(e, false, terms);
            return ConstructSum(terms);
        }

        /// <summary>
        /// Deconstruct sum into list.
        /// 4 * x + y - 2 * x -> { {2, x}, {1, y} }.
        /// </summary>
        /// <param name="e">Expression to be deconstructed.</param>
        /// <param name="minus">Negate sign of e if minus == true.</param>
        /// <param name="terms">List into which deconstructed terms are stored.</param>
        private static void DeconstructSum(Expression e, bool minus, List<Term> terms)
        {
            if (e.NodeType == ExpressionType.Negate)
            {
                DeconstructSum(
                    ((UnaryExpression)e).Operand, !minus, terms);
                return;
            }
            if (e.NodeType == ExpressionType.Add)
            {
                Expression l = ((BinaryExpression)e).Left;
                Expression r = ((BinaryExpression)e).Right;
                DeconstructSum(l, minus, terms);
                DeconstructSum(r, minus, terms);
                return;
            }
            if (e.NodeType == ExpressionType.Subtract)
            {
                Expression l = ((BinaryExpression)e).Left;
                Expression r = ((BinaryExpression)e).Right;
                DeconstructSum(l, minus, terms);
                DeconstructSum(r, !minus, terms);
                return;
            }

            Term t = FoldConstants(e);
            if (minus)
                t.Constant = -t.Constant;

            int i = terms.FindIndex(t1 => t.Body.IsIdenticalTo(t1.Body));
            if (i < 0)
                terms.Add(t);
            else
                terms[i].Constant += t.Constant;
        }

        /// <summary>
        /// Construct sum from term list.
        /// { { 1, x }, { 2, y }, { 3, z } } -> x + 2 * y + 3 * z.
        /// </summary>
        /// <param name="terms">List in which expressions are stored.</param>
        /// <returns>Sum of terms.</returns>
        private static Expression ConstructSum(List<Term> terms)
        {
            Expression sum = Expression.Constant(0.0);
            foreach (var term in terms)
            {
                sum = SimplifiedAdd(sum, term.ToExpression());
            }
            return sum;
        }

        /// <summary>
		/// Optimize a term by folding constants.
		/// For example, 2 * x * 3 * x * 4 -> 24 * x * x.
		/// </summary>
		/// <param name="e">Expression to be optimized.</param>
		private static Term FoldConstants(Expression e)
        {
            List<Expression> n = new List<Expression>();
            List<Expression> d = new List<Expression>();
            DeconstructProduct(e, n, d);
            return ConstructProduct(n, d);
        }

        /// <summary>
        /// Reduce a common denominator.
        /// </summary>
        /// <param name="e">Target</param>
        /// <returns>Result</returns>
        private static Expression Reduce(this Expression e)
        {
            return FoldConstants(e).ToExpression();
        }

        /// <summary>
        /// Deconstruct product into list.
        /// x / a * y * z / b / c -> num = {x, y, z}, denom = {a, b, c}.
        /// </summary>
        /// <param name="e">Expression to be deconstructed.</param>
        /// <param name="list">List into which deconstructed expressions are stored.</param>
        private static void DeconstructProduct(Expression e, List<Expression> num, List<Expression> denom)
        {
            if (e.NodeType == ExpressionType.Multiply)
            {
                Expression left = ((BinaryExpression)e).Left;
                Expression right = ((BinaryExpression)e).Right;

                DeconstructProduct(left, num, denom);
                DeconstructProduct(right, num, denom);
                return;
            }

            if (e.NodeType == ExpressionType.Divide)
            {
                Expression left = ((BinaryExpression)e).Left;
                Expression right = ((BinaryExpression)e).Right;

                DeconstructProduct(left, num, denom);
                DeconstructProduct(right, denom, num);
                return;
            }

            Expression simplified = e.Simplify();

            // result of e.Simplify() could be a form of
            // a * x, a / x or a * x / y where a is constant.
            if (simplified.NodeType == ExpressionType.Multiply)
            {
                Expression left = ((BinaryExpression)simplified).Left;
                Expression right = ((BinaryExpression)simplified).Right;
                num.Add(left);
                if (right.NodeType == ExpressionType.Divide)
                {
                    left = ((BinaryExpression)right).Left;
                    right = ((BinaryExpression)right).Right;
                    num.Add(left);
                    denom.Add(right);
                }
                else
                    num.Add(right);
            }
            else if (simplified.NodeType == ExpressionType.Divide)
            {
                Expression left = ((BinaryExpression)simplified).Left;
                Expression right = ((BinaryExpression)simplified).Right;
                num.Add(left);
                denom.Add(right);
            }
            else
                num.Add(simplified);
        }

        /// <summary>
        /// Construct product from list.
        /// {x, y, z} -> x * y * z.
        /// </summary>
        /// <param name="list">List in which expressions are stored.</param>
        private static Term ConstructProduct(IEnumerable<Expression> list)
        {
            double c = 1;
            Expression prod = null;
            foreach (var e in list)
            {
                if (e == null) continue;

                if (e.IsConstant())
                    c *= (double)((ConstantExpression)e).Value;
                else if (prod == null)
                    prod = e;
                else
                    prod = Expression.Multiply(prod, e);
            }
            return new Term(c, prod);
        }

        /// <summary>
        /// Construct fraction from list.
        /// num = {x, y, z}, denom = {a, b, c} -> (x * y * z) / (a * b * c).
        /// </summary>
        /// <param name="num">List in which expressions of numerator are stored.</param>
        /// <param name="denom">List in which expressions of denominator are stored.</param>
        private static Term ConstructProduct(List<Expression> num, List<Expression> denom)
        {
            double c = 1;

            for (int i = 0; i < num.Count; ++i)
            {
                if (num[i] == null) continue;

                // fold constant
                if (num[i].IsConstant())
                {
                    c *= (double)((ConstantExpression)num[i]).Value;
                    num[i] = null;
                }

                for (int j = 0; j < denom.Count; ++j)
                {
                    if (denom[j] == null) continue;

                    // fold constant
                    if (denom[j].IsConstant())
                    {
                        c /= (double)((ConstantExpression)denom[j]).Value;
                        denom[j] = null;
                    }

                    // reduce common denominator
                    if (num[i].IsIdenticalTo(denom[j]))
                    {
                        num[i] = null;
                        denom[j] = null;
                    }
                }
            }

            Term n = ConstructProduct(num);
            Term d = ConstructProduct(denom);
            n.Constant *= c;

            return Div(n, d);
        }

        /// <summary>
        /// Divide terms with optimization such as x / x -> 1.
        /// </summary>
        /// <param name="t1">Operand 1</param>
        /// <param name="t2">Operand 2</param>
        /// <returns>Result</returns>
        private static Term Div(Term t1, Term t2)
        {
            double c1 = t1.Constant;
            double c2 = t2.Constant;
            Expression b1 = t1.Body;
            Expression b2 = t2.Body;

            if (c1 == 0)
                return new Term(0);

            double c = c1 / c2;

            if (b1 == null)
            {
                if (b2 == null)
                    return new Term(c);
                return new Term(Expression.Divide(Expression.Constant(c), b2));
            }
            if (b2 == null)
                return new Term(c, b1);

            if (b1.IsIdenticalTo(b2))
            {
                return new Term(c);
            }

            return new Term(c,
                Expression.Divide(b1, b2));
        }

        /// <summary>
		/// Negate an expressions with simplification such as -(-x) -> x.
		/// </summary>
		/// <param name="e">Operand</param>
		/// <returns>Result</returns>
		internal static Expression SimplifiedNegate(Expression e)
        {
            if (e.IsConstant())
            {
                return Expression.Constant(
                    -(double)((ConstantExpression)e).Value);
            }

            if (e.NodeType == ExpressionType.Negate)
            {
                return ((UnaryExpression)e).Operand;
            }

            Term t = FoldConstants(e);
            t.Constant = -t.Constant;
            return t.ToExpression();
        }

        /// <summary>
        /// Add two expressions with simplification such as x + 0 -> x.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result</returns>
        internal static Expression SimplifiedAdd(Expression left, Expression right)
        {
            // case: 0 + x -> x
            if (left.IsConstant())
            {
                double c = GetConstantValue((ConstantExpression)left);
                if (c == 0)
                    return right;

                if (right.IsConstant())
                {
                    c += (double)((ConstantExpression)right).Value;
                    return Expression.Constant(c);
                }
            }

            // case: x + 0 -> x
            if (right.IsConstant())
            {
                double c = GetConstantValue((ConstantExpression)right);
                if (c == 0)
                    return left;
            }

            // case: x + x -> 2 * x
            if(left.IsIdenticalTo(right))
                return Expression.Multiply(Expression.Constant(2.0), left);

            // a x + b x -> (a + b) x
            Term t1 = FoldConstants(left);
            Term t2 = FoldConstants(right);

            if (t1.Body.IsIdenticalTo(t2.Body))
                return Expression.Multiply(Expression.Constant(t1.Constant + t2.Constant), t1.Body);

            // otherwise
            return Expression.Add(t1.ToExpression(), t2.ToExpression());
        }

        /// <summary>
        /// Subtract two expressions with simplification such as x - 0 -> x.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result</returns>
        internal static Expression SimplifiedSub(Expression left, Expression right)
        {
            // case: 0 - x -> -x
            if (left.IsConstant())
            {
                double c = GetConstantValue((ConstantExpression)left);
                if (c == 0)
                    return Expression.Negate(right);

                if (right.IsConstant())
                {
                    c -= (double)((ConstantExpression)right).Value;
                    return Expression.Constant(c);
                }
            }

            // case: x - 0 -> x
            if (right.IsConstant())
            {
                double c = GetConstantValue((ConstantExpression)right);
                if (c == 0)
                    return left;
            }

            // case: x - x -> 0
            if (left.IsIdenticalTo(right))
                return Expression.Constant(0.0);

            // a x - b x -> (a - b) x
            Term t1 = FoldConstants(left);
            Term t2 = FoldConstants(right);

            if (t1.Body.IsIdenticalTo(t2.Body))
                return Expression.Multiply(Expression.Constant(t1.Constant - t2.Constant), t1.Body);

            // otherwise
            return Expression.Subtract(t1.ToExpression(), t2.ToExpression());
        }

        /// <summary>
        /// Multiply two expressions with simplification such as x * 3 * x * 2 -> 6 * x * x.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result</returns>
        internal static Expression SimplifiedMul(Expression left, Expression right)
        {
            Expression mul = Expression.Multiply(left, right);

            Term t = FoldConstants(mul);
            return t.ToExpression();
        }

        /// <summary>
        /// Divide two expressions with simplification such as x / x -> 1.
        /// </summary>
        /// <param name="left">Left operand.</param>
        /// <param name="right">Right operand.</param>
        /// <returns>Result</returns>
        internal static Expression SimplifiedDiv(Expression left, Expression right)
        {
            Expression div = Expression.Divide(left, right);

            Term t = FoldConstants(div);
            return t.ToExpression();
        }

        /// <summary>
		/// check whether e1 is identical to e2 or not.
		/// </summary>
		/// <param name="e1">operand 1</param>
		/// <param name="e2">operand 2</param>
		/// <returns>true if identical</returns>
		/// <remarks>
		/// This method is not enough to check the identity completelly.
		/// The identity check could be failed when e1 and e2 are complex.
		/// For instance, so far, even in the case that
		/// e1 = x + 1 + y and e2 = y + 1 + x,
		/// the check is failed.
		/// </remarks>
		public static bool IsIdenticalTo(this Expression e1, Expression e2)
        {
            if (e1 == null)
                return (e2 == null);
            else if (e2 == null)
                return false;

            if (e1.NodeType != e2.NodeType)
                return false;

            switch (e1.NodeType)
            {
                case ExpressionType.Lambda:
                    {
                        LambdaExpression le1 = (LambdaExpression)e1;
                        LambdaExpression le2 = (LambdaExpression)e2;
                        if (!le1.Parameters.IsIdenticalTo(le2.Parameters))
                            return false;

                        return le1.Body.IsIdenticalTo(le2.Body);
                    }
                case ExpressionType.Parameter:
                    {
                        string n1 = ((ParameterExpression)e1).Name;
                        string n2 = ((ParameterExpression)e2).Name;
                        return n1.Equals(n2);
                    }
                case ExpressionType.Constant:
                    {
                        object o1 = ((ConstantExpression)e1).Value;
                        object o2 = ((ConstantExpression)e2).Value;
                        return o1.Equals(o2);
                    }
                case ExpressionType.Negate:
                    {
                        Expression o1 = ((UnaryExpression)e1).Operand;
                        Expression o2 = ((UnaryExpression)e2).Operand;
                        return o1.IsIdenticalTo(o2);
                    }
                case ExpressionType.Add:
                case ExpressionType.Multiply:
                    {
                        Expression o1l = ((BinaryExpression)e1).Left;
                        Expression o1r = ((BinaryExpression)e1).Right;
                        Expression o2l = ((BinaryExpression)e2).Left;
                        Expression o2r = ((BinaryExpression)e2).Right;

                        return
                            (o1l.IsIdenticalTo(o2l) && o1r.IsIdenticalTo(o2r))
                            ||
                            (o1l.IsIdenticalTo(o2r) && o1r.IsIdenticalTo(o2l))
                            ;
                    }
                case ExpressionType.Subtract:
                case ExpressionType.Divide:
                    {
                        Expression o1l = ((BinaryExpression)e1).Left;
                        Expression o1r = ((BinaryExpression)e1).Right;
                        Expression o2l = ((BinaryExpression)e2).Left;
                        Expression o2r = ((BinaryExpression)e2).Right;

                        return (o1l.IsIdenticalTo(o2l) && o1r.IsIdenticalTo(o2r));
                    }
                case ExpressionType.Call:
                    {
                        MethodCallExpression me1 = (MethodCallExpression)e1;
                        MethodCallExpression me2 = (MethodCallExpression)e2;

                        if (me1.Arguments.Count != me2.Arguments.Count)
                            return false;

                        for (int i = 0; i < me1.Arguments.Count; ++i)
                            if (!me1.Arguments[i].IsIdenticalTo(me2.Arguments[i]))
                                return false;

                        MethodInfo mi1 = me1.Method;
                        MethodInfo mi2 = me2.Method;

                        if (!mi1.IsStatic || mi1.DeclaringType.FullName != "System.Math"
                            || !mi2.IsStatic || mi2.DeclaringType.FullName != "System.Math"
                            )
                            return false;

                        if (mi1.Name != mi2.Name)
                            return false;

                        return me1.Arguments.IsIdenticalTo(me2.Arguments);
                    }
            }

            return false;
        }

        private static bool IsIdenticalTo(this ICollection<ParameterExpression> args1, ICollection<ParameterExpression> args2)
        {
            if (args1.Count != args2.Count) return false;

            var enum1 = args1.GetEnumerator();
            var enum2 = args2.GetEnumerator();

            while (enum1.MoveNext() && enum2.MoveNext())
            {
                if (enum1.Current.Name != enum2.Current.Name)
                    return false;
            }

            return true;
        }

        private static bool IsIdenticalTo(this ICollection<Expression> args1, ICollection<Expression> args2)
        {
            if (args1.Count != args2.Count) return false;

            var enum1 = args1.GetEnumerator();
            var enum2 = args2.GetEnumerator();

            while (enum1.MoveNext() && enum2.MoveNext())
            {
                if (!enum1.Current.IsIdenticalTo(enum2.Current))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// check if e == 0
        /// </summary>
        /// <param name="e">operand</param>
        /// <returns>true if 0</returns>
        public static bool IsZero(this Expression e)
        {
            return e.IsIdenticalTo(Expression.Constant(0.0));
        }

        /// <summary>
		/// Check if e is a constant of Double.
		/// </summary>
		/// <param name="e">operand</param>
		/// <returns>True if e is a constant.</returns>
        private static bool IsConstant(this Expression e)
        {
            if (e.NodeType == ExpressionType.Constant)
            {
                switch (e.Type.Name)
                {
                    case "Double":
                    case "Single":
                        return true;

                    default:
                        return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// Whether the two Expression are matched as strings.
        /// </summary>
        /// <param name="e1">Operand 1</param>
        /// <param name="e2">Operand 2</param>
        /// <returns>True if e1 and e2 is matched as string.</returns>
        private static bool EqualAsString(this Expression e1, Expression e2)
        {
            return e1.ToString().Equals(e2.ToString());
        }

        /// <summary>
        /// Return constants from ConstantExpression.
        /// If it is a float, cast it to double.
        /// </summary>
        /// <param name="e">Constant expression.</param>
        /// <returns>Constant value.</returns>
        private static double GetConstantValue(ConstantExpression e)
        {
            double c;
            // For UnityEngine.Mathf, Since Mathf only supports float
            if (e.Type.Name == "Single")
                c = double.Parse(e.Value.ToString());
            else
                c = (double)e.Value;

            return c;
        }

        /// <summary>
        /// Return the constant contained in the target Expression.
        /// Performs four arithmetic operations according to operands.
        /// </summary>
        /// <param name="e">Target expression.</param>
        /// <returns>Constant value.</returns>
        private static double GetConstantValue(Expression e)
        {
            switch(e.NodeType)
            {
                case ExpressionType.Constant:
                    return (double)((ConstantExpression)e).Value;
                case ExpressionType.Parameter:
                    return 1;
                case ExpressionType.Add:
                    return GetConstantValue(e, "+");
                case ExpressionType.Subtract:
                    return GetConstantValue(e, "-");
                case ExpressionType.Multiply:
                    return GetConstantValue(e, "*");
                case ExpressionType.Divide:
                    return GetConstantValue(e, "/");

                default:
                    throw new NotSupportedException("Unsupported NodeType is included.");
            }
        }

        /// <summary>
        /// Return the constant contained in the target Expression.
        /// Performs four arithmetic operations according to operands.
        /// </summary>
        /// <remarks>
        /// Note the calculation results. Accurate values may not be returned.
        /// </remarks>
        /// <param name="e">Target expression.</param>
        /// <param name="op">Operand as string.</param>
        /// <returns>Constant value.</returns>
        private static double GetConstantValue(Expression e, string op)
        {
            Expression left = ((BinaryExpression)e).Left;
            Expression right = ((BinaryExpression)e).Right;

            if (left.NodeType == ExpressionType.Constant)
            {
                if (right.NodeType == ExpressionType.Constant)
                {
                    switch(op)
                    {
                        case "+":
                            return (double)((ConstantExpression)left).Value + (double)((ConstantExpression)right).Value;
                        case "-":
                            return (double)((ConstantExpression)left).Value - (double)((ConstantExpression)right).Value;
                        case "*":
                            return (double)((ConstantExpression)left).Value * (double)((ConstantExpression)right).Value;
                        case "/":
                            return (double)((ConstantExpression)left).Value / (double)((ConstantExpression)right).Value;
                    }
                }
                else
                {
                    switch (op)
                    {
                        case "+":
                            return (double)((ConstantExpression)left).Value + GetConstantValue(right);
                        case "-":
                            return (double)((ConstantExpression)left).Value - GetConstantValue(right);
                        case "*":
                            return (double)((ConstantExpression)left).Value * GetConstantValue(right);
                        case "/":
                            return (double)((ConstantExpression)left).Value / GetConstantValue(right);
                    }
                }
                    
            }

            if (right.NodeType == ExpressionType.Constant)
            {
                switch (op)
                {
                    case "+":
                        return GetConstantValue(left) + (double)((ConstantExpression)right).Value;
                    case "-":
                        return GetConstantValue(left) - (double)((ConstantExpression)right).Value;
                    case "*":
                        return GetConstantValue(left) * (double)((ConstantExpression)right).Value;
                    case "/":
                        return GetConstantValue(left) / (double)((ConstantExpression)right).Value;
                }
            }

            switch (op)
            {
                case "+":
                    return GetConstantValue(left) + GetConstantValue(right);
                case "-":
                    return GetConstantValue(left) - GetConstantValue(right);
                case "*":
                    return GetConstantValue(left) * GetConstantValue(right);
                case "/":
                    return GetConstantValue(left) / GetConstantValue(right);
                default:
                    throw new NotSupportedException("Unsupported operator. Operator >> " + op);
            }
        }
    }
}
