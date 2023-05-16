using Common.Core.QueryBuilders.Translators;

namespace Common.Core.QueryBuilders.Queris;

public abstract class BaseQueryBuilder
{
    private readonly List<Translator> _translators = new List<Translator>();
    public void Build(QueryBuilderOptions options)
    {
        foreach (var translator in _translators)
            translator.Run(options);
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
}
