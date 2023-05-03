using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public abstract class RootQueryBuilder
{
    private readonly ICollection<TranslatorNew> _translators = new List<TranslatorNew>();
    private readonly StringBuilder _sb;
    public RootQueryBuilder(StringBuilder sb) => _sb = sb;

    public void Build()
    { 
    }
}

public abstract class DeleteQueryBuilder<T> : RootQueryBuilder
{
    public DeleteQueryBuilder(StringBuilder sb) : base(sb) { }

    public DeleteQueryBuilder<T> Delete()
    {
        return this;
    }
}

public class MsDeleteQueryBuilder<T> : DeleteQueryBuilder<T> 
{
    public MsDeleteQueryBuilder(StringBuilder sb) : base(sb) { }

    public static MsDeleteQueryBuilder<T> Make(StringBuilder sb) 
        => new MsDeleteQueryBuilder<T>(sb);
}

/*public class DeleteQueryBuilder<T> : BaseQueryBuilder<T>, IDeleteQueryBuilder<T>
{
    public DeleteQueryBuilder(StringBuilder sb) : base(sb) {}

    public DeleteQueryBuilder<T> Delete()
    {
        DeleteTranslator<T>.Delete(_sb);
        return this;
    }

    public static DeleteQueryBuilder<T> Delete(StringBuilder sb)
        => new DeleteQueryBuilder<T>(sb).Delete();

    IDeleteQueryBuilder<T> IDeleteQueryBuilder<T>.Delete()
    {
        Delete();
        return this;
    }

    IDeleteQueryBuilder<T> IDeleteQueryBuilder<T>.Where(Action<IWhereTranslator<T>> inner)
    {
        Where(inner);
        return this;
    }
}*/
