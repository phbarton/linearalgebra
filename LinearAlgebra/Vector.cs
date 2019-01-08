using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Math.LinearAlgebra
{
    /// <summary>
    /// Represents a vector as an 1 x N matrix of decimal values.
    /// </summary>
    /// <seealso cref="System.Math.LinearAlgebra.MatrixBase{System.Math.LinearAlgebra.SparseArray{System.Decimal}}" />
    public class Vector : MatrixBase<SparseArray<decimal>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public Vector(int length) : base(1, length)
        {
            this.Storage = new SparseArray<decimal>(length);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public Vector(IEnumerable<decimal> values) : base(1, values.Count())
        {
            this.Storage = new SparseArray<decimal>(values);
        }

        /// <summary>
        /// Gets or sets the <see cref="System.Decimal"/> with the specified row.
        /// </summary>
        /// <value>
        /// The <see cref="System.Decimal"/>.
        /// </value>
        /// <param name="row">The row.</param>
        /// <returns></returns>
        public decimal this[int row] {
            get => this.Storage[row];
            set => this.Storage[row] = value;
        }

        /// <summary>
        /// Converts the vector to an array.
        /// </summary>
        /// <returns>An array of <see cref="System.Decimal"/>.</returns>
        public decimal[] ToArray() => this.Storage.ToArray();
    }
}
