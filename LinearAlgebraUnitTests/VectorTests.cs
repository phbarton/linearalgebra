using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Math.LinearAlgebra.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void Constructor_Copy_NonNull_Value()
        {
            var copy = new Vector(new[] { 1M, 2M, 3M });
            var v = new Vector(copy);

            Assert.IsNotNull(v, "The vector is null after construction.");
            Assert.AreEqual(copy.Dimensions, v.Dimensions, "Incorrect vector dimensions");
            Assert.IsTrue(copy.ToArray().SequenceEqual(v.ToArray()), "Incorrect values and/or order for vector.");
        }

        [TestMethod]
        public void Constructor_Copy_Null_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Vector((Vector)null));

        [TestMethod]
        public void Constructor_Enumerable_Empty_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Vector(new List<decimal>()));

        [TestMethod]
        public void Constructor_Enumerable_NonNull_Value()
        {
            var values = new List<decimal> { 1M, 2M, 3M };
            var v = new Vector(values);

            Assert.IsNotNull(v, "The vector is null after construction.");
            Assert.AreEqual(new Dimension(1, 3), v.Dimensions, "Incorrect vector dimensions");
            Assert.IsTrue(values.SequenceEqual(v.ToArray()), "Incorrect values and/or order for vector.");
        }

        [TestMethod]
        public void Constructor_Enumerable_Null_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Vector((IEnumerable<decimal>)null));

        [TestMethod]
        public void Constructor_Params_Empty_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Vector(new decimal[0]));

        [TestMethod]
        public void Constructor_Params_NonNull_Value()
        {
            var values = new[] { 1M, 2M, 3M };
            var v = new Vector(values);

            Assert.IsNotNull(v, "The vector is null after construction.");
            Assert.AreEqual(new Dimension(1, 3), v.Dimensions, "Incorrect vector dimensions");
            Assert.IsTrue(values.SequenceEqual(v.ToArray()), "Incorrect values and/or order for vector.");
        }

        [TestMethod]
        public void Constructor_Params_Null_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Vector((decimal[])null));

        [TestMethod]
        public void Constructor_ZeroVector_NonZero_Length()
        {
            var v = new Vector(3);

            Assert.IsNotNull(v, "The vector is null after construction.");
            Assert.AreEqual(new Dimension(1, 3), v.Dimensions, "Incorrect vector dimensions");
            Assert.IsTrue(v.ToArray().All(i => i == decimal.Zero), "Incorrect vector values.");
        }

        [TestMethod]
        public void Constructor_ZeroVector_Zero_Length() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Vector(0));

        [TestMethod]
        public void Indexer_Getter_OK()
        {
            var values = new[] { 1M, 2M, 3M };
            var v = new Vector(values);

            Assert.IsNotNull(v, "The vector is null after construction.");
            Assert.AreEqual(new Dimension(1, 3), v.Dimensions, "Incorrect vector dimensions");

            for (var i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(values[i], v[i], $"Incorrect value for vector at position {i}.");
            }
        }

        [TestMethod]
        public void Indexer_Setter_OK()
        {
            var values = new[] { 1M, 2M, 3M };
            var v = new Vector(values);

            v[0] = 2M;
            v[1] = 4M;
            v[2] = 6M;

            Assert.IsNotNull(v, "The vector is null after construction.");
            Assert.AreEqual(new Dimension(1, 3), v.Dimensions, "Incorrect vector dimensions");

            for (var i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(2M * values[i], v[i], $"Incorrect value for vector at position {i}.");
            }
        }

        [TestMethod]
        public void ToArray_OK()
        {
            var values = new[] { 1M, 2M, 3M };
            var v = new Vector(values);
            var a = v.ToArray();

            Assert.IsTrue(values.SequenceEqual(a), "Incorrect values and/or order for vector.");
        }

        [TestMethod]
        public void Transpose_OK()
        {
            var values = new[] { 1M, 2M, 3M };
            var v = new Vector(values);
            var vT = v.Transpose();

            Assert.AreEqual(v.Dimensions.Rows, vT.Dimensions.Columns, "Failed to transpose column dimension");
            Assert.AreEqual(v.Dimensions.Columns, vT.Dimensions.Rows, "Failed to transpose row dimension");
            Assert.IsTrue(vT.ToArray().SequenceEqual(values), "Transposed values are not correct.");
        }
    }
}