# TH-Utils-Math-Unity<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">

Unityにおける数学関連のメソッドを扱います。
著者が個人的な開発を行っていた際に必要になったちょっとしたメソッドをまとめています。

もともと個人的な使用で開発していたものなので、一部の関数は正確ではなかったり、精度が低かったりする場合があります。
使用の際は値の妥当性を慎重に検討し、より精度を求める場合は別のライブラリの使用を検討してください。

# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Environment](#environment)
- [Installation](#installation)
  - [Unity Package Manager経由での導入](#unity-package-manager経由での導入)
- [Description](#description)
  - [ArrayLengthMismatchException class](#arraylengthmismatchexception-class)
    - [Definition](#definition)
  - [Calculus class](#calculus-class)
    - [Definition](#definition-1)
    - [Methods](#methods)
  - [Combinatorics class](#combinatorics-class)
    - [Definition](#definition-2)
    - [Methods](#methods-1)
  - [ContinuousProbabilityDistribution class](#continuousprobabilitydistribution-class)
    - [Definition](#definition-3)
    - [Methods](#methods-2)
  - [ExpressionNodeTypeException class](#expressionnodetypeexception-class)
    - [Definition](#definition-4)
  - [ListLengthMismatchException class](#listlengthmismatchexception-class)
    - [Definition](#definition-5)
  - [MathExtentions struct](#mathextentions-struct)
    - [Definition](#definition-6)
    - [Methods](#methods-3)
  - [MathfExtentions struct](#mathfextentions-struct)
    - [Definition](#definition-7)
    - [Methods](#methods-4)
  - [MathSpecial class](#mathspecial-class)
    - [Definition](#definition-8)
    - [Methods](#methods-5)
  - [Matrix3x3 struct](#matrix3x3-struct)
    - [Definition](#definition-9)
    - [Static Properties](#static-properties)
    - [Properties](#properties)
    - [Methods](#methods-6)
    - [Operators](#operators)
  - [NumericArrayCore class](#numericarraycore-class)
    - [Definition](#definition-10)
    - [Methods](#methods-7)
  - [QuaternionConverter class](#quaternionconverter-class)
    - [Definition](#definition-11)
    - [Methods](#methods-8)
- [References](#references)
- [Troubleshooting](#troubleshooting)
- [Versions](#versions)
- [Author](#author)
- [License](#license)
</details>

# Environment
- Unity 2021 or Later

# Installation
## Unity Package Manager経由での導入
1. Window -> Package Managerを開きます
2. 左上のプラスアイコンをクリックし、「Add package from git URL...」をクリックします
3. このリポジトリURLを入力し、addをクリックします


# Description
<!-- -------------------------------------------------- -->
## ArrayLengthMismatchException class
### Definition
- Namespace: TH.Utils

配列の長さが不一致だった場合の例外を提供します。
参照: [ArrayLengthMismatchException](/Docs~/doc_Exception.md#arraylengthmismatchexception)



<!-- -------------------------------------------------- -->
## Calculus class
### Definition
- Namespace: TH.Utils

微分・積分に関する静的メソッドを提供します。

### Methods
| Name | Summary |
| ---- | ---- |
| [Derivative(Func<double, double>, int, double)](/Docs~/doc_Calculus.md#derivativefuncdouble-double-int-double) | 有限差分による数値微分を行い任意の点における微分係数を計算します。 |
| [Integral(Func<double, double>, double, double, int)](/Docs~/doc_Calculus.md#integralfuncdouble-double-double-double-int) | 数値積分を計算します。 |
| [IsIdenticalTo(Expression, Expression)](/Docs~/doc_Calculus.md#isidenticaltoexpression-expression) | 2つの式木が同等かどうかを判定します。 |
| [IsZero(Expression)](/Docs~/doc_Calculus.md#iszeroexpression) | 式木が0かどうかを判定します。 |
| [MaclaurinExpansion(this Expression<Func<double, double>>, int)](/Docs~/doc_Calculus.md#maclaurinexpansionthis-expressionfuncdouble-double-int) | 式木で与えられた関数のマクローリン展開を返します。 |
| [SymbolicDerivative\<T>(Expression\<T>)](/Docs~/doc_Calculus.md#symbolicderivativetexpressiont) | 式木に対して記号微分を行います。 |
| [SymbolicDerivative\<T>(Expression\<T>, string)](/Docs~/doc_Calculus.md#symbolicderivativetexpressiont-string) | 式木に対して、任意のパラメータに対する記号微分を行います。 |
| [SymbolicDerivative\<T>(Expression\<T>, int)](/Docs~/doc_Calculus.md#symbolicderivativetexpressiont-int) | 式木に対して、任意のパラメータに対するn階記号微分を行います。 |
| [Simplify\<T>(Expression\<T>)](/Docs~/doc_Calculus.md#simplifytexpressiont) | 式木を単純化します。 |
| [TaylorExpansion(this Expression<Func<double, double>>, int, double)](/Docs~/doc_Calculus.md#taylorexpansionthis-expressionfuncdouble-double-int-double) | 式木で与えられた関数のテイラー展開を返します。 |



<!-- -------------------------------------------------- -->
## Combinatorics class
### Definition
- Namespace: TH.Utils

組み合わせに関する静的メソッドを提供します。

### Methods
| Name | Summary |
| ---- | ---- |
| [BinomialCoefficients(int, int)](/Docs~/doc_Combinatorics.md#binomialcoefficientsint-int) | 二項係数を返します。 |



<!-- -------------------------------------------------- -->
## ContinuousProbabilityDistribution class
### Definition
- Namespace: TH.Utils

連続確率分布に関する静的メソッドを提供します。

### Methods
| Name | Summary |
| ---- | ---- |
| [StandardNormal()](/Docs~/doc_ContinuousProbabilityDistribution.md#standardnormal) | 標準正規分布を計算します。 |
| [StandardNormal(float, float, int)](/Docs~/doc_ContinuousProbabilityDistribution.md#standardnormalfloat-float-int) | 標準正規分布を計算します。 |
| [Normal(float, float)](/Docs~/doc_ContinuousProbabilityDistribution.md#normalfloat-float) | 正規分布を計算します。 |
| [Normal(float, float, int, float, float)](/Docs~/doc_ContinuousProbabilityDistribution.md#normalfloat-float-int-float-float) | 正規分布を計算します。 |



<!-- -------------------------------------------------- -->
## ExpressionNodeTypeException class
### Definition
- Namespace: TH.Utils

式木のNodeTypeが不正な場合のエラーを表します。
参照: [ExpressionNodeTypeException](/Docs~/doc_Exception.md#expressionnodetypeexception)



<!-- -------------------------------------------------- -->
## ListLengthMismatchException class
### Definition
- Namespace: TH.Utils

2つのListの長さが一致していない場合のエラーを表します。
参照: [ListLengthMismatchException](/Docs~/doc_Exception.md#listlengthmismatchexception)



<!-- -------------------------------------------------- -->
## MathExtentions struct
### Definition
- Namespace: TH.Utils

System.Mathを拡張する静的メソッドを提供します。

### Methods
| Name | Summary |
| ---- | ---- |
| [Approximately(double, double, double)](/Docs~/doc_MathExtentions.md#approximatelydouble-double-double) | 2つの引数がほぼ等しい値かどうかを判定します。 |
| [Arange(int, int, int)](/Docs~/doc_MathExtentions.md#arangeint-int-int) | 等差数列を返します。 |
| [Arange(double, double, int)](/Docs~/doc_MathExtentions.md#arangedouble-double-int) | 等差数列を返します。 |
| [DoubleFactorial(int)](/Docs~/doc_MathExtentions.md#doublefactorialint) | 二重階乗を計算します。 |
| [Linspace(int, int, int, bool)](/Docs~/doc_MathExtentions.md#linspaceint-int-int-bool) | 要素数を指定して等差数列を作成します。 |
| [Linspace(double, double, int, bool)](/Docs~/doc_MathExtentions.md#linspacedouble-double-int-bool) | 要素数を指定して等差数列を作成します。 |
| [MultiSin(double[], double[], double)](/Docs~/doc_MathExtentions.md#multisindouble-double-double) | 複合正弦波形の任意の時刻における値を返します。 |
| [Factorial(double)](/Docs~/doc_MathExtentions.md#factorialdouble) | 階乗を計算します。 |
| [Remap(double, double, double, double, double)](/Docs~/doc_MathExtentions.md#remapdouble-double-double-double-double) | 任意の範囲の値を別の範囲に変換します。 |



<!-- -------------------------------------------------- -->
## MathfExtentions struct
### Definition
- Namespace: TH.Utils

UnityEngine.Mathfを拡張する静的メソッドを提供します。

### Methods
| Name | Summary |
| ---- | ---- |
| [Arange(float, float, int)](/Docs~/doc_MathfExtentions.md#arangefloat-float-int) | 等差数列を返します。 |
| [Linspace(float, float, int, bool)](/Docs~/doc_MathfExtentions.md#linspacefloat-float-int-bool) | 要素数を指定して等差数列を作成します。 |
| [MultiSin(float[], float[], float)](/Docs~/doc_MathfExtentions.md#multisinfloat-float-float) | 複合正弦波形の任意の時刻における値を返します。 |
| [Remap(float, float, float, float, float)](/Docs~/doc_MathfExtentions.md#remapfloat-float-float-float-float) | 任意の範囲の値を別の範囲に変換します。 |



<!-- -------------------------------------------------- -->
## MathSpecial class
### Definition
- Namespace: TH.Utils

特殊関数に関する静的メソッドを提供します。
一部の関数は精度に問題があります。そのため、値の妥当性を慎重に検討してください。

### Methods
| Name | Summary |
| ---- | ---- |
| [Gamma(double)](/Docs~/doc_MathSpecial.md#gammadouble) | ガンマ関数を計算します。 |
| [Beta(double)](/Docs~/doc_MathSpecial.md#betadouble) | ベータ関数を計算します。 |
| [BesselJ(double, double)](/Docs~/doc_MathSpecial.md#besseljdouble-double) | 第1種ベッセル関数を計算します。 |



<!-- -------------------------------------------------- -->
## Matrix3x3 struct
### Definition
- Namespace: TH.Utils

標準3x3変換行列を定義します。

### Static Properties
| Name | Summary |
| ---- | ---- |
| [identity](/Docs~/doc_Matrix3x3.md#identity) | 単位行列を返します。 (Read Only) |
| [zero](/Docs~/doc_Matrix3x3.md#zero) | すべての要素をゼロに設定した行列を返します。 (Read Only) |

### Properties
| Name | Summary |
| ---- | ---- |
| [this[int]](/Docs~/doc_Matrix3x3.md#thisint) | index指定して要素にアクセスします。 |
| [this[int, int]](/Docs~/doc_Matrix3x3.md#thisint-int) | [行、列]を指定して要素にアクセスします。|

### Methods
| Name | Summary |
| ---- | ---- |
| [GetColumn(int)](/Docs~/doc_Matrix3x3.md#getcolumnint) | 行列の列を取得します。 |
| [GetRow(int)](/Docs~/doc_Matrix3x3.md#getrowint) | 行列の行を取得します。 |
| [Inverse()](/Docs~/doc_Matrix3x3.md#inverse) | この行列を逆行列に変換します。もとの行列を変更します。 |
| [Inverse(Matrix3x3)](/Docs~/doc_Matrix3x3.md#inversematrix3x3) | 任意の行列の逆行列を返します。もとの行列は変更されません。 |
| [Scale(Vector2)](/Docs~/doc_Matrix3x3.md#scalevector2) | スケーリング行列を作成します。返される行列はベクトルvectorによる座標の軸に沿ってスケールします。 |
| [SetColumn(int, Vector3)](/Docs~/doc_Matrix3x3.md#setcolumnint-vector3) | 指定した列の値をセットします。 |
| [SetRow(int, Vector3)](/Docs~/doc_Matrix3x3.md#setrowint-vector3) | 指定した行の値をセットします。 |
| [ToString()](/Docs~/doc_Matrix3x3.md#tostring) | 文字列に変換します。 |
| [ToString(string)](/Docs~/doc_Matrix3x3.md#tostringstring) | フォーマットを指定して文字列に変換します。 |
| [ToString(string, IFormatProvider)](/Docs~/doc_Matrix3x3.md#tostringstring-iformatprovider) | フォーマットを指定して文字列に変換します。 |
| [Translate(Vector2)](/Docs~/doc_Matrix3x3.md#translatevector2) | 変換行列を作成します。 |
| [T()](/Docs~/doc_Matrix3x3.md#t) | この行列を転置行列に変換します。もとの行列を変更します。Transpose()と同等です。 |
| [Transpose()](/Docs~/doc_Matrix3x3.md#transpose) | この行列を転置行列に変換します。もとの行列を変更します。 |
| [Transpose(Matrix3x3)](/Docs~/doc_Matrix3x3.md#transposematrix3x3) | 任意の行列の転置行列を返します。もとの行列は変更されません。 |

### Operators
| Operator | Summary |
| ---- | ---- |
| [operator *](/Docs~/doc_Matrix3x3.md#operator-) | 2つの行列を乗算します。 |
| [operator *](/Docs~/doc_Matrix3x3.md#operator-1) | 行列にVector3を乗算します。 |
| [operator ==](/Docs~/doc_Matrix3x3.md#operator-2) | 2つの行列が等しいかどうかを判定します。 |
| [operator !=](/Docs~/doc_Matrix3x3.md#operator-3) | 2つの行列が等しくないかどうかを判定します。 |



<!-- -------------------------------------------------- -->
## NumericArrayCore class
### Definition
- Namespace: TH.Utils

数値配列に関する静的メソッドを提供します。

### Methods
| Name | Summary |
| ---- | ---- |
| [Diff(this int[], int order)](/Docs~/doc_NumericArrayCore.md#diffthis-int-int-order) | 配列の各要素の差分を返します。 |
| [Diff(this float[], int order)](/Docs~/doc_NumericArrayCore.md#diffthis-float-int-order) | 配列の各要素の差分を返します。 |



<!-- -------------------------------------------------- -->
## QuaternionConverter class
### Definition
- Namespace: TH.Utils

UnityEngine.Quaternionの変換に関する静的メソッドを提供します。

### Methods
| Name | Summary |
| ---- | ---- |
| [ToEulerAngles(this Quaternion, string, RotationType, bool)](/Docs~/doc_QuaternionConverter.md#toeuleranglesthis-quaternion-string-rotationtype-bool) | Quaternionを任意の回転シーケンスでEuler角に変換します。 |
| [ToEulerAngles(this Quaternion, Sequence, RotationType, bool)](/Docs~/doc_QuaternionConverter.md#toeuleranglesthis-quaternion-sequence-rotationtype-bool) | Quaternionを任意の回転シーケンスでEuler角に変換します。 |
| [ToQuaternion(this Vector3, Sequence, bool isEulerDeg)](/Docs~/doc_QuaternionConverter.md#toquaternionthis-vector3-sequence-bool-iseulerdeg) | Euler角を任意の回転シーケンスでQuaternionに変換します。 |





# References
- https://ufcpp.net/study/csharp/sp3_expressionsample.html
- https://en.wikipedia.org/wiki/Taylor_series
- https://en.wikipedia.org/wiki/Finite_difference
- https://www1.doshisha.ac.jp/~bukka/lecture/computer/resume/chap07.pdf
- https://fluid.mech.kogakuin.ac.jp/~iida/Lectures/seminar/java/document/cfd-t02.pdf
- https://en.wikipedia.org/wiki/Combinatorics
- https://en.wikipedia.org/wiki/Binomial_coefficient
- https://en.wikipedia.org/wiki/Normal_distribution
- https://en.wikipedia.org/wiki/Stirling%27s_approximation
- Nemes Gergő, New asymptotic expansion for the Gamma function, Archiv der Mathematik, 95, 161-169, 2010. https://doi.org/10.1007/s00013-010-0146-9
- https://en.wikipedia.org/wiki/Beta_function
- https://qiita.com/aa_debdeb/items/3d02e28fb9ebfa357eaf
- https://qiita.com/edo_m18/items/5db35b60112e281f840e
- https://jp.mathworks.com/help/driving/ref/quaternion.rotmat.html
- https://jp.mathworks.com/help/driving/ref/quaternion.euler.html


# Troubleshooting


# Versions
- [CHANGELOG](/CHANGELOG.md)


# Author
- Takayoshi Hagiwara
    - Toyohashi University of Technology


# License
- MIT License