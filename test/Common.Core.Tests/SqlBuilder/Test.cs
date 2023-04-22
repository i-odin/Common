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
            Action<IUpdate> inner = x => x.Test();
            var qwe = new SyntaxWriter();
            //var qwe = new UpdateWriter();
            var asd = (IUpdate)(ISyntax)qwe; 
            inner?.Invoke((IUpdate)qwe);
            var ewq = qwe.ToString();
            Assert.Equal(1, 1);
        }
    }
}
