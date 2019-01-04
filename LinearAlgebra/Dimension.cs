using System.Diagnostics;

namespace System.Math.LinearAlgebra
{
    [Serializable]
    [DebuggerDisplay("Rows = {Rows}, Columns = {Columns}")]
    public struct Dimension : ICloneable,
        IEquatable<Dimension>,
        IEquatable<Tuple<int, int>>
    {
        private static readonly Dimension _empty = new Dimension();
        private readonly int _columns;
        private readonly int _rows;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimension"/> struct.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        internal Dimension(int rows, int columns)
        {
            Guard.ThrowIfLessThan(rows, 1, nameof(rows));
            Guard.ThrowIfLessThan(columns, 1, nameof(columns));

            this._columns = columns;
            this._rows = rows;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimension"/> struct.
        /// </summary>
        /// <param name="dimensions">The dimensions.</param>
        internal Dimension(Dimension dimensions)
        {
            this._rows = dimensions._rows;
            this._columns = dimensions._columns;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimension"/> struct.
        /// </summary>
        /// <param name="dimensions">The dimesions.</param>
        internal Dimension(Tuple<int, int> dimensions)
        {
            Guard.ThrowIfArgumentNull(dimensions, nameof(dimensions));
            Guard.ThrowIfLessThan(dimensions.Item1, 1, nameof(dimensions));
            Guard.ThrowIfLessThan(dimensions.Item2, 1, nameof(dimensions));

            this._rows = dimensions.Item1;
            this._columns = dimensions.Item2;
        }

        /// <summary>
        /// Gets the empty.
        /// </summary>
        /// <value>
        /// The empty.
        /// </value>
        public static Dimension Empty => _empty;

        /// <summary>
        /// Gets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        public int Columns => this._columns;

        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty => this._rows == 0 && this._columns == 0;

        /// <summary>
        /// Gets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public int Rows => this._rows;

        /// <summary>
        /// Performs an implicit conversion from <see cref="Tuple{System.Int32, System.Int32}"/> to <see cref="Dimension"/>.
        /// </summary>
        /// <param name="dim">The dim.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Dimension(Tuple<int, int> dim) => dim == null ? Dimension.Empty : new Dimension(dim.Item1, dim.Item2);

        /// <summary>
        /// Performs an implicit conversion from <see cref="Dimension"/> to <see cref="Tuple{System.Int32, System.Int32}"/>.
        /// </summary>
        /// <param name="dim">The dim.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        ///
        public static implicit operator Tuple<int, int>(Dimension dim) => new Tuple<int, int>(dim.Rows, dim.Columns);

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator -(Dimension left, int right)
        {
            var rows = left.Rows - right;
            var cols = left.Columns - right;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator -(Dimension left, Dimension right)
        {
            var rows = left.Rows - right.Rows;
            var cols = left.Columns - right.Columns;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator -(Dimension left, Tuple<int, int> right)
        {
            var rows = left.Rows - right.Item1;
            var cols = left.Columns - right.Item2;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Dimension left, Dimension right) => !(left == right);

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(Dimension left, Tuple<int, int> right) => !(left == right);

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator *(Dimension left, int right)
        {
            var rows = left.Rows * right;
            var cols = left.Columns * right;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator *(Dimension left, Dimension right)
        {
            var rows = left.Rows * right.Rows;
            var cols = left.Columns * right.Columns;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator *.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator *(Dimension left, Tuple<int, int> right)
        {
            var rows = left.Rows * right.Item1;
            var cols = left.Columns * right.Item2;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator /(Dimension left, int right)
        {
            var rows = left.Rows / right;
            var cols = left.Columns / right;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator /(Dimension left, Dimension right)
        {
            var rows = left.Rows / right.Rows;
            var cols = left.Columns / right.Columns;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator /.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator /(Dimension left, Tuple<int, int> right)
        {
            var rows = left.Rows / right.Item1;
            var cols = left.Columns / right.Item2;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator +(Dimension left, int right)
        {
            var rows = left.Rows + right;
            var cols = left.Columns + right;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator +(Dimension left, Dimension right)
        {
            var rows = left.Rows + right.Rows;
            var cols = left.Columns + right.Columns;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator +.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Dimension operator +(Dimension left, Tuple<int, int> right)
        {
            var rows = left.Rows + right.Item1;
            var cols = left.Columns + right.Item2;

            ThrowIfFesultLessThanOne(rows);
            ThrowIfFesultLessThanOne(cols);

            return new Dimension(rows, cols);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Dimension left, Dimension right) => left.Equals(right);

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(Dimension left, Tuple<int, int> right) => left.Equals(right);

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone() => new Dimension(this);

        /// <summary>
        /// Dots the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public Dimension Dot(Dimension other)
        {
            ThrowIfCannotDotProduct(this, other);

            return new Dimension(this.Rows, other.Columns);
        }

        /// <summary>
        /// Dots the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public Dimension Dot(Tuple<int, int> other)
        {
            ThrowIfCannotDotProduct(this, other);
            Guard.ThrowIfLessThan(other.Item2, 1, nameof(other));

            return new Dimension(this.Rows, other.Item2);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Dimension other) => this.Rows == other.Rows && this.Columns == other.Columns;

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is Dimension)
            {
                return this.Equals((Dimension)obj);
            }

            if (obj is Tuple<int, int>)
            {
                return this.Equals((Tuple<int, int>)obj);
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Tuple<int, int> other) => other != null ? this._rows == other.Item1 && this._columns == other.Item2 : false;

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode() => (((long)this._rows) << 32 | this._columns).GetHashCode();

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString() => $"({this._rows}, {this._columns})";

        /// <summary>
        /// Transposes this instance.
        /// </summary>
        /// <returns></returns>
        public Dimension Transpose() => new Dimension(this.Columns, this.Rows);

        /// <summary>
        /// Throws if cannot dot product.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        /// <param name="other">The other.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private static void ThrowIfCannotDotProduct(Dimension dimension, Dimension other)
        {
            if (dimension.Columns != other.Rows)
            {
                throw new InvalidOperationException("The dimensions do not allow for a dot product operation.");
            }
        }

        /// <summary>
        /// Throws if cannot dot product.
        /// </summary>
        /// <param name="dimension">The dimension.</param>
        /// <param name="other">The other.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private static void ThrowIfCannotDotProduct(Dimension dimension, Tuple<int, int> other)
        {
            if (dimension.Columns != other.Item1)
            {
                throw new InvalidOperationException("The dimensions do not allow for a dot product operation.");
            }
        }

        /// <summary>
        /// Throws if fesult less than one.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <exception cref="InvalidOperationException">The resultant value would be less than one.</exception>
        private static void ThrowIfFesultLessThanOne(int result)
        {
            if (result < 1)
            {
                throw new InvalidOperationException("The resultant value would be less than one.");
            }
        }
    }
}