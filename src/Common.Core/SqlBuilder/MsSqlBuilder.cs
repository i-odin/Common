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
        private Action<StringBuilder> _execute;

        private QueryBuilder UpdateImpl<T>([NotNull] Action<UpdateWriter<T>> inner) where T : class
        {
            _execute += sb => inner(sb);
            return this;
        }

        private QueryBuilder WhereImpl<T>(Action<WhereWriter<T>> inner) where T : class
        {
            _execute += sb => inner.Invoke(sb);
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder(500);
            _execute.Invoke(sb);
            return sb.ToString();
        }
    }

    public class MsSqlBuilder : QueryBuilder
    {
        
    }
}
