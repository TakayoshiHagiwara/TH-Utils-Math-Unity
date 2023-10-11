# TH-Utils-Math-Unity/TH.Utils.Calculus class<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Methods](#methods)
  - [Derivative(Func\<double, double\>, int, double)](#derivativefuncdouble-double-int-double)
    - [Parameters](#parameters)
    - [Returns](#returns)
    - [Exapmle](#exapmle)
    - [References](#references)
  - [Integral(Func\<double, double\>, double, double, int)](#integralfuncdouble-double-double-double-int)
    - [Parameters](#parameters-1)
    - [Returns](#returns-1)
  - [IsIdenticalTo(Expression, Expression)](#isidenticaltoexpression-expression)
    - [Parameters](#parameters-2)
    - [Returns](#returns-2)
    - [References](#references-1)
  - [IsZero(Expression)](#iszeroexpression)
    - [Parameters](#parameters-3)
    - [Returns](#returns-3)
    - [References](#references-2)
  - [MaclaurinExpansion(this Expression\<Func\<double, double\>\>, int)](#maclaurinexpansionthis-expressionfuncdouble-double-int)
    - [Parameters](#parameters-4)
    - [Returns](#returns-4)
    - [Exapmle](#exapmle-1)
  - [SymbolicDerivative\<T\>(Expression\<T\>)](#symbolicderivativetexpressiont)
    - [Parameters](#parameters-5)
    - [Returns](#returns-5)
    - [Exapmle](#exapmle-2)
    - [References](#references-3)
  - [SymbolicDerivative\<T\>(Expression\<T\>, string)](#symbolicderivativetexpressiont-string)
    - [Parameters](#parameters-6)
    - [Returns](#returns-6)
    - [References](#references-4)
  - [SymbolicDerivative\<T\>(Expression\<T\>, int)](#symbolicderivativetexpressiont-int)
    - [Parameters](#parameters-7)
    - [Returns](#returns-7)
    - [Example](#example)
  - [Simplify\<T\>(Expression\<T\>)](#simplifytexpressiont)
    - [Parameters](#parameters-8)
    - [Returns](#returns-8)
    - [References](#references-5)
  - [TaylorExpansion(this Expression\<Func\<double, double\>\>, int, double)](#taylorexpansionthis-expressionfuncdouble-double-int-double)
    - [Parameters](#parameters-9)
    - [Returns](#returns-9)
    - [Exapmle](#exapmle-3)
</details>


# Definition
Namespace: TH.Utils

微分・積分に関する静的メソッドを提供します。

式木を用いた記号微分に関するスクリプトは[++C++;](https://ufcpp.net/)様が[公開してくださっているもの](https://ufcpp.net/study/csharp/sp3_expressionsample.html)を参考にさせていただきました。一部修正、追加させていただいています。ありがとうございます。

# Methods
<!-- -------------------------------------------------- -->
## Derivative(Func<double, double>, int, double)
有限差分による数値微分を行い任意の点における微分係数を計算します。
中心差分を用いて計算を行います。
有限差分を使用しているため、計算結果は近似値であることに注意してください。
また、次数が高いほど誤差は大きくなります。

```csharp
public static double Derivative(Func<double, double> f, int n, double x)
```

### Parameters
- `f`: Func<double, double>
  - 微分対象の式。double型の引数を1つ取り、double型の結果を返すFunc。
- `n`: int
  - 微分する階数。
- `x`: double
  - 微分係数を計算する点。

### Returns
- double
  - 点xにおけるn階微分係数。

### Exapmle

```csharp
using System;
using TH.Utils;

double res;

// d/dx (2x^2) where x = 1
res = Calculus.Derivative((x) => 2.0 * x * x, 1, 1);

// res ≈ 4
```

### References
- https://en.wikipedia.org/wiki/Finite_difference
- https://www1.doshisha.ac.jp/~bukka/lecture/computer/resume/chap07.pdf
- https://fluid.mech.kogakuin.ac.jp/~iida/Lectures/seminar/java/document/cfd-t02.pdf

<!-- -------------------------------------------------- -->
## Integral(Func<double, double>, double, double, int)
数値積分を計算します。
台形則を用いて計算を行います。
計算結果は近似値であることに注意してください。

```csharp
public static double Integral(Func<double, double> f, double a, double b, int n = 1000)
```

### Parameters
- `f`: Func<double, double>
  - 積分対象の式。double型の引数を1つ取り、double型の結果を返すFunc。
- `a`: double
  - 積分区間の始点。
- `b`: double
  - 積分区間の終点。
- `n`: int
  - 積分区間の分割数。デフォルトでは1000です。

### Returns
- double
  - 数値積分結果。

```csharp
using System;
using TH.Utils;

double res;

// Integral x^2 from 1 to 2
res = Calculus.Integral((x) => x * x, 1, 2);

// res ≈ 2.3333335
```

<!-- -------------------------------------------------- -->
## IsIdenticalTo(Expression, Expression)
2つの式木が同等かどうかを判定します。

```csharp
public static bool IsIdenticalTo(this Expression e1, Expression e2)
```

### Parameters
- `e1, e2`: Expression
  - 対象の式木。

### Returns
- bool
  - 同等であればTrue。

### References
- https://ufcpp.net/study/csharp/sp3_expressionsample.html

<!-- -------------------------------------------------- -->
## IsZero(Expression)
式木が0かどうかを判定します。

```csharp
public static bool IsZero(this Expression e)
```

### Parameters
- `e`: Expression
  - 対象の式木。

### Returns
- bool
  - 0であればTrue。

### References
- https://ufcpp.net/study/csharp/sp3_expressionsample.html

<!-- -------------------------------------------------- -->
## MaclaurinExpansion(this Expression<Func<double, double>>, int)
式木で与えられた関数のマクローリン展開を返します。
`n`次の近似を計算します。
このメソッドは同じ関数に対してテイラー展開を`a = 0`で行った場合と同じ結果を返します。
以下の式に従って計算を行います。

$$ \sum_{i=0}^{n} \dfrac{f^{(i)}(0)}{i!} x^{i} $$

```csharp
public static Expression<Func<double, double>> MaclaurinExpansion(this Expression<Func<double, double>> e, int n)
```

### Parameters
- `e`: Expression<Func<double, double>>
  - 対象の式木。
- `n`: int
  - 近似する次数。

### Returns
- Expression<Func<double, double>>
  - 結果の式木。

### Exapmle

```csharp
using System;
using System.Linq.Expressions;
using TH.Utils;

Expression<Func<double, double>> f = x => Math.Sin(x);
int order = 4;

var maclaurinExpansion = f.MaclaurinExpansion(order);

// Debug.Log($"Maclaurin Expansion of {f.Body} = {maclaurinExpansion.Body}");
```

<!-- -------------------------------------------------- -->
## SymbolicDerivative\<T>(Expression\<T>)
式木に対して記号微分を行います。

```csharp
public static Expression<T> SymbolicDerivative<T>(this Expression<T> e)
```

### Parameters
- `e`: Expression<T>
  - 微分対象の式木。

### Returns
- Expression<T>
  - 微分結果の式木。

### Exapmle

```csharp
using System;
using System.Linq.Expressions;
using TH.Utils;

Expression<Func<double, double>> f = x => x * x;
var df = f.SymbolicDerivative();

// Debug.Log($"df(x) = {df.Body}");

// double c = 1;
// Debug.Log($"df({c}) = {df.Compile()(c)}"));
```

### References
- https://ufcpp.net/study/csharp/sp3_expression.html
- https://ufcpp.net/study/csharp/sp3_expressionsample.html


<!-- -------------------------------------------------- -->
## SymbolicDerivative\<T>(Expression\<T>, string)
式木に対して、任意のパラメータに対する記号微分を行います。

```csharp
public static Expression<T> SymbolicDerivative<T>(this Expression<T> e, string paramName)
```

### Parameters
- `e`: Expression<T>
  - 微分対象の式木。
- `paramName`: string
  - 変数パラメータ。

### Returns
- Expression<T>
  - 微分結果の式木。

### References
- https://ufcpp.net/study/csharp/sp3_expressionsample.html

<!-- -------------------------------------------------- -->
## SymbolicDerivative\<T>(Expression\<T>, int)
式木に対して、任意のパラメータに対するn階記号微分を行います。

```csharp
public static Expression<T> SymbolicDerivative<T>(this Expression<T> e, int n)
```

### Parameters
- `e`: Expression<T>
  - 微分対象の式木。
- `n`: int
  - 微分階数。

### Returns
- Expression<T>
  - n階微分結果の式木。

### Example
```csharp
Expression<Func<double, double>> f = x => x * x * x * x;

int n = 3;
var df = f.SymbolicDerivative(n);

// Debug.Log($"df^({n})(x) = {df.Body}");
// result is 24*x
```


<!-- -------------------------------------------------- -->
## Simplify\<T>(Expression\<T>)
式木を単純化します。

```csharp
public static Expression<T> Simplify<T>(this Expression<T> e)
```

### Parameters
- `e`: Expression<T>
  - 対象の式木。

### Returns
- Expression<T>
  - 単純化された式木。

### References
- https://ufcpp.net/study/csharp/sp3_expressionsample.html

<!-- -------------------------------------------------- -->
## TaylorExpansion(this Expression<Func<double, double>>, int, double)
式木で与えられた関数のテイラー展開を返します。
`n`次の近似を計算します。
以下の式に従って計算を行います。

$$ \sum_{i=0}^{n} \dfrac{f^{(i)}(a)}{i!} (x-a)^{i} $$

```csharp
public static Expression<Func<double, double>> TaylorExpansion(this Expression<Func<double, double>> e, int n, double a)
```

### Parameters
- `e`: Expression<Func<double, double>>
  - 対象の式木。
- `n`: int
  - 近似する次数。
- `a`: double
  - 展開する点。この点のまわりで展開します。

### Returns
- Expression<Func<double, double>>
  - 結果の式木。

### Exapmle

```csharp
using System;
using System.Linq.Expressions;
using TH.Utils;

Expression<Func<double, double>> f = x => Math.Sin(x);
int order = 4;
double a = Math.PI / 2;

var taylorExpansion = f.TaylorExpansion(order, a);

// Debug.Log($"Taylor Expansion of {f.Body} = {taylorExpansion.Body}");
```