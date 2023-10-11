// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/10
// Summary: Exception when two list (or array) length does not match.
// -----------------------------------------------------------------------

using System;

namespace TH.Utils
{
    public class ArrayLengthMismatchException : Exception
    {
        public ArrayLengthMismatchException() : base() { }
        public ArrayLengthMismatchException(string message) : base(message) { }
    }

    public class ListLengthMismatchException : Exception
    {
        public ListLengthMismatchException() : base() { }
        public ListLengthMismatchException(string message) : base(message) { }
    }
}
