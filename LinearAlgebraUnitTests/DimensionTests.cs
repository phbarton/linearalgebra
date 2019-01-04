using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Math.LinearAlgebra.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DimensionTests
    {
        [TestMethod]
        public void AdditionOperator_Dimension_OK()
        {
            var a = new Dimension(1, 2);
            var d = new Dimension(2, 3) + a;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(3, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(5, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void AdditionOperator_Integer_ColumnResultTooSmall()
        {
            var d = new Dimension(2, 1);

            Assert.ThrowsException<InvalidOperationException>(() => d + -1);
        }

        [TestMethod]
        public void AdditionOperator_Integer_OK()
        {
            var a = new Dimension(2, 3);
            var d = a + 1;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(3, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(4, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void AdditionOperator_Integer_RowResultTooSmall()
        {
            var d = new Dimension(1, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d + -1);
        }

        [TestMethod]
        public void AdditionOperator_Tuple_ColumnResultTooSmall()
        {
            var a = new Tuple<int, int>(1, -2);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d + a);
        }

        [TestMethod]
        public void AdditionOperator_Tuple_OK()
        {
            var a = new Tuple<int, int>(1, 2);
            var d = new Dimension(2, 3) + a;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(3, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(5, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void AdditionOperator_Tuple_RowResultTooSmall()
        {
            var a = new Tuple<int, int>(-2, 1);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d + a);
        }

        [TestMethod]
        public void Clone_OK()
        {
            var rows = 1;
            var cols = 2;
            var d = new Dimension(rows, cols);
            var clone = (Dimension)d.Clone();

            Assert.AreEqual(d.Rows, clone.Rows, "Incorrect row count on cloned dimension.");
            Assert.AreEqual(d.Columns, clone.Columns, "Incorrect column count on cloned dimension.");
        }

        [TestMethod]
        public void Constructor_Dimension_OK()
        {
            var a = new Dimension(1, 2);
            var d = new Dimension(a);

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(2, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void Constructor_RowColumn_BadColumn_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Dimension(1, 0));

        [TestMethod]
        public void Constructor_RowColumn_BadRow_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Dimension(0, 1));

        [TestMethod]
        public void Constructor_RowColumn_OK()
        {
            var d = new Dimension(1, 2);

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(2, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void Constructor_Tuple_BadColumn_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Dimension(new Tuple<int, int>(0, 1)));

        [TestMethod]
        public void Constructor_Tuple_BadRow_Value() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Dimension(new Tuple<int, int>(1, 0)));

        [TestMethod]
        public void Constructor_Tuple_Null() => Assert.ThrowsException<ArgumentNullException>(() => new Dimension((Tuple<int, int>)null));

        [TestMethod]
        public void Constructor_Tuple_OK()
        {
            var d = new Dimension(new Tuple<int, int>(1, 2));

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(2, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void DivisionOperator_Dimension_OK()
        {
            var a = new Dimension(1, 2);
            var d = new Dimension(2, 3) / a;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(2, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(1, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void DivisionOperator_Integer_OK()
        {
            var a = new Dimension(2, 3);
            var d = a / 2;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(1, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void DivisionOperator_Integer_ResultTooSmall()
        {
            var d = new Dimension(2, 1);

            Assert.ThrowsException<InvalidOperationException>(() => d / -1);
        }

        [TestMethod]
        public void DivisionOperator_Tuple_ColumnResultTooSmall()
        {
            var a = new Tuple<int, int>(1, -2);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d / a);
        }

        [TestMethod]
        public void DivisionOperator_Tuple_ColumnResultTooSmall_NonNegative()
        {
            var a = new Tuple<int, int>(1, 3);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d / a);
        }

        [TestMethod]
        public void DivisionOperator_Tuple_OK()
        {
            var a = new Tuple<int, int>(1, 2);
            var d = new Dimension(2, 3) / a;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(2, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(1, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void DivisionOperator_Tuple_RowResultTooSmall()
        {
            var a = new Tuple<int, int>(-2, 1);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d / a);
        }

        [TestMethod]
        public void DotProduct_Dimension_InvalidInner()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(3, 4);

            Assert.ThrowsException<InvalidOperationException>(() => a.Dot(b));
        }

        [TestMethod]
        public void DotProduct_Dimension_OK()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(2, 3);
            var d = a.Dot(b);

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(3, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void DotProduct_Tuple_InvalidInner()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(3, 4);

            Assert.ThrowsException<InvalidOperationException>(() => a.Dot(b));
        }

        [TestMethod]
        public void DotProduct_Tuple_InvalidOuter()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(2, 0);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => a.Dot(b));
        }

        [TestMethod]
        public void DotProduct_Tuple_OK()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(2, 3);
            var d = a.Dot(b);

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(3, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void Empty_OK()
        {
            var e = Dimension.Empty;

            Assert.IsTrue(e.IsEmpty, "The dimension should be empty");
            Assert.AreEqual(0, e.Rows, "Invalid value for rows.");
            Assert.AreEqual(0, e.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void EqualOperator_Dimension_Equal()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(1, 2);

            Assert.IsTrue(a == b, "Incorrect result for equality of dimensions");
        }

        [TestMethod]
        public void EqualOperator_Dimension_NotEqual()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(3, 4);

            Assert.IsFalse(a == b, "Incorrect result for equality of dimensions");
        }

        [TestMethod]
        public void EqualOperator_Tuple_Equal()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(1, 2);

            Assert.IsTrue(a == b, "Incorrect result for equality of dimensions");
        }

        [TestMethod]
        public void EqualOperator_Tuple_NotEqual()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(3, 4);

            Assert.IsFalse(a == b, "Incorrect result for equality of dimensions");
        }

        [TestMethod]
        public void Equals_Dimension_NotEqual()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(3, 4);

            Assert.IsFalse(a.Equals(b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Dimension_OK()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(1, 2);

            Assert.IsTrue(a.Equals(b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Object_Dimension_NotEqual()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(3, 4);

            Assert.IsFalse(a.Equals((object)b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Object_Dimension_OK()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(1, 2);

            Assert.IsTrue(a.Equals((object)b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Object_Other()
        {
            var a = new Dimension(1, 2);

            Assert.IsFalse(a.Equals(6), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Object_Other_Null()
        {
            var a = new Dimension(1, 2);

            Assert.IsFalse(a.Equals((object)null), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Object_Tuple_NotEqual()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(3, 4);

            Assert.IsFalse(a.Equals((object)b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Object_Tuple_Null()
        {
            var a = new Dimension(1, 2);
            var b = (Tuple<int, int>)null;

            Assert.IsFalse(a.Equals((object)b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Object_Tuple_OK()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(1, 2);

            Assert.IsTrue(a.Equals((object)b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Tuple_NotEqual()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(3, 4);

            Assert.IsFalse(a.Equals(b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Tuple_Null()
        {
            var a = new Dimension(1, 2);
            var b = (Tuple<int, int>)null;

            Assert.IsFalse(a.Equals(b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void Equals_Tuple_OK()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(1, 2);

            Assert.IsTrue(a.Equals(b), "Incorrect result from Equals(...) method.");
        }

        [TestMethod]
        public void GetHashCode_OK()
        {
            var rows = 1;
            var cols = 2;
            var hash = (((long)rows) << 32 | cols).GetHashCode();
            var d = new Dimension(rows, cols);

            Assert.AreEqual(hash, d.GetHashCode(), "Incorrect hash code returned.");
        }

        [TestMethod]
        public void Implicit_Cast_ToDimension()
        {
            var t = new Tuple<int, int>(1, 2);
            Dimension d = t;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(2, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void Implicit_Cast_ToDimension_NullTuple()
        {
            var t = (Tuple<int, int>)null;
            Dimension d = t;

            Assert.IsTrue(d.IsEmpty, "The dimension should not be empty.");
        }

        [TestMethod]
        public void Implicit_Cast_ToTuple()
        {
            var d = new Dimension(1, 2);
            Tuple<int, int> t = d;

            Assert.AreEqual(1, t.Item1, "Invalid value for Item1.");
            Assert.AreEqual(2, t.Item2, "Invalid value for Item2.");
        }

        [TestMethod]
        public void MultiplicationOperator_Dimension_OK()
        {
            var a = new Dimension(1, 2);
            var d = new Dimension(2, 3) * a;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(2, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(6, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void MultiplicationOperator_Integer_OK()
        {
            var a = new Dimension(2, 3);
            var d = a * 2;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(4, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(6, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void MultiplicationOperator_Integer_ResultTooSmall()
        {
            var d = new Dimension(2, 1);

            Assert.ThrowsException<InvalidOperationException>(() => d * -1);
        }

        [TestMethod]
        public void MultiplicationOperator_Tuple_ColumnResultTooSmall()
        {
            var a = new Tuple<int, int>(1, -2);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d * a);
        }

        [TestMethod]
        public void MultiplicationOperator_Tuple_OK()
        {
            var a = new Tuple<int, int>(1, 2);
            var d = new Dimension(2, 3) * a;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(2, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(6, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void MultiplicationOperator_Tuple_RowResultTooSmall()
        {
            var a = new Tuple<int, int>(-2, 1);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d * a);
        }

        [TestMethod]
        public void NotEqualOperator_Dimension_Equal()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(1, 2);

            Assert.IsFalse(a != b, "Incorrect result for equality of dimensions");
        }

        [TestMethod]
        public void NotEqualOperator_Dimension_NotEqual()
        {
            var a = new Dimension(1, 2);
            var b = new Dimension(3, 4);

            Assert.IsTrue(a != b, "Incorrect result for equality of dimensions");
        }

        [TestMethod]
        public void NotEqualOperator_Tuple_Equal()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(1, 2);

            Assert.IsFalse(a != b, "Incorrect result for equality of dimensions");
        }

        [TestMethod]
        public void NotEqualOperator_Tuple_NotEqual()
        {
            var a = new Dimension(1, 2);
            var b = new Tuple<int, int>(3, 4);

            Assert.IsTrue(a != b, "Incorrect result for equality of dimensions");
        }

        [TestMethod]
        public void SubtractOperator_Dimension_ColumnResultTooSmall()
        {
            var a = new Dimension(1, 2);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d - a);
        }

        [TestMethod]
        public void SubtractOperator_Dimension_OK()
        {
            var a = new Dimension(1, 1);
            var d = new Dimension(2, 3) - a;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(2, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void SubtractOperator_Dimension_RowResultTooSmall()
        {
            var a = new Dimension(2, 1);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d - a);
        }

        [TestMethod]
        public void SubtractOperator_Integer_ColumnResultTooSmall()
        {
            var d = new Dimension(2, 1);

            Assert.ThrowsException<InvalidOperationException>(() => d - 1);
        }

        [TestMethod]
        public void SubtractOperator_Integer_OK()
        {
            var a = new Dimension(2, 3);
            var d = a - 1;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(2, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void SubtractOperator_Integer_RowResultTooSmall()
        {
            var d = new Dimension(1, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d - 1);
        }

        [TestMethod]
        public void SubtractOperator_Tuple_ColumnResultTooSmall()
        {
            var a = new Tuple<int, int>(1, 2);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d - a);
        }

        [TestMethod]
        public void SubtractOperator_Tuple_OK()
        {
            var a = new Tuple<int, int>(1, 1);
            var d = new Dimension(2, 3) - a;

            Assert.IsFalse(d.IsEmpty, "The dimension should not be empty.");
            Assert.AreEqual(1, d.Rows, "Invalid value for rows.");
            Assert.AreEqual(2, d.Columns, "Invalid value for columns.");
        }

        [TestMethod]
        public void SubtractOperator_Tuple_RowResultTooSmall()
        {
            var a = new Tuple<int, int>(2, 1);
            var d = new Dimension(2, 2);

            Assert.ThrowsException<InvalidOperationException>(() => d - a);
        }

        [TestMethod]
        public void ToString_OK()
        {
            var d = new Dimension(1, 2);

            Assert.AreEqual("(1, 2)", d.ToString(), "Incorrect value from ToString()");
        }

        [TestMethod]
        public void Transpose_OK()
        {
            var rows = 1;
            var cols = 2;
            var d = new Dimension(rows, cols);
            var t = d.Transpose();

            Assert.AreEqual(d.Columns, t.Rows, "Incorrect row count for transposed dimension.");
            Assert.AreEqual(d.Rows, t.Columns, "Incorrect column count for transposed dimension.");
        }
    }
}