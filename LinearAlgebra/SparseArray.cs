using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Math.LinearAlgebra
{
    /// <summary>
    /// Represents a sparse array which only contains non-default values in order to conserve space.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the array.</typeparam>
    /// <seealso cref="System.ICloneable" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    /// <seealso cref="System.Collections.Generic.IList{T}" />
    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof(SparseArrayDebugView))]
    [Serializable]
    internal class SparseArray<T> : ICloneable, IEnumerable<T>, IList<T> where T : IComparable<T>
    {
        /// <summary>
        /// Enumerator for the <seealso cref="SparseArray{T}"/> class.
        /// </summary>
        /// <seealso cref="System.ICloneable" />
        /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
        /// <seealso cref="System.Collections.Generic.IList{T}" />
        private class SparseArrayEnumerator : IEnumerator<T>
        {
            private readonly SparseArray<T> _values;
            private int _index;
            private readonly int _version;

            /// <summary>
            /// Initializes a new instance of the <see cref="SparseArrayEnumerator"/> class.
            /// </summary>
            /// <param name="values">The values.</param>
            public SparseArrayEnumerator(SparseArray<T> values)
            {
                Guard.ThrowIfArgumentNull(values, nameof(values));

                this._values = values;
                this._index = -1;
                this._version = values._version;
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            /// <exception cref="InvalidOperationException"></exception>
            public T Current
            {
                get
                {
                    if (this._index < 0)
                    {
                        throw new InvalidOperationException("The enumerator has not been started.");
                    }

                    if (this._index > this._values.Count - 1)
                    {
                        throw new InvalidOperationException("The enumerator has ended.");
                    }

                    return this._values[this._index];
                }
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            object IEnumerator.Current => this.Current;

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (this._version != this._values._version)
                {
                    throw new InvalidOperationException("The collection has been modified.");
                }

                this._index++;
                return this._index < this._values.Capacity;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                if (this._version != this._values._version)
                {
                    throw new InvalidOperationException("The collection has been modified.");
                }

                this._index = -1;
            }
        }

        [NonSerialized]
        private int _version = 0;
        private readonly int _capacity;
        private readonly IComparer<T> _comparer;
        private readonly Dictionary<int, T> _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="SparseArray{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public SparseArray(int capacity, IComparer<T> comparer = null)
        {
            Guard.ThrowIfLessThan(capacity, 1, nameof(capacity));

            this._capacity = capacity;
            this._values = new Dictionary<int, T>(capacity);
            this._comparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SparseArray{T}"/> class.
        /// </summary>
        /// <param name="values">The values.</param>
        public SparseArray(IEnumerable<T> values, IComparer<T> comparer = null)
        {
            Guard.ThrowIfArgumentNull(values, nameof(values));

            var count = values.Count();

            Guard.ThrowIfLessThan(count, 1, nameof(values));

            this._capacity = count;
            this._values = new Dictionary<int, T>(count);
            this._comparer = comparer ?? Comparer<T>.Default;

            for (var i = 0; i < count; i++)
            {
                this[i] = values.ElementAt(i);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SparseArray{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="keyValuePairs">The key value pairs.</param>
        internal SparseArray(int capacity, IDictionary<int, T> keyValuePairs, IComparer<T> comparer = null)
        {
            Guard.ThrowIfLessThan(capacity, 1, nameof(capacity));
            Guard.ThrowIfArgumentNull(keyValuePairs, nameof(keyValuePairs));
            Guard.ThrowIfGreaterThan(keyValuePairs.Count, capacity, nameof(capacity));

            this._capacity = capacity;
            this._values = new Dictionary<int, T>(keyValuePairs);
            this._comparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>
        /// Gets the capacity.
        /// </summary>
        /// <value>
        /// The capacity.
        /// </value>
        public int Capacity => this._capacity;

        /// <summary>
        /// Gets the comparer.
        /// </summary>
        /// <value>
        /// The comparer.
        /// </value>
        public IComparer<T> Comparer => this._comparer;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        public int Count => this.Capacity;

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <value>
        /// The keys.
        /// </value>
        internal IEnumerable<int> Keys => this._values.Keys;

        /// <summary>
        /// Gets the sparsity.
        /// </summary>
        /// <value>
        /// The sparsity.
        /// </value>
        internal int Sparsity => this._values.Count;

        /// <summary>
        /// Gets the sparsity percent.
        /// </summary>
        /// <value>
        /// The sparsity percent.
        /// </value>
        internal decimal SparsityPercent => this._values.Count / (decimal)this.Capacity;

        /// <summary>
        /// Gets or sets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="T"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns>An instance of <typeparamref name="T"/> at the given index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if <paramref name="index"/>is less than zero or greater than <see cref="Capacity"/>.</exception>
        public T this[int index]
        {
            get
            {
                Guard.ThrowIfIndexLessThanZero(index);
                Guard.ThrowIfIndexGreaterThan(index, this.Capacity, true);

                return this._values.TryGetValue(index, out var value) ? value : default(T);
            }
            set
            {
                Guard.ThrowIfIndexLessThanZero(index);
                Guard.ThrowIfIndexGreaterThan(index, this.Capacity, true);

                if (this.Comparer.Compare(value, default(T)) == 0)
                {
                    this._values.Remove(index);
                }
                else
                {
                    this._values[index] = value;
                }

                this._version++;
            }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="NotSupportedException">This method is not supported</exception>
        public void Add(T item) => throw new NotSupportedException();

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        public void Clear()
        {
            this._values.Clear();
            this._version++;
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone() => new SparseArray<T>(this.Capacity, this._values, this._comparer);

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if <paramref name="item">item</paramref> is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        public bool Contains(T item) => this._values.ContainsValue(item);

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Guard.ThrowIfArgumentNull(array, nameof(array));
            Guard.ThrowIfLessThanZero(arrayIndex, nameof(arrayIndex));
            Guard.ThrowIfGreaterThan(arrayIndex, array.Length, nameof(arrayIndex), true);

            if (arrayIndex + this.Capacity > array.Length)
            {
                throw new InvalidOperationException("Cannot copy to target array - target array is too small given the array index.");
            }

            for (var i = 0; i < this.Capacity; i++)
            {
                array[arrayIndex + i] = this[i];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator() => new SparseArrayEnumerator(this);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"></see>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
        /// <returns>
        /// The index of <paramref name="item">item</paramref> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(T item)
        {
            if (this._values.ContainsValue(item))
            {
                var kvp = this._values.First(k => this.Comparer.Compare(k.Value, item) == 0);

                return kvp.Key;
            }

            return -1;
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"></see> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"></see>.</param>
        public void Insert(int index, T item)
        {
            this[index] = item;
            this._version++;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if <paramref name="item">item</paramref> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if <paramref name="item">item</paramref> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        public bool Remove(T item)
        {
            if (this._values.ContainsValue(item))
            {
                var kvp = this._values.First(k => this.Comparer.Compare(k.Value, item) == 0);

                this._version++;
                return this._values.Remove(kvp.Key);
            }

            return false;
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"></see> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            this[index] = default(T);
            this._version++;
        }

        /// <summary>
        /// Creates a fixed array from the sparse array.
        /// </summary>
        /// <returns>An array of instances of <typeparamref name="T"/>.</returns>
        public T[] ToArray()
        {
            var array = new T[this.Capacity];

            this.CopyTo(array, 0);
            return array;
        }
    }
}