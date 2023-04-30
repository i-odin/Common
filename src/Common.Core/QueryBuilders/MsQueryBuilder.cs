using Common.Core.QueryBuilders.Query;
using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders;

public class MsQueryBuilder
{
    private readonly StringBuilder _sb = new();
    public override string ToString() => _sb.ToString();

    public IInsertQueryBuilder Insert<T>(Action<IInsertTranslator<T>> inner)
        where T : class
        => InsertQueryBuilder.Create(_sb, inner);

    public IUpdateQueryBuilder Update<T>(Action<IUpdateTranslator<T>> inner)
        where T : class
        => UpdateQueryBuilder.Create(_sb, inner);

    public IDeleteQueryBuilder Delete<T>()
        where T : class
        => DeleteQueryBuilder.Create<T>(_sb);
}
