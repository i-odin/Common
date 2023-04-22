using Common.Core.Models;
using Common.Core.SqlBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Tests.SqlBuilder
{
    public class Test
    {
        [Theory]
        [InlineData("Hello")]
        public void Equals_CompareTwoObjects(string str)
        {
            var builder = new MsSqlBuilder().Update<TestClass>(x => x.Test()).Where<TestClass>(x => x.Test());
            var qwe = builder.ToString();
            Assert.Equal(1, 1);
        }
    }

    public class TestClass
    {

    }
}
