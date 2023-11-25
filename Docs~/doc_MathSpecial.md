# TH-Utils-Math-Unity/TH.Utils.MathSpecial class<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-Creative Commons Attribution ShareAlike 4.0-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Methods](#methods)
  - [Gamma(double)](#gammadouble)
    - [Parameters](#parameters)
    - [Returns](#returns)
    - [Exapmle](#exapmle)
    - [Reference](#reference)
  - [Beta(double)](#betadouble)
    - [Parameters](#parameters-1)
    - [Returns](#returns-1)
    - [Exapmle](#exapmle-1)
    - [Reference](#reference-1)
  - [BesselJ(double, double)](#besseljdouble-double)
    - [Parameters](#parameters-2)
    - [Returns](#returns-2)
    - [Exapmle](#exapmle-2)
    - [Reference](#reference-2)
  - [BesselI(double, double)](#besselidouble-double)
    - [Parameters](#parameters-3)
    - [Returns](#returns-3)
    - [Reference](#reference-3)
  - [Hyp0f1(double, double)](#hyp0f1double-double)
    - [Parameters](#parameters-4)
    - [Returns](#returns-4)
    - [Reference](#reference-4)
  - [Digamma(double)](#digammadouble)
    - [Parameters](#parameters-5)
    - [Returns](#returns-5)
    - [Reference](#reference-5)
  - [HarmonicNumber(int)](#harmonicnumberint)
    - [Parameters](#parameters-6)
    - [Returns](#returns-6)
    - [Reference](#reference-6)
</details>


# Definition
Namespace: TH.Utils

特殊関数に関する静的メソッドを提供します。
一部の関数は精度に問題があります。そのため、値の妥当性を慎重に検討してください。

# Methods
<!-- -------------------------------------------------- -->
## Gamma(double)
スターリングの近似を用いたガンマ関数を計算します。

ガンマ関数の定義は以下です。

$$ \Gamma(z) = \int_{0}^{\infty} t^{z-1} e^{-t} dt$$

この関数ではスターリングの近似を用いて計算を行います。
ここでは[Nemes (2010)](https://doi.org/10.1007/s00013-010-0146-9) による近似式を用います。近似式は以下です。

$$ \Gamma(z) \sim \sqrt{\dfrac{2 \pi}{z}} \left( \dfrac{1}{e} \left( z + \dfrac{1}{12z - \dfrac{1}{10z}} \right) \right)^z $$

また、一部の`z`に対しては以下の式に従って値を計算します。
- `z`が自然数の場合

$$ \Gamma(z) = (z-1)! $$

- `z`が1/2の場合

$$ \Gamma \left( \dfrac{1}{2} \right) = \sqrt{\pi} $$

- 上記より、自然数`n`に対して、 $z=\frac{1}{2}+n$ の時

$$ \Gamma \left( \dfrac{1}{2} + n \right) = \dfrac{(2n-1)!!}{2^n} \sqrt{\pi} $$

```csharp
public static double Gamma(double z)
```

### Parameters
- `z`: double
  - 実数。

### Returns
- double
  - ガンマ関数の近似値。

### Exapmle

```csharp
using TH.Utils;

double z = 3.0 / 2.0;
double res = MathSpecial.Gamma(z);
// 0.886226925452758
```

### Reference
- https://en.wikipedia.org/wiki/Stirling%27s_approximation
- Nemes Gergő, New asymptotic expansion for the Gamma function, Archiv der Mathematik, 95, 161-169, 2010. https://doi.org/10.1007/s00013-010-0146-9

<!-- -------------------------------------------------- -->
## Beta(double)
ベータ関数を計算します。
ベータ関数の定義は以下です。

$$ \mathrm{B}(x, y) = \int_{0}^{1} t^{x-1} (1-t)^{y-1} dt$$

この関数では、ガンマ関数との関係を用いて、以下の式に従って計算を行います。

$$ \mathrm{B}(x, y) = \dfrac{\Gamma(x) \Gamma(y)}{\Gamma(x+y)} $$


```csharp
public static double Beta(double x, double y)
```

### Parameters
- `x`,`y`: double
  - 実数。

### Returns
- double
  - ベータ関数の値。

### Exapmle

```csharp
using TH.Utils;

double x = 1.0 / 2.0;
double y = 1.0 / 2.0;
double res = MathSpecial.Beta(x, y);
// 3.14159265358979
```

### Reference
- https://en.wikipedia.org/wiki/Beta_function

<!-- -------------------------------------------------- -->
## BesselJ(double, double)
第1種ベッセル関数を計算します。
定義は以下です。

$$ z^2 \frac{d^2y}{dz^2} + z \frac{dy}{dz} + (z^2 - v^2)y = 0$$

第1種ベッセル関数は $J_v(z)$ または $J_{-v}(z)$ と表記されます。
この関数では、以下の式に従って計算を行います。

$$ J_v(z) = \sum_{k=0}^{\infty} \frac{(-1)^k}{k! \Gamma(k + v + 1)} \left( \frac{z}{2} \right)^{2k + v} $$

ただし、この関数では一定の範囲までの展開しかできていません。
そのため、 $v$ が非整数の場合は精度が著しく低下します。

```csharp
public static double BesselJ(double v, double z)
```

### Parameters
- `v`: double
  - 次数。

- `x`: double
  - 定義域。

### Returns
- double
  - 第1種ベッセル関数の値。

### Exapmle

```csharp
using TH.Utils;

double res = MathSpecial.BesselJ(1, 1.12);
// 0.476663355426007
```

### Reference
- https://en.wikipedia.org/wiki/Bessel_function

<!-- -------------------------------------------------- -->
## BesselI(double, double)
変形第1種ベッセル関数を計算します。
定義は以下です。

$$ I_v(z) = \sum_{k=0}^{\infty} \frac{1}{k! \Gamma(k + v + 1)} \left( \frac{z}{2} \right)^{2k + v} $$

この関数では近似値を求めるのみなので、値の妥当性に注意してください。

```csharp
public static double BesselI(double v, double z)
```

### Parameters
- `v`: double
  - 次数。

- `x`: double
  - 定義域。

### Returns
- double
  - 変形第1種ベッセル関数の値。

### Reference
- https://en.wikipedia.org/wiki/Bessel_function

<!-- -------------------------------------------------- -->
## Hyp0f1(double, double)
超幾何関数の近似値を計算します。
定義は以下です。

<!-- $$ {}_0F_1 (a, z) = \sum_{k=0}^{\infty} \frac{z^k}{(a)_k k!} $$ -->

$$ \sum_{k=0}^{\infty} \frac{z^k}{(a)_k k!} $$

$(a)_k$ は昇冪。
この関数では近似値を求めるのみなので、値の妥当性に注意してください。

```csharp
public static double Hyp0f1(double a, double z)
```

### Parameters
- `a`, `z`: double
  - 実数のパラメータ。

### Returns
- double
  - 超幾何関数の近似値。

### Reference
- https://en.wikipedia.org/wiki/Hypergeometric_function

<!-- -------------------------------------------------- -->
## Digamma(double)
ディガンマ関数の近似値を計算します。
定義は以下です。

$$ \psi(z) = \frac{\Gamma'(z)}{\Gamma(z)} $$

この関数では以下の式に従って計算します。
この関数では近似値を求めるのみなので、値の妥当性に注意してください。

$$ \psi(z) = -\gamma + \sum_{n=0}^{\infty} \frac{z-1}{(n+1)(z+n)} $$

```csharp
public static double Digamma(double z)
```

### Parameters
- `z`: double
  - 実数のパラメータ。

### Returns
- double
  - ディガンマ関数の近似値。

### Reference
- https://en.wikipedia.org/wiki/Digamma_function

<!-- -------------------------------------------------- -->
## HarmonicNumber(int)
調和数を計算します。
定義は以下です。

$$ H_n = \sum_{k=1}^{n} \frac{1}{k} $$

```csharp
public static double HarmonicNumber(int n)
```

### Parameters
- `n`: int
  - 自然数。

### Returns
- double
  - 調和数。

### Reference
- https://en.wikipedia.org/wiki/Harmonic_number