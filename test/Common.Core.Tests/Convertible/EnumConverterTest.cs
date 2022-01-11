
using Common.Core.Convertible;
using Xunit;

namespace Common.Core.Tests.Convertible
{

    public class EnumConverterTest
    {
        [Theory]
        [InlineData(0, TestConvertEnum.Zero)]
        [InlineData(1, TestConvertEnum.One)]
        [InlineData(2, TestConvertEnum.Two)]
        public void EnumConverter_ConvertIntToEnum_ReturnTrue(int value, TestConvertEnum expected)
        {
            var result = EnumConverter<TestConvertEnum>.Convert(value);
            
            Assert.Equal(expected, result);
        }

        public enum TestConvertEnum
        {
            Zero = 0,
            One = 1,
            Two = 2
        }
    }
}
