using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public abstract class RootQueryBuilder
{
    private readonly ICollection<TranslatorNew> _translators = new List<TranslatorNew>();
    public void Build(StringBuilder sb)
    { 
        foreach (var translator in _translators) { translator.Run(sb); }
    }

    protected void Add(TranslatorNew translator) 
        => _translators.Add(translator);
}

public abstract class DeleteQueryBuilder<T> : RootQueryBuilder
{
    public DeleteQueryBuilder<T> Delete(Action<TranslatorTable<T>> inner)
    {
        Add(MsDeleteTranslator<T>.Make(inner));
        return this;
    }
}

public class MsDeleteQueryBuilder<T> : DeleteQueryBuilder<T> 
{
    public static DeleteQueryBuilder<T> Make(Action<TranslatorTable<T>> inner) 
        => new MsDeleteQueryBuilder<T>().Delete(inner);
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
