using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace System.Math.LinearAlgebra.UnitTests
{
    [ExcludeFromCodeCoverage]
    internal class MockComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y) => Comparer<T>.Default.Compare(x, y);
    }
}
