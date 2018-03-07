using System;
using IslandOfMisfitTypes.Windows;
using Xunit;

namespace IslandOfMisfitTypes.UnitTests.Windows
{
    public class RegistryGuidConverterTests
    {
        [Theory]
        [InlineData(
            "5C6F5296-AC5D-40FD-AE20-3C5E2E704077", "6925F6C5-D5CA-DF04-EA02-C3E5E2070477")]
        public void Convert_AGuid_ConvertsTheGuid(string fromString, string toString)
        {
            var guidToConvert = new Guid(fromString);
            var expected = new Guid(toString);
            var actual = RegistryGuidConverter.Convert(guidToConvert);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Convert_IsItsOwnInverse()
        {
            var expected = Guid.NewGuid();
            Assert.Equal(
                expected, RegistryGuidConverter.Convert(RegistryGuidConverter.Convert(expected)));
        }
    }
}
