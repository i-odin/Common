using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public class QueryBuilderOptions
{
    public StringBuilder StringBuilder { get; set; } = new StringBuilder();
    public Parameters Parameters { get; set; } = new Parameters();
    public override string ToString() 
        => StringBuilder.ToString();
}

public abstract class RootQueryBuilder
{
    private readonly ICollection<TranslatorNew> _translators = new List<TranslatorNew>();
    public void Build(QueryBuilderOptions options)
    {
        foreach (var translator in _translators)
            translator.Run(options);
    }

    protected void Add(TranslatorNew translator)
        => _translators.Add(translator);
}
