using System;
using System.Collections.Generic;
using System.Text;

namespace System.Math.LinearAlgebra
{
    public class Matrix
    {
        private Dimension _dimension;
        private SparseArray<decimal>[] _rows;

        public Matrix(int rows, int columns) : this(new Dimension(rows, columns))
        {
        }

        public Matrix(Tuple<int, int> tuple) : this(new Dimension(tuple.Item1, tuple.Item2))
        {
        }

        public Matrix(Dimension dimension)
        {
            this._dimension = (Dimension)dimension.Clone();
            this._rows = new SparseArray<decimal>[this._dimension.Columns];

            for (var i = 0; i < this._dimension.Rows; i++)
            {
                this._rows[i] = new SparseArray<decimal>(this._dimension.Columns);
            }
        }

        /// <summary>
        /// Gets the dimensions.
        /// </summary>
        /// <value>
        /// The dimensions.
        /// </value>
        public Dimension Dimensions => this._dimension;
    }
}
