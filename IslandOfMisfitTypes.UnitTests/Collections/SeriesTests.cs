using System;
using System.Collections.Generic;
using System.Linq;
using IslandOfMisfitTypes.Collections;
using Xunit;

namespace IslandOfMisfitTypes.UnitTests.Collections
{
    public class SeriesTests
    {
        [Fact]
        public void ArrayConstructor_NullInitialValues_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Series<int>(null, i => i[0]));
            Assert.Equal("initialValues", ex.ParamName);
        }

        [Fact]
        public void ArrayConstructor_NullFunc_Throws()
        {
            var ex =
                Assert.Throws<ArgumentNullException>(() => new Series<int>(new[] { 1 }, null));
            Assert.Equal("func", ex.ParamName);
        }

        [Fact]
        public void ArrayConstructor_ValidArgs_Constructs()
        {
            var _ = new Series<int>(new[] { 1 }, i => i[0]);
        }

        [Fact]
        public void Next_InvokesFuncWithExpectedArguments()
        {
            var seriesSpy = new SeriesSpy(new[] { 8, 3, 0, 6, 6, 2, 3 });
            var series = new Series<int>(seriesSpy.NextArgs, seriesSpy);
            for (var i = 0; i < 10; i += 1) series.Next();
            Assert.True(seriesSpy.Verify());
        }

        [Fact]
        public void Next_ReturnsValuesFromFunc()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var nextVal = -1;
            var series = new Series<int>(new int[0], (args) =>
            {
                var currentVal = nextVal;
                nextVal = random.Next();
                return currentVal;
            });
            for (var i = 0; i < 10; i += 1)
            {
                Assert.Equal(nextVal, series.Next());
            }
        }

        [Fact]
        public void Reset_BeforeNext_IsBenign()
        {
            var expected = new[] { 1, 2, 3, 4, 5 };
            var series = new Series<int>(new[] { 1 }, t => t[0] + 1);
            series.Reset();
            foreach (var value in expected)
            {
                Assert.Equal(value, series.Next());
            }
        }

        [Fact]
        public void Reset_DuringInitialValues_ResetsSeries()
        {
            var expected = new[] { 1, 2, 3, 4, 5 };
            var series = new Series<int>(new[] { 1, 2, 3 }, t => t[2] + 1);
            series.Next();
            series.Next();
            series.Reset();
            foreach (var value in expected)
            {
                Assert.Equal(value, series.Next());
            }
        }

        [Fact]
        public void Reset_AfterInitialValues_ResetsSeries()
        {
            var expected = new[] { 1, 2, 3, 4, 5 };
            var series = new Series<int>(new[] { 1, 2, 3 }, t => t[2] + 1);
            for (int i = 0; i < expected.Length * 2; i += 1)
            {
                series.Next();
            }
            series.Reset();
            foreach (var value in expected)
            {
                Assert.Equal(value, series.Next());
            }
        }

        [Fact]
        public void CopyConstructor_NullSource_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Series<int>(null));
            Assert.Equal("source", ex.ParamName);
        }

        [Fact]
        public void CopyConstructor_CopiesSeries()
        {
            var expected = new[] { 1, 2, 3, 4, 5 };
            var sourceSeries = new Series<int>(new[] { 1 }, t => t[0] + 1);
            var series = new Series<int>(sourceSeries);
            foreach (var value in expected)
            {
                Assert.Equal(value, series.Next());
            }
        }

        [Fact]
        public void CopyConstructor_ResetsNewSeriesByDefault()
        {
            var expected = new[] { 1, 2, 3, 4, 5 };
            var sourceSeries = new Series<int>(new[] { 1 }, t => t[0] + 1);
            sourceSeries.Next();
            var series = new Series<int>(sourceSeries);
            foreach (var value in expected)
            {
                Assert.Equal(value, series.Next());
            }
        }

        [Fact]
        public void CopyConstructor_PreservePosition_DoesNotResetNewSeries()
        {
            var expected = new[] { 2, 3, 4, 5 };
            var sourceSeries = new Series<int>(new[] { 1 }, t => t[0] + 1);
            sourceSeries.Next();
            var series = new Series<int>(sourceSeries, true);
            foreach (var value in expected)
            {
                Assert.Equal(value, series.Next());
            }
        }

        [Fact]
        public void OneArgConstructor_NullFunc_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Series<int>(0, null));
            Assert.Equal("func", ex.ParamName);
        }

        [Fact]
        public void OneArgConstructor_NonNullFunc_Constructs()
        {
            var _ = new Series<int>(0, i => i + 1);
        }

        [Fact]
        public void Next_OneArgConstructor_FuncIsInvokedCorrectly()
        {
            var seriesSpy = new SeriesSpy(new[] { 1 });
            Func<int[], int> spyFunc = seriesSpy;
            var func = new Func<int, int>(i => spyFunc.Invoke(new int[] { i }));
            var series = new Series<int>(seriesSpy.NextArgs[0], func);
            for (int i = 0; i < seriesSpy.NextArgs.Length * 2; i += 1)
            {
                series.Next();
            }
            Assert.True(seriesSpy.Verify());
        }

        [Fact]
        public void TwoArgConstructor_NullFunc_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Series<int>(0, 1, null));
            Assert.Equal("func", ex.ParamName);
        }

        [Fact]
        public void TwoArgConstructor_NonNullFunc_Constructs()
        {
            var _ = new Series<int>(0, 1, (a, b) => b + 1);
        }

        [Fact]
        public void Next_TwoArgConstructor_FuncIsInvokedCorrectly()
        {
            var t = new[] { 0, 1 };
            var seriesSpy = new SeriesSpy(t);
            Func<int[], int> spyFunc = seriesSpy;
            var func = new Func<int, int, int>((a, b) => spyFunc.Invoke(new int[] { a, b }));
            var series = new Series<int>(t[0], t[1], func);
            for (int i = 0; i < seriesSpy.NextArgs.Length * 2; i += 1)
            {
                series.Next();
            }
            Assert.True(seriesSpy.Verify());
        }

        [Fact]
        public void ThreeArgConstructor_NullFunc_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Series<int>(0, 1, 2, null));
            Assert.Equal("func", ex.ParamName);
        }

        [Fact]
        public void ThreeArgConstructor_NonNullFunc_Constructs()
        {
            var _ = new Series<int>(0, 1, 2, (a, b, c) => c + 1);
        }

        [Fact]
        public void Next_ThreeArgConstructor_FuncIsInvokedCorrectly()
        {
            var t = new[] { 0, 1, 2 };
            var seriesSpy = new SeriesSpy(t);
            Func<int[], int> spyFunc = seriesSpy;
            var func =
                new Func<int, int, int, int>((a, b, c) => spyFunc.Invoke(new int[] { a, b, c }));
            var series = new Series<int>(t[0], t[1], t[2], func);
            for (int i = 0; i < seriesSpy.NextArgs.Length * 2; i += 1)
            {
                series.Next();
            }
            Assert.True(seriesSpy.Verify());
        }

        [Fact]
        public void FourArgConstructor_NullFunc_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Series<int>(0, 1, 2, 3, null));
            Assert.Equal("func", ex.ParamName);
        }

        [Fact]
        public void FourArgConstructor_NonNullFunc_Constructs()
        {
            var _ = new Series<int>(0, 1, 2, 3, (a, b, c, d) => d + 1);
        }

        [Fact]
        public void Next_FourArgConstructor_FuncIsInvokedCorrectly()
        {
            var t = new[] { 0, 1, 2, 3 };
            var seriesSpy = new SeriesSpy(t);
            Func<int[], int> spyFunc = seriesSpy;
            var func =
                new Func<int, int, int, int, int>(
                    (a, b, c, d) => spyFunc.Invoke(new int[] { a, b, c, d }));
            var series = new Series<int>(t[0], t[1], t[2], t[3], func);
            for (int i = 0; i < seriesSpy.NextArgs.Length * 2; i += 1)
            {
                series.Next();
            }
            Assert.True(seriesSpy.Verify());
        }

        [Fact]
        public void FiveArgConstructor_NullFunc_Throws()
        {
            var ex =
                Assert.Throws<ArgumentNullException>(() => new Series<int>(0, 1, 2, 3, 4, null));
            Assert.Equal("func", ex.ParamName);
        }

        [Fact]
        public void FiveArgConstructor_NonNullFunc_Constructs()
        {
            var _ = new Series<int>(0, 1, 2, 3, 4, (a, b, c, d, e) => e + 1);
        }

        [Fact]
        public void Next_FiveArgConstructor_FuncIsInvokedCorrectly()
        {
            var t = new[] { 0, 1, 2, 3, 4 };
            var seriesSpy = new SeriesSpy(t);
            Func<int[], int> spyFunc = seriesSpy;
            var func =
                new Func<int, int, int, int, int, int>(
                    (a, b, c, d, e) => spyFunc.Invoke(new int[] { a, b, c, d, e}));
            var series = new Series<int>(t[0], t[1], t[2], t[3], t[4], func);
            for (int i = 0; i < seriesSpy.NextArgs.Length * 2; i += 1)
            {
                series.Next();
            }
            Assert.True(seriesSpy.Verify());
        }
    }

    internal class SeriesSpy
    {
        private readonly Random _random;
        private int[] _expectedArgs;
        private bool _receivedUnexpectedArgs;

        internal int[] NextArgs => _expectedArgs.ToArray();

        internal SeriesSpy(int[] initialValues)
        {
            if (initialValues.Length < 1)
            {
                throw new ArgumentException("Length must be greater than 0.", nameof(initialValues));
            }

            _random = new Random(Guid.NewGuid().GetHashCode());
            _expectedArgs = initialValues.ToArray();
        }

        public static implicit operator Func<int[], int>(SeriesSpy spy)
        {
            return args =>
            {
                spy._receivedUnexpectedArgs = !args.SequenceEqual(spy._expectedArgs);
                spy._expectedArgs = spy._expectedArgs.Skip(1).Append(spy._random.Next()).ToArray();
                return spy._expectedArgs.Last();
            };
        }

        public bool Verify() => !_receivedUnexpectedArgs;
    }
}
