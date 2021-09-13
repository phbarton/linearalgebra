using System.Linq;

namespace System.Math.LinearAlgebra
{
    public class Matrix : MatrixBase<Matrix>
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

            for (var i = 0; i < vectors.Length; i++)
            {
                for (var j = 0; j < dim; j++)
                {
                    this._storage[i * dim + j] = vectors[i][j];
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix"/> class.
        /// </summary>
        /// <param name="copy">The copy.</param>
        public Matrix(Matrix copy) :
            base(copy?.Dimensions ?? new Dimension(0, 0))
        {
            foreach(var k in copy._storage.Keys)
            {
                this._storage[k] = copy._storage[k];
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
        public Vector this[int row]
        {
            get
            {
                var values = new decimal[this.Dimensions.Columns];

                for (var j = 0; j < values.Length; j++)
                {
                    values[j] = this._storage[row * this.Dimensions.Columns + j];
                }

                return new Vector(values);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Decimal"/> with the specified row.
        /// </summary>
        /// <value>
        /// The <see cref="System.Decimal"/>.
        /// </value>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <returns></returns>
        public decimal this[int row, int column]
        {
            get => this._storage[row * this.Dimensions.Columns + column];
            set => this._storage[row * this.Dimensions.Columns + column] = value;
        }

        /// <summary>
        /// Transposes this instance.
        /// </summary>
        /// <returns></returns>
        public override Matrix Transpose()
        {
            var m = new Matrix(this.Dimensions.Columns, this.Dimensions.Rows);

            for (var i = 0; i < this.Dimensions.Rows; i++)
            {
                for (var j = 0; j < this.Dimensions.Columns; j++)
                {
                    m[j, i] = this[i, j];
                }
            }

            return m;
        }
    }
}