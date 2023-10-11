# TH-Utils-Math-Unity/TH.Utils.QuaternionConverter class<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Enum](#enum)
  - [Sequence](#sequence)
  - [RotationType](#rotationtype)
- [Methods](#methods)
  - [ToEulerAngles(this Quaternion, string, RotationType, bool)](#toeuleranglesthis-quaternion-string-rotationtype-bool)
    - [Parameters](#parameters)
    - [Returns](#returns)
    - [Exapmle](#exapmle)
  - [ToEulerAngles(this Quaternion, Sequence, RotationType, bool)](#toeuleranglesthis-quaternion-sequence-rotationtype-bool)
    - [Parameters](#parameters-1)
    - [Returns](#returns-1)
    - [Exapmle](#exapmle-1)
  - [ToQuaternion(this Vector3, Sequence, bool isEulerDeg)](#toquaternionthis-vector3-sequence-bool-iseulerdeg)
    - [Parameters](#parameters-2)
    - [Returns](#returns-2)
    - [Exapmle](#exapmle-2)
- [Algorithm](#algorithm)
  - [Quaternion to Euler angles](#quaternion-to-euler-angles)
  - [Euler angles to Quaternion](#euler-angles-to-quaternion)
- [References](#references)
</details>


# Definition
Namespace: TH.Utils

UnityEngine.Quaternionの変換に関する静的メソッドを提供します。

# Enum
## Sequence
XYZ, XZY, YXZ, YZX, ZXY, ZYX

オイラー表現の回転シーケンス。
回転シーケンスは、軸を中心とした回転の順序を定義します。たとえば、'XYZ' という回転シーケンスを指定した場合、次のようになります。
1. 最初の回転は、x軸を中心にして行われます。
2. 2 番目の回転は、新しい y軸を中心にして行われます。
3. 3 番目の回転は、新しい z軸を中心にして行われます。

## RotationType
Frame, Point

回転のタイプ。
点の回転では、点が移動し、座標系は静的です。座標系の回転では、点は静的で、座標系が移動します。点の回転と座標系の回転は同等の角変位を定義しますが、方向は逆です。

# Methods
<!-- -------------------------------------------------- -->
## ToEulerAngles(this Quaternion, string, RotationType, bool)
Quaternionを任意の回転シーケンスでEuler角に変換します。
Unityで使用されている回転シーケンス (ZXY) 以外を使用した場合、値はUnity上のものと異なることに注意してください。

```csharp
public static Vector3 ToEulerAngles(this Quaternion q, string sequence, RotationType type = RotationType.Point, bool isDeg = true)
```

### Parameters
- `q`: Quaternion
  - 変換するQuaternion。
- `sequence`: string
  - 回転シーケンス。
- `type`: RotationType
  - 回転のタイプ。
- `isDeg`: bool
  - Deg表現に変換するかどうか。falseの場合、Rad表現になります。

### Returns
- Vector3
  - Euler角。

### Exapmle

```csharp
using System;
using UnityEngine;
using TH.Utils;

public GameObject obj;

Quaternion q = obj.rotation;
Vector3 eulerAngles = q.ToEulerAngles("ZXY", QuaternionConverter.RotationType.Frame);
```

<!-- -------------------------------------------------- -->
## ToEulerAngles(this Quaternion, Sequence, RotationType, bool)
Quaternionを任意の回転シーケンスでEuler角に変換します。
Unityで使用されている回転シーケンス (ZXY) 以外を使用した場合、値はUnity上のものと異なることに注意してください。


```csharp
public static Vector3 ToEulerAngles(this Quaternion q, Sequence sequence, RotationType type = RotationType.Point, bool isDeg = true)
```

### Parameters
- `q`: Quaternion
  - 変換するQuaternion。
- `sequence`: Sequence
  - 回転シーケンス。
- `type`: RotationType
  - 回転のタイプ。
- `isDeg`: bool
  - Deg表現に変換するかどうか。falseの場合、Rad表現になります。

### Returns
- Vector3
  - Euler角。

### Exapmle

```csharp
using System;
using UnityEngine;
using TH.Utils;

public GameObject obj;

Quaternion q = obj.rotation;
Vector3 eulerAngles = q.ToEulerAngles(QuaternionConverter.Sequence.ZXY, QuaternionConverter.RotationType.Frame);
```

<!-- -------------------------------------------------- -->
## ToQuaternion(this Vector3, Sequence, bool isEulerDeg)
Euler角を任意の回転シーケンスでQuaternionに変換します。
Unityで使用されている回転シーケンス (ZXY) 以外を使用した場合、値はUnity上のものと異なることに注意してください。


```csharp
public static Quaternion ToQuaternion(this Vector3 euler, Sequence sequence, bool isEulerDeg = true)
```

### Parameters
- `euler`: Vector3
  - 変換するEuler角。
- `sequence`: Sequence
  - 回転シーケンス。
- `isEulerDeg`: bool
  - 変換するEuler角がDeg表現かどうか。

### Returns
- Quaternion
  - Quaternion。

### Exapmle

```csharp
using System;
using UnityEngine;
using TH.Utils;

Vector3 euler = new Vector3(0, 45, 45);
Quaternion q = euler.ToQuaternion(QuaternionConverter.Sequence.ZXY);
```


# Algorithm
## Quaternion to Euler angles
QuaternionからEuler角への変換については以下に従って計算を行います。ここでは例として、ZXYの順序での変換を行います。

まず、Quaternionから回転行列への変換を行います。
回転行列の各要素を次のように定義します。

$$
R = \begin{pmatrix} 
        m_{00} & m_{01} & m_{02} \\ 
        m_{10} & m_{11} & m_{12} \\ 
        m_{20} & m_{21} & m_{22}
      \end{pmatrix} 
$$

Quaternionの各要素を $(q_x, q_y, q_z, q_w)$ と定義します。
座標系の回転 (RotationType.Frame) に対応する回転行列は以下のように定義されます。

$$
m_F = \begin{pmatrix} 
        2q_w^2 + 2q_x^2 - 1 & 2q_xq_y - 2q_zq_w   & 2q_xq_z + 2q_yq_w \\ 
        2q_xq_y + 2q_zq_w   & 2q_w^2 + 2q_y^2 - 1 & 2q_yq_z - 2q_xq_w \\ 
        2q_xq_z - 2q_yq_w   & 2q_yq_z + 2q_xq_w   & 2q_w^2 + 2q_x^2 - 1
      \end{pmatrix} 
$$

また、座標系の回転 (RotationType.Point) に対応する回転行列は以下のように定義されます。

$$
m_P = \begin{pmatrix} 
        2q_w^2 + 2q_x^2 - 1 & 2q_xq_y + 2q_zq_w   & 2q_xq_z - 2q_yq_w \\ 
        2q_xq_y - 2q_zq_w   & 2q_w^2 + 2q_y^2 - 1 & 2q_yq_z + 2q_xq_w \\ 
        2q_xq_z + 2q_yq_w   & 2q_yq_z - 2q_xq_w   & 2q_w^2 + 2q_x^2 - 1
      \end{pmatrix} 
$$


ここで、XYZそれぞれの軸に対する回転行列を以下のように定義します。

$$ 
Rx 
=   \begin{pmatrix} 
        1 & 0 & 0 \\ 
        0 & C_x & -S_x \\ 
        0 & S_x & C_x 
    \end{pmatrix}  
, \quad
R_y
=   \begin{pmatrix} 
        C_y & 0 & S_y \\ 
        0 & 1 & 0 \\ 
        -S_y & 0 & C_y 
    \end{pmatrix} 
, \quad
R_z 
=   \begin{pmatrix} 
        C_z & -S_z & 0 \\ 
        S_z & C_z & 0 \\ 
        0 & 0 & 1 
    \end{pmatrix} 
$$

それぞれの要素は、以下のように定義します。

$$ S_x = \sin(\theta_x), \quad S_y = \sin(\theta_y), \quad S_z = \sin(\theta_z)  $$

$$ C_x = \cos(\theta_x), \quad C_y = \cos(\theta_y), \quad C_z = \cos(\theta_z) $$


この回転行列のZXY順序での積は以下のようになります。

$$
R_zR_xR_y = \begin{pmatrix} 
        C_zC_y - S_zS_xS_y  & -S_zC_x   & C_zS_y + S_zS_xC_y \\ 
        S_zC_y + C_zS_xS_y  & C_zC_y    & S_xS_y - C_zS_xC_y \\ 
        -C_xS_y             & S_x       & C_xC_y
      \end{pmatrix} 
$$


ここで、 $m_{21}$ から、Euler角のxである $\theta_x$ が得られます。
$$m_{21} = \sin(\theta_x)$$

$$ \theta_x = \arcsin(m_{21})$$


同様にして、 $\theta_y, \theta_z$ は以下のようにして得られます。

$$ \frac{-S_y}{C_y} = \frac{m_{20}}{m_{22}} = -\tan(\theta_y)$$

$$ \theta_y = \arctan(-m_{20}, m_{22})$$

$$ \frac{-S_z}{C_z} = \frac{m_{01}}{m_{11}} = -\tan(\theta_z)$$

$$ \theta_z = \arctan(-m_{01}, m_{11})$$


ここで、 $\cos(\theta_x) = 0$ のとき、ジンバルロックとなり値が一意に求まらないため、 $\theta_y = 0$ と仮定すると、以下のようにして得られます。

$$ \frac{-S_z}{C_z} = \frac{m_{10}}{m_{00}} = -\tan(\theta_z)$$

$$ \theta_z = \arctan(-m_{10}, m_{00})$$



よって、

$$ \theta_x = \arcsin(m_{21}) $$

$$ 
\theta_y = 
  \begin{cases}
    \arctan(-\frac{m_{20}}{m_{22}}) & when \cos(\theta_x) \ne 0 \\
    0 & when \cos(\theta_x) = 0
  \end{cases}
$$

$$ 
\theta_z =
  \begin{cases}
    \arctan(-\frac{m_{01}}{m_{11}}) & when \cos(\theta_x) \ne 0 \\
    \arctan(\frac{m_{10}}{m_{00}}) & when \cos(\theta_x) = 0
  \end{cases}
$$

が得られます。座標系の回転 (RotationType.Frame) に対応する回転行列を例とすると、以下のようにして得られます。

$$ \theta_x = \arcsin(2q_yq_z + 2q_xq_w) $$

$$
\theta_y = 
  \begin{cases}
    \arctan(-\frac{2q_xq_z - 2q_yq_w}{2q_w^2 + 2q_z^2 - 1}) & when \cos(\theta_x) \ne 0 \\
    0 & when \cos(\theta_x) = 0
  \end{cases}
$$

$$
\theta_z =
  \begin{cases}
    \arctan(-\frac{2q_xq_y - 2q_zq_w}{2q_w^2 + 2q_y^2 - 1}) & when \cos(\theta_x) \ne 0 \\
    \arctan(\frac{2q_xq_y + 2q_zq_w}{2q_w^2 + 2q_x^2 - 1}) & when \cos(\theta_x) = 0
  \end{cases}
$$


## Euler angles to Quaternion
Euler角からQuaternionへの変換については以下に従って計算を行います。ここでは例として、ZXYの順序での変換を行います。

単位ベクトル $n = (n_x, n_y, n_z)$ を軸として、 $\theta$ だけ回転させるQuaternionは以下のようにあらわされます。

$$
q = 
  \begin{pmatrix} 
    n_x \sin\frac{\theta}{2} \\
    n_y \sin\frac{\theta}{2} \\
    n_z \sin\frac{\theta}{2} \\
    \cos\frac{\theta}{2} \\
  \end{pmatrix} 
$$

XYZ軸それぞれの回転をあらわすQuaternionは以下のようにあらわされます。

$$
q_x = 
  \begin{pmatrix} 
    \sin\frac{\theta_x}{2} \\
    0 \\
    0 \\
    \cos\frac{\theta}{2} \\
  \end{pmatrix} 
, \quad
q_y = 
  \begin{pmatrix} 
    0 \\
    \sin\frac{\theta_y}{2} \\
    0 \\
    \cos\frac{\theta}{2} \\
  \end{pmatrix}
, \quad
q_z = 
  \begin{pmatrix} 
    0 \\
    0 \\
    \sin\frac{\theta_z}{2} \\
    \cos\frac{\theta}{2} \\
  \end{pmatrix} 
$$

この3つの積を求めることで、Euler角からQuaternionへの変換を行います。ここでは例としてZXY順序での変換を行います。

$$
q_zq_xq_y = 
  \begin{pmatrix} 
    -\cos\frac{\theta_x}{2} \sin\frac{\theta_y}{2} \sin\frac{\theta_z}{2} + \sin\frac{\theta_x}{2} \cos\frac{\theta_y}{2} \cos\frac{\theta_z}{2} \\
    \cos\frac{\theta_x}{2} \sin\frac{\theta_y}{2} \cos\frac{\theta_z}{2} + \sin\frac{\theta_x}{2} \cos\frac{\theta_y}{2} \sin\frac{\theta_z}{2} \\
    \sin\frac{\theta_x}{2} \sin\frac{\theta_y}{2} \cos\frac{\theta_z}{2} + \cos\frac{\theta_x}{2} \cos\frac{\theta_y}{2} \sin\frac{\theta_z}{2} \\
    -\sin\frac{\theta_x}{2} \sin\frac{\theta_y}{2} \sin\frac{\theta_z}{2} + \cos\frac{\theta_x}{2} \cos\frac{\theta_y}{2} \cos\frac{\theta_z}{2} \\
  \end{pmatrix} 
$$

よって、Quaternionの各要素を $(x, y, z, w)$ と定義すると、以下のようにして得られます。

$$
\begin{aligned}
  x &= -\cos\frac{\theta_x}{2} \sin\frac{\theta_y}{2} \sin\frac{\theta_z}{2} + \sin\frac{\theta_x}{2} \cos\frac{\theta_y}{2} \cos\frac{\theta_z}{2} \\
  y &= \cos\frac{\theta_x}{2} \sin\frac{\theta_y}{2} \cos\frac{\theta_z}{2} + \sin\frac{\theta_x}{2} \cos\frac{\theta_y}{2} \sin\frac{\theta_z}{2} \\
  z &= \sin\frac{\theta_x}{2} \sin\frac{\theta_y}{2} \cos\frac{\theta_z}{2} + \cos\frac{\theta_x}{2} \cos\frac{\theta_y}{2} \sin\frac{\theta_z}{2} \\
  w &= -\sin\frac{\theta_x}{2} \sin\frac{\theta_y}{2} \sin\frac{\theta_z}{2} + \cos\frac{\theta_x}{2} \cos\frac{\theta_y}{2} \cos\frac{\theta_z}{2} \\
\end{aligned}
$$


# References
- https://qiita.com/aa_debdeb/items/3d02e28fb9ebfa357eaf
- https://qiita.com/edo_m18/items/5db35b60112e281f840e
- https://jp.mathworks.com/help/driving/ref/quaternion.rotmat.html
- https://jp.mathworks.com/help/driving/ref/quaternion.euler.html
