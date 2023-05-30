using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders.Queris;

public abstract class UpdateQueryBuilder<T> : QueryBuilder
{
    protected UpdateQueryBuilder(QueryBuilderSource source) : base(source) {}

    public abstract UpdateQueryBuilder<T> Update(Action<TableTranslator<T>> inner);
    public UpdateQueryBuilder<T> Update() => Update(inner: null);
    public UpdateQueryBuilder<T> Where(Action<WhereTranslator<T>> inner)
    {
        MsWhereTranslator<T>.Make(_source, inner);
        return this;
    }
}

public class MsUpdateQueryBuilder<T> : UpdateQueryBuilder<T>
{
    private readonly string _command = "update";

    public MsUpdateQueryBuilder(QueryBuilderSource source) : base(source) {}

    public override MsUpdateQueryBuilder<T> Update(Action<TableTranslator<T>> inner)
    {
        MsTableTranslator<T>.Make(_command,  _source, inner).Run();
        return this;
    }

    public static MsUpdateQueryBuilder<T> Make(QueryBuilderSource source, Action<TableTranslator<T>> inner)
        => new MsUpdateQueryBuilder<T>(source).Update(inner);
}

public class PgUpdateQueryBuilder<T> : UpdateQueryBuilder<T>
{
    private readonly string _command = "update";

    public PgUpdateQueryBuilder(QueryBuilderSource source) : base(source) {}

    public override PgUpdateQueryBuilder<T> Update( Action<TableTranslator<T>> inner)
    {
        PgTableTranslator<T>.Make(_command, _source, inner).Run();
        return this;
    }

    public static PgUpdateQueryBuilder<T> Make(QueryBuilderSource source, Action<TableTranslator<T>> inner)
        => new PgUpdateQueryBuilder<T>(source).Update(inner);
}