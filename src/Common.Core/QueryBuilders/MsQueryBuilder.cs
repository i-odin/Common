using Common.Core.QueryBuilders.Query;
using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders;

public class MsQueryBuilder
{
    private readonly StringBuilder _sb = new();
    public override string ToString() => _sb.ToString();

    public IInsertQueryBuilder<T> Insert<T>(Action<IInsertTranslator<T>> inner)
        where T : class
        => InsertQueryBuilder<T>.Insert(_sb, inner);

    public IUpdateQueryBuilder<T> Update<T>(Action<IUpdateTranslator<T>> inner)
        where T : class
        => UpdateQueryBuilder<T>.Update(_sb, inner);

    public IDeleteQueryBuilder<T> Delete<T>()
        where T : class
        => DeleteQueryBuilder<T>.Delete(_sb);
}
