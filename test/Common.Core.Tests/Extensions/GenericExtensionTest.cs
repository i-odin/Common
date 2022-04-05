using Common.Core.Extensions;

namespace Common.Core.Tests.Extensions;

public class GenericExtensionTest
{
    [Theory]
    [InlineData(EnumTest.One, EnumTest.Two)]
    public void In_MultipleEnums_ReturnTrue(EnumTest inputOne, EnumTest inputTwo)
    {
        const EnumTest enumOne = EnumTest.One;
        var result = enumOne.In(inputOne, inputTwo);
        Assert.True(result);
    }

    [Theory]
    [InlineData(EnumTest.Two, EnumTest.Three)]
    public void In_MultipleEnums_ReturnFalse(EnumTest inputOne, EnumTest inputTwo)
    {
        const EnumTest enumOne = EnumTest.One;
        var result = enumOne.In(inputOne, inputTwo);
        Assert.False(result);
    }

    public enum EnumTest : byte
    {
        One,
        Two,
        Three
    }
}