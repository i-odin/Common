using System;
using System.Text;
using Common.Core.Structs;
using Common.Core.Extensions;
using Xunit;

namespace Common.Core.Tests.Extensions
{
    public class StringBuilderExtensionTest
    {
        [Theory]
        [InlineData("Key", ':', "Value", ';', "Key:Value;")]
        [InlineData("", ':', " ", ';', ": ;")]
        [InlineData(null, ':', " ", ';', ": ;")]
        public void AppendJoin_AppendString_ReturnTrue(string key, char keyValueSeparator, string value, char separator, string expected)
        {
            var sb = new StringBuilder();
            var spanValues = new ReadOnlySpan<KeyValueString>(new[] { new KeyValueString(key, value, keyValueSeparator, separator) });

            var result = sb.AppendJoin(in spanValues);
            
            Assert.Equal(expected, result.ToString());
        }
    }
}