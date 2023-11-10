# TH-Utils-Math-Unity/TH.Utils.MathfExtentions struct<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Methods](#methods)
  - [Arange(float, float, int)](#arangefloat-float-int)
    - [Parameters](#parameters)
    - [Returns](#returns)
    - [Exapmle](#exapmle)
  - [Linspace(float, float, int, bool)](#linspacefloat-float-int-bool)
    - [Parameters](#parameters-1)
    - [Returns](#returns-1)
  - [MultiSin(float\[\], float\[\], float)](#multisinfloat-float-float)
    - [Parameters](#parameters-2)
    - [Returns](#returns-2)
    - [Exapmle](#exapmle-1)
  - [Remap(float, float, float, float, float)](#remapfloat-float-float-float-float)
    - [Parameters](#parameters-3)
    - [Returns](#returns-3)
</details>


# Definition
Namespace: TH.Utils

UnityEngine.Mathfを拡張する静的メソッドを提供します。
倍精度での計算を行う場合は、[TH.Utils.MathExtentions](/doc_MathExtentions.md)も参照ください。

# Methods
<!-- -------------------------------------------------- -->
## Arange(float, float, int)
等差数列を返します。数列は以下の範囲で作成します。
間隔は`step`です。

$$ start \leqq n < stop $$


```csharp
public static float[] Arange(float start, float stop, int step)
```

### Parameters
- `start`: float
  - 等差数列の開始値。
- `stop`: float
  - 等差数列の終値。この値より1 `step` 前の値までが数列に含まれます。
- `step`: int
  - 数列の間隔。

### Returns
- float[]
  - 等差数列。

### Exapmle

```csharp
using TH.Utils;

var array = MathfExtentions.Arange(0, 5, 1);
// 0, 1, 2, 3, 4
```

<!-- -------------------------------------------------- -->
## Linspace(float, float, int, bool)
要素数を指定して等差数列を作成します。


```csharp
public static float[] Linspace(float start, float stop, int num, bool endPoint = true)
```

### Parameters
- `start`: float
  - 等差数列の開始値。
- `stop`: float
  - 等差数列の終値。
- `num`: int
  - 要素数。
- `endPoint`: bool
  - `stop`の値を含めるかどうか。デフォルトでは含めます。

### Returns
- float[]
  - 等差数列。

<!-- -------------------------------------------------- -->
## MultiSin(float[], float[], float)
以下の式であらわされる、複合正弦波形の任意の時刻における値を返します。
引数`amp`と`omega`の配列の長さは一致している必要があります。
引数`amp`の長さ分だけ正弦波形を足し合わせます。

$$ \sum_{i=0}^{n} A_{i} \sin(\omega_{i} t) $$


```csharp
public static float MultiSinAngularFreq(float[] amp, float[] omega, float t)
```

### Parameters
- `amp`: float[]
  - 振幅配列。
- `omega`: float[]
  - 角速度配列。
- `t`: float
  - 時間。

### Returns
- float
  - 振幅A、角速度 $\omega$ の正弦波形の時刻tにおける値。

### Exapmle

```csharp
using System;
using UnityEngine;
using TH.Utils;

private float[] _ampx = new float[3] { 1, 1, 1 };
private float[] _ampy = new float[3] { 1, 1, 1 };
private float[] _ampz = new float[3] { 1, 1, 1 };
private float[] _omegax = new float[3] { 0.2f, 0.5f, 1 };
private float[] _omegay = new float[3] { 0.3f, 0.1f, 1 };
private float[] _omegaz = new float[3] { 0.6f, 0.4f, 1 };

float x = MathfExtentions.MultiSin(_ampx, _omegax, Time.realtimeSinceStartup);
float y = MathfExtentions.MultiSin(_ampy, _omegay, Time.realtimeSinceStartup);
float z = MathfExtentions.MultiSin(_ampz, _omegaz, Time.realtimeSinceStartup);

// Move some object.
// this.position = new Vector3(x, y, z);
```

<!-- -------------------------------------------------- -->
## Remap(float, float, float, float, float)
任意の範囲の値を別の範囲に変換します。

```csharp
public static float Remap(float val, float minFrom, float maxFrom, float minTo, float maxTo)
```

### Parameters
- `val`: float
  - 変換対象の値。
- `minFrom`: float
  - 変換前の範囲の最小値。
- `maxFrom`: float
  - 変換前の範囲の最大値。
- `minTo`: float
  - 変換後の範囲の最小値。
- `maxTo`: float
  - 変換後の範囲の最大値。

### Returns
- float
  - 任意の範囲に変換された値。