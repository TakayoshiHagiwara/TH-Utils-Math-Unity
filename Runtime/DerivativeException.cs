// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/10
// Summary: Exception when the NodeType of the expression tree is invalid.
// -----------------------------------------------------------------------

using System;

namespace TH.Utils
{
    public class ExpressionNodeTypeException : Exception
    {
        public ExpressionNodeTypeException() : base() { }
        public ExpressionNodeTypeException(string message) : base(message) { }
    }
}
