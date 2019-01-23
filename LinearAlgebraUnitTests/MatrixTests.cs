using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Math.LinearAlgebra.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void Constructor_Copy_Null() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix((Matrix)null));

        [TestMethod]
        public void Constructor_Copy_OK()
        {
            var m = new Matrix(new Vector(new[] { 1M, 2M }), new Vector(new[] { 3M, 4M }));
            var m2 = new Matrix(m);

            Assert.AreEqual(new Dimension(2, 2), m2.Dimensions, "Incorrect dimensions of new matrix");

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    Assert.AreEqual(m[i][j], m2[i][j], $"Invalid value in matrix at ({i},{j}).");
                }
            }
        }

        [TestMethod]
        public void Constructor_Vectors_Empty() => Assert.ThrowsException<InvalidOperationException>(() => new Matrix(new Vector[0]));

        [TestMethod]
        public void Constructor_Vectors_InvalidVectors() => Assert.ThrowsException<InvalidOperationException>(() => new Matrix(new Vector(new[] { 1M, 2M }), new Vector(new[] { 3M, 4M, 5m })));

        [TestMethod]
        public void Constructor_Vectors_Null() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix((Vector[])null));

        [TestMethod]
        public void Constructor_Vectors_OK()
        {
            var m = new Matrix(new Vector(new[] { 1M, 2M }), new Vector(new[] { 3M, 4M }));

            Assert.AreEqual(new Dimension(2, 2), m.Dimensions, "Incorrect dimensions of new matrix");

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    Assert.AreEqual(2 * i + j + 1, m[i][j], $"Invalid value in matrix at ({i},{j}).");
                }
            }
        }

        [TestMethod]
        public void Constructor_ZeroMatrix_Dimension_InvalidColumn() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(new Dimension(2, 0)));

        [TestMethod]
        public void Constructor_ZeroMatrix_Dimension_InvalidRow() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(new Dimension(0, 2)));

        [TestMethod]
        public void Constructor_ZeroMatrix_Dimension_OK()
        {
            var m = new Matrix(new Dimension(2, 2));

            Assert.AreEqual(new Dimension(2, 2), m.Dimensions, "Incorrect dimensions of new matrix");

            var values = m.Storage.SelectMany(v => v.ToArray());
            Assert.AreEqual(4, values.Count(), "Incorrect number of values in matrix");
            Assert.IsTrue(values.All(i => i == 0), "Incorrect value in matrix");
        }

        [TestMethod]
        public void Constructor_ZeroMatrix_IndividualComponents_InvalidColumn() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(2, 0));

        [TestMethod]
        public void Constructor_ZeroMatrix_IndividualComponents_InvalidRow() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(0, 2));

        [TestMethod]
        public void Constructor_ZeroMatrix_IndividualComponents_OK()
        {
            var m = new Matrix(2, 2);

            Assert.AreEqual(new Dimension(2, 2), m.Dimensions, "Incorrect dimensions of new matrix");

            var values = m.Storage.SelectMany(v => v.ToArray());
            Assert.AreEqual(4, values.Count(), "Incorrect number of values in matrix");
            Assert.IsTrue(values.All(i => i == 0), "Incorrect value in matrix");
        }

        [TestMethod]
        public void Constructor_ZeroMatrix_Tuple_InvalidColumn() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(new Tuple<int, int>(2, 0)));

        [TestMethod]
        public void Constructor_ZeroMatrix_Tuple_InvalidRow() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Matrix(new Tuple<int, int>(0, 2)));

        [TestMethod]
        public void Constructor_ZeroMatrix_Tuple_OK()
        {
            var m = new Matrix(new Tuple<int, int>(2, 2));

            Assert.AreEqual(new Dimension(2, 2), m.Dimensions, "Incorrect dimensions of new matrix");

            var values = m.Storage.SelectMany(v => v.ToArray());
            Assert.AreEqual(4, values.Count(), "Incorrect number of values in matrix");
            Assert.IsTrue(values.All(i => i == 0), "Incorrect value in matrix");
        }

        [TestMethod]
        public void Identity_OK()
        {
            var m = new Matrix(new Vector(new[] { 1M, 2M, 2M }), new Vector(new[] { 3M, 4M, 4M }));
            var id = m.Identity;

            Assert.IsNotNull(id, "Null matrix returned for matrix identity.");
            Assert.AreEqual(new Dimension(m.Dimensions.Columns, m.Dimensions.Columns), id.Dimensions, "Incorrect dimensions of matrix identity");

            for (var i = 0; i < id.Dimensions.Rows; i++)
            {
                for (var j = 0; j < id.Dimensions.Columns; j++)
                {
                    var value = id[i][j];

                    if (i == j)
                    {
                        Assert.AreEqual(decimal.One, value, $"Incorrect value of {value} at ({i},{j}) in matrix identiity - expected 1");
                    }
                    else
                    {
                        Assert.AreEqual(decimal.Zero, value, $"Incorrect value of {value} at ({i},{j}) in matrix identiity - expected 0");
                    }
                }
            }
        }

        [TestMethod]
        public void IsSquare_False()
        {
            var m = new Matrix(new Vector(new[] { 1M, 2M, 2M }), new Vector(new[] { 3M, 4M, 4M }));

            Assert.IsFalse(m.IsSquare, "Incorrect value for non-square matrix.");
        }

        [TestMethod]
        public void IsSquare_True()
        {
            var m = new Matrix(new Vector(new[] { 1M, 2M }), new Vector(new[] { 3M, 4M }));

            Assert.IsTrue(m.IsSquare, "Incorrect value for square matrix.");
        }

        [TestMethod]
        public void Iterator_OK()
        {
            var m = new Matrix(new Vector(new[] { 1M, 2M }), new Vector(new[] { 3M, 4M }));

            Assert.AreEqual(new Dimension(2, 2), m.Dimensions, "Incorrect dimensions of new matrix");

            for (var i = 0; i < 2; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    Assert.AreEqual(2 * i + j + 1, m[i][j], $"Invalid value in matrix at ({i},{j}).");
                }
            }
        }

        [TestMethod]
        public void Transpose_OK()
        {
            var m = new Matrix(new Vector(new[] { 1M, 2M, 2M }), new Vector(new[] { 3M, 4M, 4M }));
            var mT = m.Transpose();

            Assert.AreEqual(m.Dimensions.Rows, mT.Dimensions.Columns, "Failed to transpose column dimension");
            Assert.AreEqual(m.Dimensions.Columns, mT.Dimensions.Rows, "Failed to transpose row dimension");
            Assert.IsTrue(mT[0].ToArray().SequenceEqual(new[] { 1M, 3M }), "Failed to transpose row 0 correctly");
            Assert.IsTrue(mT[1].ToArray().SequenceEqual(new[] { 2M, 4M }), "Failed to transpose row 1 correctly");
            Assert.IsTrue(mT[2].ToArray().SequenceEqual(new[] { 2M, 4M }), "Failed to transpose row 2 correctly");
        }
    }
}