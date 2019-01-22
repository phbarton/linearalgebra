using System.Collections.Generic;
using System.Linq;

namespace System.Math.LinearAlgebra
{
    /// <summary>
    /// Represents a vector as an 1 x N matrix of decimal values.
    /// </summary>
    /// <seealso cref="System.Math.LinearAlgebra.MatrixBase{System.Math.LinearAlgebra.SparseArray{System.Decimal}}" />
    public class Vector : MatrixBase<Vector, SparseArray<decimal>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public Vector(int length) : base(1, length) => this.Storage = new SparseArray<decimal>(length);

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public Vector(IEnumerable<decimal> values) :
            base(1, values?.Count() ?? 0) => this.Storage = new SparseArray<decimal>(values);

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public Vector(params decimal[] values) : base(1, values?.Length ?? 0) => this.Storage = new SparseArray<decimal>(values);

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="copy">The copy.</param>
        public Vector(Vector copy) : base(copy?.Dimensions ?? new Dimension(0, 0)) => this.Storage = new SparseArray<decimal>(copy.ToArray());

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="values">The values.</param>
        protected Vector(int rows, int columns, params decimal[] values) : base(rows, columns) => this.Storage = new SparseArray<decimal>(values);

        /// <summary>
        /// Gets or sets the <see cref="System.Decimal"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="System.Decimal"/> value at <paramref name="index"/>.
        /// </value>
        /// <param name="index">The index into the vector.</param>
        /// <returns>The <see cref="System.Decimal"/> value at <paramref name="index"/></returns>
        public decimal this[int index]
        {
            get => this.Storage[index];
            set => this.Storage[index] = value;
        }

        /// <summary>
        /// Converts the vector to an array.
        /// </summary>
        /// <returns>An array of <see cref="System.Decimal"/>.</returns>
        public decimal[] ToArray() => this.Storage.ToArray();

        /// <summary>
        /// Transposes this instance.
        /// </summary>
        /// <returns></returns>
        public override Vector Transpose() => new Vector(this.Dimensions.Columns, this.Dimensions.Rows, this.ToArray());
    }
}