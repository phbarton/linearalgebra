using System;
using System.Collections.Generic;
using System.Text;

namespace System.Math.LinearAlgebra
{
    public class Matrix<T> where T: IComparable<T>
    {
        private Dimension _dimension;
        private SparseArray<T>[] _rows;

        private Matrix()
        {
        }
    }
}
