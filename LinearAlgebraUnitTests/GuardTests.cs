using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace System.Math.LinearAlgebra.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestCategory("Guard Tests")]
    [TestClass]
    public class GuardTests
    {
        [TestMethod]
        public void ThrowIfArgumentNull_Value_NonNull() => Guard.ThrowIfArgumentNull(new object(), "paramName");

        [TestMethod]
        public void ThrowIfArgumentNull_Value_Null() => Assert.ThrowsException<ArgumentNullException>(
            () => Guard.ThrowIfArgumentNull(null, "paramName"),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfGreaterThan_Inclusive_Value_OK() => Guard.ThrowIfGreaterThan(99, 100, "paramName", true);

        [TestMethod]
        public void ThrowIfGreaterThan_Inclusive_Value_TooBig() => Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => Guard.ThrowIfGreaterThan(100, 100, "paramName", true),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfGreaterThan_Value_OK() => Guard.ThrowIfGreaterThan(100, 100, "paramName");

        [TestMethod]
        public void ThrowIfGreaterThan_Value_TooBig() => Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => Guard.ThrowIfGreaterThan(101, 100, "paramName"),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfIndexGreaterThan_Inclusive_Value_OK() => Guard.ThrowIfIndexGreaterThan(99, 100, true);

        [TestMethod]
        public void ThrowIfIndexGreaterThan_Inclusive_Value_TooBig() =>
            Assert.ThrowsException<IndexOutOfRangeException>(
                () => Guard.ThrowIfIndexGreaterThan(100, 100, true),
                "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfIndexGreaterThan_Value_OK() => Guard.ThrowIfIndexGreaterThan(100, 100);

        [TestMethod]
        public void ThrowIfIndexGreaterThan_Value_TooBig() => Assert.ThrowsException<IndexOutOfRangeException>(
            () => Guard.ThrowIfIndexGreaterThan(101, 100),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfIndexLessThan_Inclusive_Value_OK() => Guard.ThrowIfIndexLessThan(1, 0, true);

        [TestMethod]
        public void ThrowIfIndexLessThan_Inclusive_Value_TooSmall() => Assert.ThrowsException<IndexOutOfRangeException>(
            () => Guard.ThrowIfIndexLessThan(0, 0, true),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfIndexLessThan_Value_OK() => Guard.ThrowIfIndexLessThan(0, 0);

        [TestMethod]
        public void ThrowIfIndexLessThan_Value_TooSmall() => Assert.ThrowsException<IndexOutOfRangeException>(
            () => Guard.ThrowIfIndexLessThan(-1, 0),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfIndexLessThanZero_Value_OK() => Guard.ThrowIfIndexLessThanZero(0);

        [TestMethod]
        public void ThrowIfIndexLessThanZero_Value_TooSmall() => Assert.ThrowsException<IndexOutOfRangeException>(
            () => Guard.ThrowIfIndexLessThanZero(-1),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfLessThan_Inclusive_Value_OK() => Guard.ThrowIfLessThan(1, 0, "paramName", true);

        [TestMethod]
        public void ThrowIfLessThan_Inclusive_Value_TooSmall() => Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => Guard.ThrowIfLessThan(0, 0, "paramName", true),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfLessThan_Value_OK() => Guard.ThrowIfLessThan(0, 0, "paramName");

        [TestMethod]
        public void ThrowIfLessThan_Value_TooSmall() => Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => Guard.ThrowIfLessThan(-1, 0, "paramName"),
            "Failed to throw expected exception");

        [TestMethod]
        public void ThrowIfLessThanZero_Value_OK() => Guard.ThrowIfLessThanZero(0, "paramName");

        [TestMethod]
        public void ThrowIfLessThanZero_Value_TooSmall() => Assert.ThrowsException<ArgumentOutOfRangeException>(
            () => Guard.ThrowIfLessThanZero(-1, "paramName"),
            "Failed to throw expected exception");
    }
}