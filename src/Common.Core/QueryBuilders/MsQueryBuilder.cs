using Common.Core.QueryBuilders.Query;
using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders;

public abstract class QueryBuilder
{
    private readonly StringBuilder _sb = new();
    private readonly ICollection<RootQueryBuilder> _builders = new List<RootQueryBuilder>();

    public DeleteQueryBuilder<T> Delete<T>(Action<TranslatorTable<T>> inner)
    {
        var result = MsDeleteQueryBuilder<T>.Make(inner);
        _builders.Add(result);
        return result;
    }

    public void Build()
    {
        foreach (var item in _builders) 
            item.Build(_sb);
    }

    public override string ToString() => _sb.ToString();
}

public class MsQueryBuilder : QueryBuilder
{
    

    /*public IInsertQueryBuilder<T> Insert<T>(Action<IInsertTranslator<T>> inner)
        where T : class
        => InsertQueryBuilder<T>.Insert(_sb, inner);

    public IUpdateQueryBuilder<T> Update<T>(Action<IUpdateTranslator<T>> inner)
        where T : class
        => UpdateQueryBuilder<T>.Update(_sb, inner);
    */
}
