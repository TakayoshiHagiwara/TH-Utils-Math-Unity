# TH-Utils-Math-Unity/Exception<!-- omit in toc -->
<img src="https://img.shields.io/badge/Unity-2021 or Later-blue?&logo=Unity"> <img src="https://img.shields.io/badge/License-MIT-green">


# Table Of Contents <!-- omit in toc -->
<details>
<summary>Details</summary>

- [Definition](#definition)
- [ArrayLengthMismatchException](#arraylengthmismatchexception)
    - [Constructors](#constructors)
- [ListLengthMismatchException](#listlengthmismatchexception)
    - [Constructors](#constructors-1)
- [ExpressionNodeTypeException](#expressionnodetypeexception)
    - [Constructors](#constructors-2)
</details>


# Definition
Namespace: TH.Utils

例外を提供します。

<!-- -------------------------------------------------- -->
# ArrayLengthMismatchException
2つの配列の長さが一致していない場合のエラーを表します。

継承 Object -> Exception -> ArrayLengthMismatchException

### Constructors
| Name | Summary |
| ---- | ---- |
| ArrayLengthMismatchException() | ArrayLengthMismatchExceptionクラスの新しいインスタンスの Message プロパティを初期化し、その値としてエラーを説明するシステム提供のメッセージを指定します。 |
| ArrayLengthMismatchException(string message) | 指定したエラー メッセージを使用して、ArrayLengthMismatchExceptionクラスの新しいインスタンスを初期化します。 |


<!-- -------------------------------------------------- -->
# ListLengthMismatchException
2つのListの長さが一致していない場合のエラーを表します。

継承 Object -> Exception -> ListLengthMismatchException

### Constructors
| Name | Summary |
| ---- | ---- |
| ListLengthMismatchException() | ListLengthMismatchExceptionクラスの新しいインスタンスの Message プロパティを初期化し、その値としてエラーを説明するシステム提供のメッセージを指定します。 |
| ListLengthMismatchException(string message) | 指定したエラー メッセージを使用して、ListLengthMismatchExceptionクラスの新しいインスタンスを初期化します。 |


<!-- -------------------------------------------------- -->
# ExpressionNodeTypeException
式木のNodeTypeが不正な場合のエラーを表します。

継承 Object -> Exception -> ExpressionNodeTypeException

### Constructors
| Name | Summary |
| ---- | ---- |
| ExpressionNodeTypeException() | ExpressionNodeTypeExceptionクラスの新しいインスタンスの Message プロパティを初期化し、その値としてエラーを説明するシステム提供のメッセージを指定します。 |
| ExpressionNodeTypeException(string message) | 指定したエラー メッセージを使用して、ExpressionNodeTypeExceptionクラスの新しいインスタンスを初期化します。 |
