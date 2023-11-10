# TH-Utils-Math-Unity/TH.Utils.MathExtentions struct<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Methods](#methods)
  - [Approximately(double, double, double)](#approximatelydouble-double-double)
    - [Parameters](#parameters)
    - [Returns](#returns)
    - [Exapmle](#exapmle)
  - [Arange(int, int, int)](#arangeint-int-int)
    - [Parameters](#parameters-1)
    - [Returns](#returns-1)
    - [Exapmle](#exapmle-1)
  - [Arange(double, double, int)](#arangedouble-double-int)
    - [Parameters](#parameters-2)
    - [Returns](#returns-2)
  - [DoubleFactorial(int)](#doublefactorialint)
    - [Parameters](#parameters-3)
    - [Returns](#returns-3)
  - [Linspace(int, int, int, bool)](#linspaceint-int-int-bool)
    - [Parameters](#parameters-4)
    - [Returns](#returns-4)
  - [Linspace(double, double, int, bool)](#linspacedouble-double-int-bool)
    - [Parameters](#parameters-5)
    - [Returns](#returns-5)
  - [MultiSin(double\[\], double\[\], double)](#multisindouble-double-double)
    - [Parameters](#parameters-6)
    - [Returns](#returns-6)
    - [Exapmle](#exapmle-2)
  - [Factorial(double)](#factorialdouble)
    - [Parameters](#parameters-7)
    - [Returns](#returns-7)
  - [Remap(double, double, double, double, double)](#remapdouble-double-double-double-double)
    - [Parameters](#parameters-8)
    - [Returns](#returns-8)
</details>


# Definition
Namespace: TH.Utils

System.Mathを拡張する静的メソッドを提供します。
単精度での計算を行う場合は、[TH.Utils.MathfExtentions](/doc_MathfExtentions.md)も参照ください。

# Methods
<!-- -------------------------------------------------- -->
## Approximately(double, double, double)
引数`a`と`b`がほぼ等しい値かどうかを判定します。
Double型はおよそ16桁の有効数字を持っています。
そのため、このメソッドでは有効桁数の範囲内で等しいかどうかを判定します。

以下の例のプログラムを参照してください。
見かけ上`a`と`b`は同じになりますが、内部では異なる値として扱われます。


```csharp
public static bool Approximately(double a, double b, double epsilon = 1e-15)
```

### Parameters
- `a`,`b`: double
  - 比較する値。

### Returns
- bool
  - 2つの引数の値がほぼ等しい場合にtrueを返します。

### Exapmle

```csharp
using System;
using UnityEngine;
using TH.Utils;

double a = 0.1;
double b = 1.1 - 1.0;

// false
Debug.Log(a == b);

// true
Debug.Log(MathExtentions.Approximately(a, b));
```

<!-- -------------------------------------------------- -->
## Arange(int, int, int)
等差数列を返します。数列は以下の範囲で作成します。
間隔は`step`です。

$$ start \leqq n < stop $$


```csharp
public static double[] Arange(int start, int stop, int step)
```

### Parameters
- `start`: int
  - 等差数列の開始値。
- `stop`: int
  - 等差数列の終値。この値より1 `step` 前の値までが数列に含まれます。
- `step`: int
  - 数列の間隔。

### Returns
- double[]
  - 等差数列。
  
### Exapmle

```csharp
using TH.Utils;

var array = MathExtentions.Arange(0, 5, 1);
// 0, 1, 2, 3, 4
```

<!-- -------------------------------------------------- -->
## Arange(double, double, int)
等差数列を返します。数列は以下の範囲で作成します。
間隔は`step`です。

$$ start \leqq n < stop $$


```csharp
public static double[] Arange(double start, double stop, int step)
```

### Parameters
- `start`: double
  - 等差数列の開始値。
- `stop`: double
  - 等差数列の終値。この値より1 `step` 前の値までが数列に含まれます。
- `step`: int
  - 数列の間隔。

### Returns
- double[]
  - 等差数列。

<!-- -------------------------------------------------- -->
## DoubleFactorial(int)
`n` の二重階乗 $n!!$ を計算します。
階乗の二回反復(n!)!とは異なります。

`n`が偶数の場合は以下の式に従って計算を行います。

$$ n!! = \prod_{k=0}^{n/2} (2k) = n(n-2)(n-4) \cdots 4 \cdot 2  $$

`n`が奇数の場合は以下の式に従って計算を行います。

$$ n!! = \prod_{k=0}^{(n+1)/2} (2k-1) = n(n-2)(n-4) \cdots 3 \cdot 1  $$

```csharp
public static int DoubleFactorial(int n)
```

### Parameters
- `n`: double
  - 二重階乗を計算する値。

### Returns
- double
  - nの二重階乗

<!-- -------------------------------------------------- -->
## Linspace(int, int, int, bool)
要素数を指定して等差数列を作成します。


```csharp
public static double[] Linspace(int start, int stop, int num, bool endPoint = true)
```

### Parameters
- `start`: int
  - 等差数列の開始値。
- `stop`: int
  - 等差数列の終値。
- `num`: int
  - 要素数。
- `endPoint`: bool
  - `stop`の値を含めるかどうか。デフォルトでは含めます。

### Returns
- double[]
  - 等差数列。

<!-- -------------------------------------------------- -->
## Linspace(double, double, int, bool)
要素数を指定して等差数列を作成します。


```csharp
public static double[] Linspace(double start, double stop, int num, bool endPoint = true)
```

### Parameters
- `start`: double
  - 等差数列の開始値。
- `stop`: double
  - 等差数列の終値。
- `num`: int
  - 要素数。
- `endPoint`: bool
  - `stop`の値を含めるかどうか。デフォルトでは含めます。

### Returns
- double[]
  - 等差数列。

<!-- -------------------------------------------------- -->
## MultiSin(double[], double[], double)
以下の式であらわされる、複合正弦波形の任意の時刻における値を返します。
引数`amp`と`omega`の配列の長さは一致している必要があります。
引数`amp`の長さ分だけ正弦波形を足し合わせます。

$$ \sum_{i=0}^{n} A_{i} \sin(\omega_{i} t) $$


```csharp
public static double MultiSinAngularFreq(double[] amp, double[] omega, double t)
```

### Parameters
- `amp`: double[]
  - 振幅配列。
- `omega`: double[]
  - 角速度配列。
- `t`: double
  - 時間。

### Returns
- double
  - 振幅A、角速度 $\omega$ の正弦波形の時刻tにおける値。

### Exapmle

```csharp
using System;
using UnityEngine;
using TH.Utils;

private double[] _ampx = new double[3] { 1, 1, 1 };
private double[] _ampy = new double[3] { 1, 1, 1 };
private double[] _ampz = new double[3] { 1, 1, 1 };
private double[] _omegax = new double[3] { 0.2f, 0.5f, 1 };
private double[] _omegay = new double[3] { 0.3f, 0.1f, 1 };
private double[] _omegaz = new double[3] { 0.6f, 0.4f, 1 };

double x = MathExtentions.MultiSin(_ampx, _omegax, Time.realtimeSinceStartup);
double y = MathExtentions.MultiSin(_ampy, _omegay, Time.realtimeSinceStartup);
double z = MathExtentions.MultiSin(_ampz, _omegaz, Time.realtimeSinceStartup);
```

<!-- -------------------------------------------------- -->
## Factorial(double)
`n` の階乗を計算します。
階乗の計算は、比較的小さい `n` の場合でも非常に大きな値になる場合があります。
そのため、オーバーフローに注意してください。

$$ n! = n \times (n-1) \times (n-2) \times \cdots \times 3 \times 2 \times 1  $$


```csharp
public static double Factorial(double n)
```

### Parameters
- `n`: double
  - 階乗を計算する値。

### Returns
- double
  - nの階乗

<!-- -------------------------------------------------- -->
## Remap(double, double, double, double, double)
任意の範囲の値を別の範囲に変換します。

```csharp
public static double Remap(double val, double minFrom, double maxFrom, double minTo, double maxTo)
```

### Parameters
- `val`: double
  - 変換対象の値。
- `minFrom`: double
  - 変換前の範囲の最小値。
- `maxFrom`: double
  - 変換前の範囲の最大値。
- `minTo`: double
  - 変換後の範囲の最小値。
- `maxTo`: double
  - 変換後の範囲の最大値。

### Returns
- double
  - 任意の範囲に変換された値。