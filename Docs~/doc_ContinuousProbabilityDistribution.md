# TH-Utils-Math-Unity/TH.Utils.ContinuousProbabilityDistribution class<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Methods](#methods)
  - [StandardNormal()](#standardnormal)
    - [Returns](#returns)
  - [StandardNormal(float, float, int)](#standardnormalfloat-float-int)
    - [Parameters](#parameters)
    - [Returns](#returns-1)
    - [Exapmle](#exapmle)
  - [Normal(float, float)](#normalfloat-float)
    - [Parameters](#parameters-1)
    - [Returns](#returns-2)
  - [Normal(float, float, int, float, float)](#normalfloat-float-int-float-float)
    - [Parameters](#parameters-2)
    - [Returns](#returns-3)
    - [Exapmle](#exapmle-1)
- [References](#references)
</details>


# Definition
Namespace: TH.Utils

連続確率分布に関する静的メソッドを提供します。

# Methods
<!-- -------------------------------------------------- -->
## StandardNormal()
標準正規分布を計算します。
横軸の範囲は、デフォルトでは-3~3で、サンプル数は50です。

標準正規分布は平均 $\mu=0$ で、分散 $\sigma^2=0$ の場合の正規分布を表し、以下の式に従って計算を行います。

$$
f(x)
= \dfrac{1}{\sqrt{2 \pi}} \exp\left({- \dfrac{x^2}{2}}\right)
$$

```csharp
public static List<float> StandardNormal()
```

### Returns
- List<float>
  - 横軸-3~3でサンプル数50で計算された標準正規分布の値。

<!-- -------------------------------------------------- -->
## StandardNormal(float, float, int)
標準正規分布を計算します。


```csharp
public static List<float> StandardNormal(float minX, float maxX, int num)
```

### Parameters
- `minX`: float
  - 横軸の最小値。
- `maxX`: float
  - 横軸の最大値。
- `num`: int
  - サンプル数。

### Returns
- List<float>
  - 横軸の範囲内でサンプル数 `num` で計算された標準正規分布の値。

### Exapmle

```csharp
using System.Collections.Generic;
using TH.Utils;

var stdNormDist = ContinuousProbabilityDistribution.StandardNormal(-5, 5, 100);
```

<!-- -------------------------------------------------- -->
## Normal(float, float)
正規分布を計算します。
横軸の範囲は、デフォルトでは-3~3で、サンプル数は50です。

```csharp
public static List<float> Normal(float mean, float var)
```

### Parameters
- `mean`: float
  - 平均。 $\mu$
- `var`: float
  - 分散。 $\sigma^2$

### Returns
- List<float>
  - 横軸-3~3でサンプル数50で計算された正規分布の値。

<!-- -------------------------------------------------- -->
## Normal(float, float, int, float, float)
正規分布を計算します。
以下の式に従って計算を行います。

$$
f(x)
= \dfrac{1}{\sqrt{2 \pi \sigma^2}} \exp\left({- \dfrac{(x-\mu)^2}{2\sigma^2}}\right)
$$

```csharp
public static List<float> Normal(float minX, float maxX, int num, float mean, float var)
```

### Parameters
- `minX`: float
  - 横軸の最小値。
- `maxX`: float
  - 横軸の最大値。
- `num`: int
  - サンプル数。
- `mean`: float
  - 平均。 $\mu$
- `var`: float
  - 分散。 $\sigma^2$

### Returns
- List<float>
  - 横軸の範囲内でサンプル数 `num` で計算された正規分布の値。

### Exapmle

```csharp
using System.Collections.Generic;
using TH.Utils;

// range(-3, 3), sample = 99, mu = 0, sigma^2 = 1
var normalDist = ContinuousProbabilityDistribution.Normal(-3, 3, 99, 0, 1);
```

# References
- https://en.wikipedia.org/wiki/Normal_distribution