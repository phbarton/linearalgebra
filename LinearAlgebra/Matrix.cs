using System.Linq;

namespace System.Math.LinearAlgebra
{
    public class Matrix : MatrixBase<Matrix, Vector[]>
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
        /// <param name="vectors">The vectors.</param>
        public Matrix(params Vector[] vectors) :
            base(vectors?.Length ?? 0, vectors?.First().Dimensions.Columns ?? 0)
        {
            var dim = vectors.First().Dimensions.Columns;

            if (vectors.Any(v => v.Dimensions.Columns != dim))
            {
                throw new InvalidOperationException("All vectors must have the same dimension.");
            }

            this.Storage = new Vector[vectors.Length];

            for (var i = 0; i < this.Dimensions.Rows; i++)
            {
                this.Storage[i] = new Vector(vectors[i].ToArray());
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="copy">The copy.</param>
        public Matrix(Matrix copy) :
            base(copy?.Dimensions ?? new Dimension(0, 0))
        {
            this.Storage = new Vector[this.Dimensions.Rows];

            for (var i = 0; i < this.Dimensions.Rows; i++)
            {
                this.Storage[i] = new Vector(copy[i].ToArray());
            }
        }

        /// <summary>
        /// Gets a value indicating whether this matrix is square (i.e. the number of rows matches the number of columns).
        /// </summary>
        /// <value>
        ///   <c>true</c> if this matrix is square; otherwise, <c>false</c>.
        /// </value>
        public bool IsSquare => this.Dimensions.Rows == this.Dimensions.Columns;

        /// <summary>
        /// Gets the <see cref="Vector"/> with the specified row.
        /// </summary>
        /// <value>
        /// The <see cref="Vector"/>.
        /// </value>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public Vector this[int row] => this.Storage[row];

        /// <summary>
        /// Transposes this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Matrix Transpose()
        {
            var m = new Matrix(this.Dimensions.Columns, this.Dimensions.Rows);

            for (var i = 0; i < this.Dimensions.Rows; i++)
            {
                for (var j = 0; j < this.Dimensions.Columns; j++)
                {
                    m[j][i] = this[i][j];
                }
            }

            return m;
        }
    }
}