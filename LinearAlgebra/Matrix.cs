using System;
using System.Collections.Generic;
using System.Text;

namespace System.Math.LinearAlgebra
{
    public class Matrix : MatrixBase<Vector[]>
    {

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
        public Matrix(Dimension dimension) : base(dimension)
        {
            this.Storage = new Vector[dimension.Rows];

            for (var i = 0; i < dimension.Rows; i++)
            {
                this.Storage[i] = new Vector(dimension.Columns);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="copy">The copy.</param>
        public Matrix(Matrix copy):base(copy.Dimensions)
        {
            this.Storage = new Vector[this.Dimensions.Rows];

            for (var i = 0; i < this.Dimensions.Rows; i++)
            {
                this.Storage[i] = new Vector(copy[i].ToArray());
            }
        }

        /// <summary>
        /// Gets the <see cref="Vector"/> with the specified row.
        /// </summary>
        /// <value>
        /// The <see cref="Vector"/>.
        /// </value>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public Vector this[int row] => this.Storage[row];
    }
}
