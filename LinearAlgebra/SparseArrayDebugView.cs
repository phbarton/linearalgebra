using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Math.LinearAlgebra
{
    [ExcludeFromCodeCoverage]
    internal class SparseArrayDebugView
    {
        private readonly IEnumerable _array;

        /// <summary>
        /// Initializes a new instance of the <see cref="SparseArrayDebugView"/> class.
        /// </summary>
        /// <param name="array">The array.</param>
        public SparseArrayDebugView(IEnumerable array)
        {
            Guard.ThrowIfArgumentNull(array, nameof(array));
            this._array = array;
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object[] Items => this._array.Cast<object>().ToArray();
    }
}