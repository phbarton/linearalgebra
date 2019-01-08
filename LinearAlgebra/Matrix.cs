using System;
using System.Collections.Generic;
using System.Text;

namespace System.Math.LinearAlgebra
{
    public class Matrix
    {
        private Dimension _dimension;
        private SparseArray<decimal>[] _rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        public Matrix(int rows, int columns) : this(new Dimension(rows, columns))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="tuple">The tuple.</param>
        public Matrix(Tuple<int, int> tuple) : this(new Dimension(tuple.Item1, tuple.Item2))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
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
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="copy">The copy.</param>
        public Matrix(Matrix copy)
        {
            this._dimension = (Dimension)copy._dimension.Clone();
            this._rows = new SparseArray<decimal>[this._dimension.Columns];

            for (var i = 0; i < this.Dimensions.Rows; i++)
            {
                this._rows[i] = new SparseArray<decimal>(copy._rows[i].ToArray());
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
