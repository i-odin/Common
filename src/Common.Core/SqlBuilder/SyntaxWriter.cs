using Common.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.SqlBuilder
{
    public class SyntaxWriter : StringWriter
    {
        public SyntaxWriter(StringBuilder sb) : base(sb) { }
    }

    public class UpdateWriter : SyntaxWriter
    {
        public UpdateWriter(StringBuilder sb) : base(sb) { }
        public void Test()
        {
            Write("Test");
        }
        
        private UpdateWriter Update()
        {
            Write("update");
            return this;
        }

        public static implicit operator UpdateWriter(StringBuilder sb) 
            => new UpdateWriter(sb).Update();
    }

    public class WhereWriter : SyntaxWriter
    {
        public WhereWriter(StringBuilder sb) : base(sb) { }

        public void Test()
        {
            Write("Test");
        }

        private WhereWriter Where()
        {
            Write("where");
            return this;
        }

        public static implicit operator WhereWriter(StringBuilder sb)
            => new WhereWriter(sb).Where();
    }
}
