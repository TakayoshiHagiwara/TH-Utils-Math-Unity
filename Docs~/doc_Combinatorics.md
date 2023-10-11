# TH-Utils-Math-Unity/TH.Utils.Combinatorics class<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Methods](#methods)
  - [BinomialCoefficients(int, int)](#binomialcoefficientsint-int)
    - [Parameters](#parameters)
    - [Returns](#returns)
    - [Exapmle](#exapmle)
    - [References](#references)
</details>


# Definition
Namespace: TH.Utils

組み合わせに関する静的メソッドを提供します。

# Methods
<!-- -------------------------------------------------- -->
## BinomialCoefficients(int, int)
二項係数を返します。
n個のオブジェクトの中から、k個のオブジェクトを選ぶ組み合わせの数 (より正確には、n個の要素集合のk個の部分集合 (またはk個の組み合わせ) の数) を計算します。
${}_nC_k$ は以下の式にしたがって計算されます。

$$
  C(n,k)
  = \begin{pmatrix} 
      n \\ 
      k 
    \end{pmatrix}
  = \prod^k_{i=1} \dfrac{(n+1-i)}{i}
$$


```csharp
public static double BinomialCoefficients(int n, int k)
```

### Parameters
- `n`: int
  - 要素集合。全体の数。
- `k`: int
  - 部分集合。組み合わせの数。

### Returns
- double
  - n個のオブジェクトの中から、k個のオブジェクトを選ぶ組み合わせの数。

### Exapmle

```csharp
using System;
using TH.Utils.Math;

double res = Combinatorics.BinomialCoefficients(10, 5);
// Debug.Log(res); // 252
```

### References
- https://en.wikipedia.org/wiki/Combinatorics
- https://en.wikipedia.org/wiki/Binomial_coefficient
