using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Math.LinearAlgebra.UnitTests
{
    [ExcludeFromCodeCoverage]
    [TestCategory("Sparse Array Tests")]
    [TestClass]
    public class SparseArrayTests
    {
        [TestMethod]
        public void Add_Fails() => Assert.ThrowsException<NotSupportedException>(() => (new SparseArray<decimal>(4)).Add(100M));

        [TestMethod]
        public void Clear_OK()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            sa.Clear();

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity after Clear().");
            Assert.AreEqual(2, sa.Count, "Incorrect count after Clear().");
            Assert.AreEqual(0, sa.Sparsity, "Incorrect sparsity after Clear().");
            Assert.AreEqual(0.0M, sa.SparsityPercent, "Incorrect sparsity percent after Clear().");
            Assert.AreEqual(0, sa.Keys.Count(), "Incorrect keys after Clear().");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 0M, 0M }), "Unequal elements in sparse array after Clear()");
        }

        [TestMethod]
        public void Clone_OK()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            var clone = (SparseArray<decimal>)sa.Clone();

            Assert.AreEqual(sa.IsReadOnly, clone.IsReadOnly, "Incorrect readonly value after Clone().");
            Assert.AreEqual(sa.Capacity, clone.Capacity, "Incorrect capacity after Clone().");
            Assert.AreEqual(sa.Count, clone.Count, "Incorrect count after Clone().");
            Assert.AreEqual(sa.Sparsity, clone.Sparsity, "Incorrect sparsity after Clone().");
            Assert.AreEqual(sa.SparsityPercent, clone.SparsityPercent, "Incorrect sparsity percent after Clone().");
            Assert.IsTrue(clone.Keys.SequenceEqual(sa.Keys), "Incorrect keys after Clone().");
            Assert.AreSame(sa.Comparer, clone.Comparer, "Incorrect explicit comparer after Clone().");
            Assert.IsTrue(clone.ToArray().SequenceEqual(sa.ToArray()), "Unequal elements in sparse array after Clone()");
        }

        [TestMethod]
        public void Constructor_Capacity_LessThanOne() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SparseArray<decimal>(0));

        [TestMethod]
        public void Constructor_Capacity_OK_NoComparer()
        {
            var sa = new SparseArray<decimal>(3);

            Assert.IsFalse(sa.IsReadOnly, "Incorrect readonly value.");
            Assert.AreEqual(3, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(3, sa.Count, "Incorrect count.");
            Assert.AreEqual(0, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(0, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.AreSame(Comparer<decimal>.Default, sa.Comparer, "Incorrect default comparer.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 0M, 0M, 0M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Constructor_Capacity_OK_WithComparer()
        {
            var comparer = new MockComparer<decimal>();
            var sa = new SparseArray<decimal>(3, comparer);

            Assert.IsFalse(sa.IsReadOnly, "Incorrect readonly value.");
            Assert.AreEqual(3, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(3, sa.Count, "Incorrect count.");
            Assert.AreEqual(0, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(0, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.AreSame(comparer, sa.Comparer, "Incorrect explicit comparer.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 0M, 0M, 0M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Constructor_Dictionary_DictionaryTooBig() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SparseArray<decimal>(2, new Dictionary<int, decimal> { { 0, 1M }, { 1, 2M }, { 2, 3M } }));

        [TestMethod]
        public void Constructor_Dictionary_NullDictionary() => Assert.ThrowsException<ArgumentNullException>(() => new SparseArray<decimal>(4, (Dictionary<int, decimal>)null));

        [TestMethod]
        public void Constructor_Dictionary_OK_EmptyDictionary()
        {
            var sa = new SparseArray<decimal>(4, new Dictionary<int, decimal>());

            Assert.IsFalse(sa.IsReadOnly, "Incorrect readonly value.");
            Assert.AreEqual(4, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(4, sa.Count, "Incorrect count.");
            Assert.AreEqual(0, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.AreSame(Comparer<decimal>.Default, sa.Comparer, "Incorrect default comparer.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 0M, 0M, 0M, 0M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Constructor_Dictionary_OK_NonEmptyDictionary()
        {
            var sa = new SparseArray<decimal>(4, new Dictionary<int, decimal>() { { 1, 1M }, { 3, 2M } });

            Assert.IsFalse(sa.IsReadOnly, "Incorrect readonly value.");
            Assert.AreEqual(4, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(4, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(0.5M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 1, 3 }), "Incorrect keys.");
            Assert.AreSame(Comparer<decimal>.Default, sa.Comparer, "Incorrect default comparer.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 0M, 1M, 0M, 2M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Constructor_Dictionary_OK_NonEmptyDictionary_ExactLenght()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.IsFalse(sa.IsReadOnly, "Incorrect readonly value.");
            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.AreSame(Comparer<decimal>.Default, sa.Comparer, "Incorrect default comparer.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Constructor_Dictionary_OK_WithComparer()
        {
            var comparer = new MockComparer<decimal>();
            var sa = new SparseArray<decimal>(4, new Dictionary<int, decimal>() { { 1, 1M }, { 3, 2M } }, comparer);

            Assert.IsFalse(sa.IsReadOnly, "Incorrect readonly value.");
            Assert.AreEqual(4, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(4, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(0.5M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 1, 3 }), "Incorrect keys.");
            Assert.AreSame(comparer, sa.Comparer, "Incorrect explicit comparer.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 0M, 1M, 0M, 2M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Constructor_Dictionary_ZeroCapacity() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SparseArray<decimal>(0, new Dictionary<int, decimal>()));

        [TestMethod]
        public void Constructor_Enumerable_EmptyEnumerable() => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new SparseArray<decimal>(new List<decimal>()));

        [TestMethod]
        public void Constructor_Enumerable_Null() => Assert.ThrowsException<ArgumentNullException>(() => new SparseArray<decimal>(null));

        [TestMethod]
        public void Constructor_Enumerable_OK_NoComparer()
        {
            var sa = new SparseArray<decimal>(new List<decimal> { 0M, 1M, 0M, 2M });

            Assert.IsFalse(sa.IsReadOnly, "Incorrect readonly value.");
            Assert.AreEqual(4, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(4, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(0.5M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 1, 3 }), "Incorrect keys.");
            Assert.AreSame(Comparer<decimal>.Default, sa.Comparer, "Incorrect default comparer.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 0M, 1M, 0M, 2M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Constructor_Enumerable_OK_WithComparer()
        {
            var comparer = new MockComparer<decimal>();
            var sa = new SparseArray<decimal>(new List<decimal> { 0M, 1M, 0M, 2M }, comparer);

            Assert.IsFalse(sa.IsReadOnly, "Incorrect readonly value.");
            Assert.AreEqual(4, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(4, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(0.5M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 1, 3 }), "Incorrect keys.");
            Assert.AreSame(comparer, sa.Comparer, "Incorrect explicit comparer.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 0M, 1M, 0M, 2M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Contains_ValueDoesNotExist()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.IsFalse(sa.Contains(3M), "Found value that should not have existed");
        }

        [TestMethod]
        public void Contains_ValueExists()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.IsTrue(sa.Contains(2M), "Failed to find value that should exist");
        }

        [TestMethod]
        public void CopyTo_Array_CorrectSize_ArrayIndex_TooLarge()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.ThrowsException<InvalidOperationException>(() => sa.CopyTo(new decimal[2], 1));
        }

        [TestMethod]
        public void CopyTo_Array_TooSmall()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.ThrowsException<InvalidOperationException>(() => sa.CopyTo(new decimal[1], 0));
        }

        [TestMethod]
        public void CopyTo_ArrayIndex_TooLarge()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sa.CopyTo(new decimal[2], 2));
        }

        [TestMethod]
        public void CopyTo_ArrayIndex_TooSmall()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sa.CopyTo(new decimal[2], -1));
        }

        [TestMethod]
        public void CopyTo_NullArray()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.ThrowsException<ArgumentNullException>(() => sa.CopyTo(null, 0));
        }

        [TestMethod]
        public void CopyTo_OK_Array_ExactSize()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            var ar = new decimal[2];
            sa.CopyTo(ar, 0);

            Assert.IsTrue(ar.SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void CopyTo_OK_Array_Larger_NoOffset()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            var ar = new decimal[4];
            sa.CopyTo(ar, 0);

            Assert.IsTrue(ar.SequenceEqual(new[] { 1M, 2M, decimal.Zero, decimal.Zero }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void CopyTo_OK_Array_Larger_Offset()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            var ar = new decimal[4];
            sa.CopyTo(ar, 1);

            Assert.IsTrue(ar.SequenceEqual(new[] { decimal.Zero, 1M, 2M, decimal.Zero }), "Unequal elements in sparse array");
        }

        [TestMethod]
        public void Enumerating_ForEach()
        {
            var expected = new[] { decimal.Zero, 1M, 2M, decimal.Zero };
            var values = new decimal[4];
            var sa = new SparseArray<decimal>(expected);
            var i = 0;

            foreach (var x in sa)
            {
                values[i] = x;
                i++;
            }

            Assert.IsTrue(values.SequenceEqual(expected), "The values retrieved during enumeration are either out of order, or not all were enumerated");
        }

        [TestMethod]
        public void Enumerating_ForEach_Collection_Modified() => Assert.ThrowsException<InvalidOperationException>(() =>
                                                               {
                                                                   var values = new[] { decimal.Zero, 1M, 2M, decimal.Zero };
                                                                   var sa = new SparseArray<decimal>(values);
                                                                   var i = 0;

                                                                   foreach (var x in sa)
                                                                   {
                                                                       Assert.AreEqual(values[i], x, $"The value at position {i} does not match.");

                                                                       if (i == 1)
                                                                       {
                                                                           sa[2] = 3M;
                                                                       }

                                                                       i++;
                                                                   }
                                                               });

        [TestMethod]
        public void Enumerating_GetEnumerator_Current_HasEnded() => Assert.ThrowsException<InvalidOperationException>(() =>
                                                                  {
                                                                      var expected = new[] { decimal.Zero, 1M, 2M, decimal.Zero };
                                                                      var sa = new SparseArray<decimal>(expected);

                                                                      using (var e = sa.GetEnumerator())
                                                                      {
                                                                          while (e.MoveNext())
                                                                              ;

                                                                          var d = e.Current;
                                                                      }
                                                                  });

        [TestMethod]
        public void Enumerating_GetEnumerator_Current_HasNotStarted() => Assert.ThrowsException<InvalidOperationException>(() =>
                                                                       {
                                                                           var expected = new[] { decimal.Zero, 1M, 2M, decimal.Zero };
                                                                           var sa = new SparseArray<decimal>(expected);

                                                                           using (var e = sa.GetEnumerator())
                                                                           {
                                                                               var d = e.Current;
                                                                           }
                                                                       });

        [TestMethod]
        public void Enumerating_GetEnumerator_Current_OK()
        {
            var expected = new[] { 1M, 2M, 3M, 4M };
            var sa = new SparseArray<decimal>(expected);

            using (var e = sa.GetEnumerator())
            {
                Assert.IsNotNull(e, "Failed to return enumerator");
                Assert.IsTrue(e.MoveNext(), "Failed to move to first element in SparseArray");

                var d = e.Current;

                Assert.AreEqual(decimal.One, d, "Incorrect value returned from Enuermator.Current");
            }
        }

        [TestMethod]
        public void Enumerating_GetEnumerator_IEnumerable()
        {
            var expected = new[] { decimal.Zero, 1M, 2M, decimal.Zero };
            var sa = new SparseArray<decimal>(expected);

            Assert.IsNotNull(((IEnumerable)sa).GetEnumerator(), "Failed to return enumerator");
        }

        [TestMethod]
        public void Enumerating_GetEnumerator_Reset_ModifiedCollection() => Assert.ThrowsException<InvalidOperationException>(() =>
                                                                          {
                                                                              var expected = new[] { 1M, 2M, 3M, 4M };
                                                                              var sa = new SparseArray<decimal>(expected);

                                                                              using (var e = sa.GetEnumerator())
                                                                              {
                                                                                  e.MoveNext();
                                                                                  sa[0] = decimal.Zero;
                                                                                  e.Reset();
                                                                              }
                                                                          });

        [TestMethod]
        public void Enumerating_GetEnumerator_Reset_OK()
        {
            var expected = new[] { 1M, 2M, 3M, 4M };
            var sa = new SparseArray<decimal>(expected);

            using (var e = sa.GetEnumerator())
            {
                while (e.MoveNext())
                    ;

                e.Reset();

                Assert.IsTrue(e.MoveNext(), "Failed to move to the first element after Reset()");
                Assert.AreEqual(decimal.One, e.Current, "Incorrect value at current location");
            }
        }

        [TestMethod]
        public void Enumerating_IEnumerable_GetEnumerator_Current_OK()
        {
            var expected = new[] { 1M, 2M, 3M, 4M };
            var sa = new SparseArray<decimal>(expected);
            var e = ((IEnumerable)sa).GetEnumerator();

            Assert.IsNotNull(e, "Failed to return enumerator");
            Assert.IsTrue(e.MoveNext(), "Failed to move to first element in SparseArray");

            var d = e.Current;

            Assert.IsInstanceOfType(d, typeof(decimal), "Incorrect data type from enumerator.");
            Assert.AreEqual(decimal.One, (decimal)d, "Incorrect value returned from Enuermator.Current");
        }

        [TestMethod]
        public void Indexer_Get_OK()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.AreEqual(2M, sa[1], "Invalid value returned from indexer.");
        }

        [TestMethod]
        public void Indexer_Get_TooLarge() => Assert.ThrowsException<IndexOutOfRangeException>(() => (new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } }))[2], "Failed to throw expected exception.");

        [TestMethod]
        public void Indexer_Get_TooSmall() => Assert.ThrowsException<IndexOutOfRangeException>(() => (new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } }))[-1], "Failed to throw expected exception.");

        [TestMethod]
        public void Indexer_Set_OK()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            sa[1] = 3M;

            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 3M }), "Unequal elements in sparse array after indexer set.");
        }

        [TestMethod]
        public void Indexer_Set_TooLarge() => Assert.ThrowsException<IndexOutOfRangeException>(() => (new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } }))[2] = 3M, "Failed to throw expected exception.");

        [TestMethod]
        public void Indexer_Set_TooSmall() => Assert.ThrowsException<IndexOutOfRangeException>(() => (new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } }))[-1] = 3M, "Failed to throw expected exception.");

        [TestMethod]
        public void IndexOf_ValueDoesNotExist()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.AreEqual(-1, sa.IndexOf(3M), "Incorrect index for non-existent value.");
        }

        [TestMethod]
        public void IndexOf_ValueExists()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.AreEqual(1, sa.IndexOf(2M), "Incorrect index for existing value.");
        }

        [TestMethod]
        public void Insert_OK()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            sa.Insert(1, 3);

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity after insert.");
            Assert.AreEqual(2, sa.Count, "Incorrect count after insert.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity after insert.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent after insert.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys after insert.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 3M }), "Unequal elements in sparse array after insert");
        }

        [TestMethod]
        public void Remove_ValueDoesNotExist()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
            Assert.AreEqual(-1, sa.IndexOf(3M), "Incorrect index for non-existent value.");

            Assert.IsFalse(sa.Remove(3M), "Incorrect return result for removing existing value.");

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity after Remove(...).");
            Assert.AreEqual(2, sa.Count, "Incorrect count after Remove(...).");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity after Remove(...).");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent after Remove(...).");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys after Remove(...).");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array after Remove(...)");
            Assert.AreEqual(-1, sa.IndexOf(3M), "Incorrect index for non-existent value after Remove(...).");
        }

        [TestMethod]
        public void Remove_ValueExists()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            Assert.IsTrue(sa.Remove(2M), "Incorrect return result for removing existing value.");

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity after Remove(...).");
            Assert.AreEqual(2, sa.Count, "Incorrect count after Remove(...).");
            Assert.AreEqual(1, sa.Sparsity, "Incorrect sparsity after Remove(...).");
            Assert.AreEqual(0.5M, sa.SparsityPercent, "Incorrect sparsity percent after Remove(...).");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0 }), "Incorrect keys after Remove(...).");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, decimal.Zero }), "Unequal elements in sparse array after Remove(...)");
        }

        [TestMethod]
        public void RemoveAt_OK()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");

            sa.RemoveAt(1);

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity after RemoveAt(...).");
            Assert.AreEqual(2, sa.Count, "Incorrect count after RemoveAt(...).");
            Assert.AreEqual(1, sa.Sparsity, "Incorrect sparsity after RemoveAt(...).");
            Assert.AreEqual(0.5M, sa.SparsityPercent, "Incorrect sparsity percent after RemoveAt(...).");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0 }), "Incorrect keys after RemoveAt(...).");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, decimal.Zero }), "Unequal elements in sparse array after RemoveAt(...)");
        }

        [TestMethod]
        public void ToArray_OK()
        {
            var sa = new SparseArray<decimal>(2, new Dictionary<int, decimal>() { { 0, 1M }, { 1, 2M } });

            Assert.AreEqual(2, sa.Capacity, "Incorrect capacity.");
            Assert.AreEqual(2, sa.Count, "Incorrect count.");
            Assert.AreEqual(2, sa.Sparsity, "Incorrect sparsity.");
            Assert.AreEqual(1.0M, sa.SparsityPercent, "Incorrect sparsity percent.");
            Assert.IsTrue(sa.Keys.SequenceEqual(new List<int> { 0, 1 }), "Incorrect keys.");
            Assert.IsTrue(sa.ToArray().SequenceEqual(new[] { 1M, 2M }), "Unequal elements in sparse array");
        }
    }
}