using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Common.Core.SqlBuilder
{
    public partial class QueryBuilder
    {
        public QueryBuilder Update<T>(Action<IUpdateTranslator<T>> inner) 
            where T : class 
            => UpdateImpl(inner);

        public QueryBuilder Where<T>(Action<IWhereTranslator<T>> inner) 
            where T : class 
            => WhereImpl(inner);

        public override string ToString()
            => _sb.ToString();
    }

    public partial class QueryBuilder
    {
        private StringBuilder _sb = new StringBuilder();

        private QueryBuilder UpdateImpl<T>([NotNull] Action<UpdateTranslator<T>> inner) 
            where T : class
        {
            inner(_sb);
            return this;
        }

        private QueryBuilder WhereImpl<T>([NotNull] Action<WhereTranslator<T>> inner) 
            where T : class
        {
            Func<WhereTranslator<T>, WhereTranslator<T>> wrap = x => x.Where();
            inner(wrap(_sb));
            return this;
        }
    }

    public class MsSqlBuilder : QueryBuilder
    {
        
    }
}
