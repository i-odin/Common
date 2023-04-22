using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.SqlBuilder
{
    public interface ISyntax
    { }

    public interface IUpdate : ISyntax
    {
        void Test();
    }

    public class SyntaxWriter : StringWriter
    {
    }

    public class UpdateWriter : SyntaxWriter, IUpdate
    {
        public void Test()
        {
            Write("Test");
        }
    }

    public class WhereWriter : SyntaxWriter
    {
    }
}
