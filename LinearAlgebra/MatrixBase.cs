using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Math.LinearAlgebra
{
    public abstract class MatrixBase<TKind> : IEnumerable<decimal> where TKind : MatrixBase<TKind>
    {
        private Dimension _dimension;
        protected SparseArray<decimal> _storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixBase{T}"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        protected MatrixBase(int rows, int columns)
        {
            this._dimension = new Dimension(rows, columns);
            this._storage = new SparseArray<decimal>(this._dimension.Rows * this._dimension.Columns);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixBase{T}"/> class.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        protected MatrixBase(Dimension dimension)
        {
            this._dimension = (Dimension)dimension.Clone();
            this._storage = new SparseArray<decimal>(this._dimension.Rows * this._dimension.Columns);
        }

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <value>
        /// The dimension.
        /// </value>
        public Dimension Dimensions => this._dimension;

        /// <summary>
        /// Gets the identity matrix.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        public virtual Matrix Identity
        {
            get
            {
                var m = new Matrix(this.Dimensions.Columns, this.Dimensions.Columns);

                for (var i = 0; i < m.Dimensions.Columns; i++)
                {
                    m[i, i] = decimal.One;
                }

                return m;
            }
        }

        /// <summary>
        /// Transposes this instance.
        /// </summary>
        /// <returns></returns>
        public abstract TKind Transpose();

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<decimal> GetEnumerator() => ((IEnumerable<decimal>)this._storage).GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this._storage).GetEnumerator();
    }
}
