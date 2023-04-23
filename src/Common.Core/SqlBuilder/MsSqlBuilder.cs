using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.SqlBuilder
{
    public partial class QueryBuilder
    {
        public QueryBuilder Update<T>(Action<UpdateWriter<T>> inner) where T : class 
            => UpdateImpl<T>(inner);

        public QueryBuilder Where<T>(Action<WhereWriter<T>> inner) where T : class 
            => WhereImpl<T>(inner);
    }

    public partial class QueryBuilder
    {
        private StringBuilder _sb = new StringBuilder();

        private QueryBuilder UpdateImpl<T>([NotNull] Action<UpdateWriter<T>> inner) where T : class
        {
            inner(_sb);
            return this;
        }

        private QueryBuilder WhereImpl<T>(Action<WhereWriter<T>> inner) where T : class
        {
            inner(_sb);
            return this;
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }

    public class MsSqlBuilder : QueryBuilder
    {
        
    }
}
