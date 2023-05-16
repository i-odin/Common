using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders.Queris;

public abstract class UpdateQueryBuilder<T> : BaseQueryBuilder
{
    public abstract UpdateQueryBuilder<T> Update(Action<TableTranslator<T>> inner);
    public UpdateQueryBuilder<T> Update() => Update(inner: null);
    public UpdateQueryBuilder<T> Where(Action<WhereTranslator<T>> inner)
    {
        Add(MsWhereTranslator<T>.Make(inner));
        return this;
    }
}

public class MsUpdateQueryBuilder<T> : UpdateQueryBuilder<T>
{
    private readonly string _command = "update";
    public override MsUpdateQueryBuilder<T> Update(Action<TableTranslator<T>> inner)
    {
        Add(MsTableTranslator<T>.Make(_command, inner));
        return this;
    }

    public static MsUpdateQueryBuilder<T> Make(Action<TableTranslator<T>> inner)
        => new MsUpdateQueryBuilder<T>().Update(inner);
}

public class PgUpdateQueryBuilder<T> : UpdateQueryBuilder<T>
{
    private readonly string _command = "update";
    public override PgUpdateQueryBuilder<T> Update(Action<TableTranslator<T>> inner)
    {
        Add(PgTableTranslator<T>.Make(_command, inner));
        return this;
    }

    public static PgUpdateQueryBuilder<T> Make(Action<TableTranslator<T>> inner)
        => new PgUpdateQueryBuilder<T>().Update(inner);
}