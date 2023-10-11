# TH-Utils-Math-Unity/TH.Utils.Matrix3x3 struct<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [Static Properties](#static-properties)
  - [identity](#identity)
  - [zero](#zero)
- [Properties](#properties)
  - [this\[int\]](#thisint)
  - [this\[int, int\]](#thisint-int)
- [Methods](#methods)
  - [GetColumn(int)](#getcolumnint)
    - [Parameters](#parameters)
    - [Returns](#returns)
  - [GetRow(int)](#getrowint)
    - [Parameters](#parameters-1)
    - [Returns](#returns-1)
  - [Inverse()](#inverse)
  - [Inverse(Matrix3x3)](#inversematrix3x3)
    - [Parameters](#parameters-2)
    - [Returns](#returns-2)
  - [Scale(Vector2)](#scalevector2)
    - [Parameters](#parameters-3)
    - [Returns](#returns-3)
  - [SetColumn(int, Vector3)](#setcolumnint-vector3)
    - [Parameters](#parameters-4)
  - [SetRow(int, Vector3)](#setrowint-vector3)
    - [Parameters](#parameters-5)
  - [ToString()](#tostring)
  - [ToString(string)](#tostringstring)
    - [Parameters](#parameters-6)
  - [ToString(string, IFormatProvider)](#tostringstring-iformatprovider)
    - [Parameters](#parameters-7)
  - [Translate(Vector2)](#translatevector2)
    - [Parameters](#parameters-8)
  - [T()](#t)
  - [Transpose()](#transpose)
  - [Transpose(Matrix3x3)](#transposematrix3x3)
    - [Parameters](#parameters-9)
    - [Returns](#returns-4)
- [Operators](#operators)
  - [operator \*](#operator-)
    - [Parameters](#parameters-10)
    - [Returns](#returns-5)
  - [operator \*](#operator--1)
    - [Parameters](#parameters-11)
    - [Returns](#returns-6)
  - [operator ==](#operator--2)
    - [Parameters](#parameters-12)
    - [Returns](#returns-7)
  - [operator !=](#operator--3)
    - [Parameters](#parameters-13)
    - [Returns](#returns-8)
</details>


# Definition
Namespace: TH.Utils

継承 Object -> System.IEquatable, System.IFormattable -> Matrix3x3

標準3x3変換行列を定義します。

変換行列は、任意の線形変換（平行移動、回転、スケール、シアーなど）や、同次座標を使った透視変換を行うことができます。
スクリプトで行列を使用することはほとんどありません。ほとんどの場合、Vector3やQuaternionを使用し、Transformクラスの機能を使用する方が簡単です。

データへのアクセスは、row + (column*3) となります。
行列は2D配列のようにインデックスを付けることができますが、mat[a, b]のような式では、aは行インデックス、bは列インデックスを指すことに注意してください。

# Static Properties
## identity
単位行列を返します。 (Read Only)

## zero
すべての要素をゼロに設定した行列を返します。 (Read Only)


# Properties
## this[int]
index指定して要素にアクセスします。

## this[int, int]
[行、列]を指定して要素にアクセスします。


# Methods
<!-- -------------------------------------------------- -->
## GetColumn(int)
行列の列を取得します。


```csharp
public Vector3 GetColumn(int index)
```

### Parameters
- `index`: int
  - 列インデックス。0~2である必要があります。

### Returns
- Vector3
  - index番目の列。

<!-- -------------------------------------------------- -->
## GetRow(int)
行列の行を取得します。


```csharp
public Vector3 GetRow(int index)
```

### Parameters
- `index`: int
  - 行インデックス。0~2である必要があります。

### Returns
- Vector3
  - index番目の行。

<!-- -------------------------------------------------- -->
## Inverse()
この行列を逆行列に変換します。もとの行列を変更します。


```csharp
public void Inverse()
```

<!-- -------------------------------------------------- -->
## Inverse(Matrix3x3)
任意の行列の逆行列を返します。もとの行列は変更されません。


```csharp
public static Matrix3x3 Inverse(Matrix3x3 m)
```

### Parameters
- `m`: Matrix3x3
  - 対象の行列。

### Returns
- Matrix3x3
  - 引数の行列の逆行列。

<!-- -------------------------------------------------- -->
## Scale(Vector2)
スケーリング行列を作成します。
返される行列はベクトルvectorによる座標の軸に沿ってスケールします。


```csharp
public static Matrix3x3 Scale(Vector2 vector)
```

### Parameters
- `vector`: Vector2
  - スケーリングする軸。

### Returns
- Matrix3x3
  - スケーリング行列。

<!-- -------------------------------------------------- -->
## SetColumn(int, Vector3)
指定した列の値をセットします。


```csharp
public void SetColumn(int index, Vector3 column)
```

### Parameters
- `index`: int
  - 対象の列インデックス。
- `column`: int
  - 列の値。

<!-- -------------------------------------------------- -->
## SetRow(int, Vector3)
指定した行の値をセットします。


```csharp
public void SetRow(int index, Vector3 row)
```

### Parameters
- `index`: int
  - 対象の行インデックス。
- `column`: int
  - 行の値。

<!-- -------------------------------------------------- -->
## ToString()
文字列に変換します。

```csharp
public override string ToString()
```

<!-- -------------------------------------------------- -->
## ToString(string)
フォーマットを指定して文字列に変換します。

```csharp
public string ToString(string format)
```

### Parameters
- `format`: string
  - 文字列のフォーマット。

<!-- -------------------------------------------------- -->
## ToString(string, IFormatProvider)
フォーマットを指定して文字列に変換します。

```csharp
public string ToString(string format, IFormatProvider formatProvider)
```

### Parameters
- `format`: string
  - 文字列のフォーマット。
- `formatProvider`: IFormatProvider
  - フォーマットプロバイダ。

<!-- -------------------------------------------------- -->
## Translate(Vector2)
変換行列を作成します。

```csharp
public static Matrix3x3 Translate(Vector2 vector)
```

### Parameters
- `vector`: Vector2
  - 変換スケール。

<!-- -------------------------------------------------- -->
## T()
この行列を転置行列に変換します。もとの行列を変更します。
Transpose()と同等です。

```csharp
public void T()
```

<!-- -------------------------------------------------- -->
## Transpose()
この行列を転置行列に変換します。もとの行列を変更します。

```csharp
public void Transpose()
```

<!-- -------------------------------------------------- -->
## Transpose(Matrix3x3)
任意の行列の転置行列を返します。もとの行列は変更されません。

```csharp
public static Matrix3x3 Transpose(Matrix3x3 m)
```

### Parameters
- `m`: Matrix3x3
  - 対象の行列。

### Returns
- Matrix3x3
  - 引数の行列の転置行列。


# Operators
<!-- -------------------------------------------------- -->
## operator *
2つの行列を乗算します。
返される結果は`lhs`*`rhs`です。

```csharp
public static Matrix3x3 operator *(Matrix3x3 lhs, Matrix3x3 rhs)
```

### Parameters
- `lhs`, `rhs`: Matrix3x3
  - 対象の行列。

### Returns
- Matrix3x3
  - 乗算結果の行列

<!-- -------------------------------------------------- -->
## operator *
行列にVector3を乗算します。
返される結果は`lhs`*`vector`です。

```csharp
public static Vector3 operator *(Matrix3x3 lhs, Vector3 vector)
```

### Parameters
- `lhs`: Matrix3x3
  - 行列。
- `vector`: Vector3
  - ベクトル。

### Returns
- Vector3
  - 乗算結果のVector3。

<!-- -------------------------------------------------- -->
## operator ==
2つの行列が等しいかどうかを判定します。

```csharp
public static bool operator ==(Matrix3x3 lhs, Matrix3x3 rhs)
```

### Parameters
- `lhs`, `rhs`: Matrix3x3
  - 対象の行列。

### Returns
- bool
  - 等しい場合、Trueを返します。

<!-- -------------------------------------------------- -->
## operator !=
2つの行列が等しくないかどうかを判定します。

```csharp
public static bool operator !=(Matrix3x3 lhs, Matrix3x3 rhs)
```

### Parameters
- `lhs`, `rhs`: Matrix3x3
  - 対象の行列。

### Returns
- bool
  - 等しくない場合、Trueを返します。