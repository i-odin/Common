using Common.Core.Structs;

namespace Common.Core.Tests.Structs;

public class KeyValueStringTest
{
    [Fact]
    public void Equals_CompareTwoStructs_ReturnTrue()
    {
        var str1 = new KeyValueString("1", "2");
        var str2 = new KeyValueString("1", "2");

        bool strEqual1 = str1.Equals(str2);
        bool strEqual2 = str2.Equals(str1);
        bool strEqual3 = str1 == str2;
        bool strEqual4 = str2 == str1;

        Assert.True(strEqual1);
        Assert.True(strEqual2);
        Assert.True(strEqual3);
        Assert.True(strEqual4);
    }

    [Fact]
    public void Equals_CompereTwoStructs_ReturnFalse()
    {
        var str1 = new KeyValueString("1", "2");
        var str2 = new KeyValueString("2", "1");

        bool strEqual1 = str1.Equals(str2);
        bool strEqual2 = str2.Equals(str1);
        bool strEqual3 = str1 != str2;
        bool strEqual4 = str2 != str1;

        Assert.False(strEqual1);
        Assert.False(strEqual2);
        Assert.True(strEqual3);
        Assert.True(strEqual4);
    }

    [Fact]
    public void GetHashCode_CompareTwoStructs_ReturnTrue()
    {
        var str1 = new KeyValueString("1", "2");
        var str2 = new KeyValueString("1", "2");

        bool strEqual1 = str1.GetHashCode() == str2.GetHashCode();
        bool strEqual2 = str2.GetHashCode() == str1.GetHashCode();

        Assert.True(strEqual1);
        Assert.True(strEqual2);
    }

    [Fact]
    public void GetHashCode_CompareTwoStructs_ReturnFalse()
    {
        var str1 = new KeyValueString("1", "2");
        var str2 = new KeyValueString("2", "1");

        bool strEqual1 = str1.GetHashCode() == str2.GetHashCode();
        bool strEqual2 = str2.GetHashCode() == str1.GetHashCode();

        Assert.False(strEqual1);
        Assert.False(strEqual2);
    }

    [Fact]
    public void ToString_CompareOriginal_ReturnTrue()
    {
        var str = new KeyValueString("1", "2");

        var result = str.ToString();

        Assert.Equal("1=2;", result);
    }
}