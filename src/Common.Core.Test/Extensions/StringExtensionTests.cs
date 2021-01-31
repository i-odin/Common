using System;
using Common.Core.Extensions;
using Xunit;

namespace Common.Core.Test.Extensions
{
    public class StringExtensionTests
    {
        [Fact]
        public void IsDigitsOnlyTrue()
        {
            const string str = "1119987465131511";

            bool strTrue = str.IsDigitsOnly();

            Assert.True(strTrue);
        }

        [Fact]
        public void IsDigitsOnlyFalse()
        {
            const string str = "11199wqeqe8746513qweqweqwe1511";

            bool strTrue = str.IsDigitsOnly();

            Assert.False(strTrue);
        }
        
        [Fact]
        public void ToArrayEmptyNull()
        {

            const string str = "";

            var array = str.ToArray<int>();

            Assert.Empty(array);
        }

        [Fact]
        public void ToArrayEmptyWhiteSpace()
        {
            const string str = " ";

            var array = str.ToArray<int>();

            Assert.Empty(array);
        }

        
        [Fact]
        public void ToArrayTrue()
        {
            const string str = "1;1;1;9;9";

            var array = str.ToArray<int>();

            Assert.Equal(5, array.Length);
        }

        [Fact]
        public void UnixTimeToDateTimeConvert()
        {
            const string str = "1611151178";

            var dateTime = str.UnixTimeToDateTime();
            
            Assert.Equal(new DateTime(2021,01,20, 13,59,38, DateTimeKind.Utc), dateTime);
        }

        [Fact]
        public void UnixTimeToDateTimeNotConvert()
        {
            const string str = "1;1;1;9;9";

            var dateTime = str.UnixTimeToDateTime();

            Assert.Equal(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), dateTime);
        }
    }
}
