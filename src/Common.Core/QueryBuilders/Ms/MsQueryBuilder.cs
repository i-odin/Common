using Common.Core.QueryBuilders.Query;
using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Ms;
public class MsQueryBuilder
{
    private StringBuilder _sb = new();
    public IInsertQueryBuilder Insert<T>(Action<IInsertTranslator<T>> inner) 
        where T : class 
        => InsertQueryBuilder.Create(_sb, inner);

    public override string ToString() => _sb.ToString();
}
