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
</details>


# Definition
Namespace: TH.Utils

特殊関数に関する静的メソッドを提供します。

# Methods
<!-- -------------------------------------------------- -->
## Gamma(double)
スターリングの近似を用いたガンマ関数を計算します。

ガンマ関数の定義は以下です。

$$ \Gamma(z) = \int_{0}^{\infty} t^{z-1} e^{-t} dt$$

この関数ではスターリングの近似を用いて計算を行います。
ここでは[Nemes (2010)](https://doi.org/10.1007/s00013-010-0146-9) による近似式を用います。近似式は以下です。

$$ \Gamma(z) \sim \sqrt{\dfrac{2 \pi}{z}} \left( \dfrac{1}{e} \left( z + \dfrac{1}{12z - \dfrac{1}{10z}} \right) \right)^z $$

また、一部の`z`に対しては近似値ではなく、以下の式に従って正しい値を計算します。
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