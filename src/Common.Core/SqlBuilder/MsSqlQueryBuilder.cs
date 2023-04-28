using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Common.Core.SqlBuilder
{
    public partial class QueryBuilder
    {
        public QueryBuilder Insert<T>(Action<IInsertTranslator<T>> inner)
            where T : class
            => InsertImpl(inner);

        public QueryBuilder Update<T>(Action<IUpdateTranslator<T>> inner) 
            where T : class 
            => UpdateImpl(inner);

        public QueryBuilder Where<T>(Action<IWhereTranslator<T>> inner) 
            where T : class 
            => WhereImpl(inner);

        public QueryBuilder Delete<T>()
            where T : class
            => DeleteImpl<T>();

        public override string ToString()
            => _sb.ToString();
    }

    public partial class QueryBuilder
    {
        private StringBuilder _sb = new StringBuilder();

        private QueryBuilder InsertImpl<T>([NotNull] Action<InsertTranslator<T>> inner)
            where T : class
        {
            var obj = (InsertTranslator<T>)_sb;
            obj.BracketLeft();
            inner(obj);
            obj.BracketRitht();
            return this;
        }

        private QueryBuilder UpdateImpl<T>([NotNull] Action<UpdateTranslator<T>> inner) 
            where T : class
        {
            inner(_sb);
            return this;
        }

        private QueryBuilder WhereImpl<T>([NotNull] Action<WhereTranslator<T>> inner) 
            where T : class
        {
            inner(((WhereTranslator<T>)_sb).Where());
            return this;
        }

        private QueryBuilder DeleteImpl<T>()
            where T : class
        {
            var _ = (DeleteTranslator<T>)_sb;
            return this;
        }
    }

    public class MsSqlQueryBuilder : QueryBuilder
    {
        
    }
}
