using System;
using System.Collections.Generic;
using System.Text;

namespace System.Math.LinearAlgebra
{
    public abstract class MatrixBase<T>
    {
        private Dimension _dimension;
        private T _storage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixBase{T}"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        protected MatrixBase(int rows, int columns)
        {
            this._dimension = new Dimension(rows, columns);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixBase{T}"/> class.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        protected MatrixBase(Dimension dimension)
        {
            this._dimension = (Dimension)dimension.Clone();
        }

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <value>
        /// The dimension.
        /// </value>
        public Dimension Dimensions => this._dimension;

        /// <summary>
        /// Gets or sets the storage.
        /// </summary>
        /// <value>
        /// The storage.
        /// </value>
        protected internal T Storage
        {
            get => this._storage;
            set => this._storage = value;
        }
    }
}
