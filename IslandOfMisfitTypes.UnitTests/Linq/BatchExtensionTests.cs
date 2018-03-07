using System;
using System.Collections.Generic;
using System.Linq;
using IslandOfMisfitTypes.Linq;
using Xunit;

namespace IslandOfMisfitTypes.UnitTests.Linq
{
    public class BatchExtensionTests
    {
        [Fact]
        public void Batch_NullSource_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(
               () => (null as IEnumerable<int>).Batch(new[] { 1, 2, 3 }));
            Assert.Equal("target", ex.ParamName);
        }

        [Fact]
        public void Batch_NullBatchSizes_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => "abcdefg".Batch(null));
            Assert.Equal("batchSizes", ex.ParamName);
        }

        [Fact]
        public void Batch_EmptySource_ReturnsZeroBatches()
        {
            var batches = "".Batch(new[] { 1, 2, 3 });
            Assert.Empty(batches);
        }

        [Fact]
        public void Batch_EmptyBatchSizes_ReturnsZeroBatches()
        {
            var batches = "abcdefg".Batch(new int[0]);
            Assert.Empty(batches);
        }

        [Fact]
        public void Batch_SumOfBatchSizesIsLessThanSourceCount_BatchSizesAreCorrect()
        {
            var expected = new[] { 1, 2, 3 };
            var actual = "abcdefg".Batch(expected).Select(b => b.Count());
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Batch_BatchSizesIncludesZeros_BatchSizesAreCorrect()
        {
            var expected = new[] { 1, 0, 2, 0, 3 };
            var actual = "abcdefg".Batch(expected).Select(b => b.Count());
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Batch_BatchSizesIncludesNegatives_BatchSizesAreCorrect()
        {
            var batchSizes = new[] { 1, -1, 2, -2, 3 };
            var expected = batchSizes.Select(s => s > 0 ? s : 0);
            var actual = "abcdefg".Batch(expected).Select(b => b.Count());
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Batch_SumOfBatchSizesIsLessThanSourceCount_BatchContentsAreCorrect()
        {
            var batchSizes = new[] { 1, -1, 2, -2, 3 };
            var expected = new[] { "a", "", "bc", "", "def" };
            var actual =  "abcdefg".Batch(batchSizes);
            Assert.True(expected.SequenceEqual(actual.Select(s => string.Concat(s))));
        }

        [Fact]
        public void Batch_SumOfBatchSizesIsGreaterThanSourceCount_BatchSizesAreCorrect()
        {
            var batchSizes = new[] { 1, -1, 2, -2, 3, 7, 3 };
            var expected = new[] { 1, 0, 2, 0, 3, 1 };
            var actual = "abcdefg".Batch(batchSizes).Select(s => s.Count());
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Batch_SumOfBatchSizesIsGreaterThanSourceCount_BatchContentsAreCorrect()
        {
            var batchSizes = new[] { 1, -1, 2, -2, 3, 7, 3 };
            var expected = new[] { "a", "","bc", "", "def", "g" };
            var actual = "abcdefg".Batch(batchSizes);
            Assert.True(expected.SequenceEqual(actual.Select(s => string.Concat(s))));
        }
    }
}
