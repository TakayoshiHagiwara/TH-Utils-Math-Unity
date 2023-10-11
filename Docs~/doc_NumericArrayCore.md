# TH-Utils-Math-Unity/TH.Utils.NumericArrayCore class<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Methods](#methods)
  - [Diff(this int\[\], int order)](#diffthis-int-int-order)
    - [Parameters](#parameters)
    - [Returns](#returns)
    - [Exapmle](#exapmle)
  - [Diff(this float\[\], int order)](#diffthis-float-int-order)
    - [Parameters](#parameters-1)
    - [Returns](#returns-1)
</details>


# Definition
Namespace: TH.Utils

数値配列に関する静的メソッドを提供します。

# Methods
<!-- -------------------------------------------------- -->
## Diff(this int[], int order)
配列の各要素の差分を返します。
第2引数を指定することで繰り返し差分を計算します。


```csharp
public static int[] Diff(this int[] array, int order = 1)
```

### Parameters
- `array`: int[]
  - 数値配列。
- `order`: int
  - 差分計算を繰り返す回数。デフォルトは1です。

### Returns
- int[]
  - 差分結果配列。

### Exapmle

```csharp
using System;
using UnityEngine;
using TH.Utils;

public int[] array = new int[7] { 1, 2, 4, 1, 6, 8, 3 };
var diff = array.Diff(2);
// [1, -5, 8, -3, -7]
```

<!-- -------------------------------------------------- -->
## Diff(this float[], int order)
配列の各要素の差分を返します。
第2引数を指定することで繰り返し差分を計算します。


```csharp
public static float[] Diff(this float[] array, int order = 1)
```

### Parameters
- `array`: float[]
  - 数値配列。
- `order`: int
  - 差分計算を繰り返す回数。デフォルトは1です。

### Returns
- float[]
  - 差分結果配列。