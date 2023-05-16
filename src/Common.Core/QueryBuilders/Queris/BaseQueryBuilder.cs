using Common.Core.QueryBuilders.Translators;
using System.Text;

namespace Common.Core.QueryBuilders.Queris;

public abstract class BaseQueryBuilder
{
    private readonly ICollection<Translator> _translators = new List<Translator>();
    public void Build(QueryBuilderOptions options)
    {
        foreach (var translator in _translators)
            translator.Run(options);
    }

    protected void Add(Translator translator)
        => _translators.Add(translator);
}
