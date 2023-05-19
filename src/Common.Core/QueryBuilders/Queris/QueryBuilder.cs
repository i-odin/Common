using Common.Core.QueryBuilders.Translators;
using System.Text;

namespace Common.Core.QueryBuilders.Queris;

public abstract class QueryBuilder
{
    private QueryBuilder _next;
    private readonly List<Translator> _translators = new List<Translator>();
    public void Build(QueryBuilderSource options)
    {
        foreach (var translator in _translators)
            translator.Run(options);

        _next?.Build(options);
    }

    protected int Add(Translator translator)
    {
        _translators.Add(translator);
        return _translators.Count - 1;
    }

    protected Translator Get(int index)
    {
        return _translators[index];
    }

    public QueryBuilder RegisterNext(QueryBuilder builder)
    {
        _next = builder;
        return builder;
    }
}

public class QueryBuilderSource
{
    public StringBuilder Query = new StringBuilder();
    public Parameters Parameters = new Parameters();
    public override string ToString()
        => Query.ToString();
}

public class QueryBuilderManager
{
    private QueryBuilder _root;
    private QueryBuilder _current;
    
    public readonly QueryBuilderSource Source = new QueryBuilderSource();
    public T Add<T>(T builder)
        where T : QueryBuilder
    {
        if (_root == null) _current = _root = builder;
        else _current = _current.RegisterNext(builder);
        return builder;
    }

    public QueryBuilderManager Run()
    {
        _root?.Build(Source);
        return this;
    }

    public override string ToString() => Source.Query.ToString();
}
