using System.Linq;
using IslandOfMisfitTypes.Collections;
using Xunit;

namespace IslandOfMisfitTypes.UnitTests.Collections
{
    public class SeriesTests
    {
        [Fact]
        public void Triangle()
        {
            var expected = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var triangle = new Series<int>(1, a => a + 1);
            var actual = expected.Select(_ => triangle.Next());
            Assert.True(expected.SequenceEqual(actual));
        }

        [Fact]
        public void Fibonacci()
        {
            var expected = new[] { 0, 1, 1, 2, 3, 5, 8, 13, 21 };
            var fibonacci = new Series<int>(0, 1, (a, b) => a + b);
            var actual = expected.Select(_ => fibonacci.Next());
            Assert.True(expected.SequenceEqual(actual));
        }
    }
}
