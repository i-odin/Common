using Common.Core.QueryBuilders.Translator;
using System.Text;

namespace Common.Core.QueryBuilders.Query;

public abstract class RootQueryBuilder
{
    private readonly ICollection<TranslatorNew> _translators = new List<TranslatorNew>();
    public void Build(StringBuilder sb)
    {
        foreach (var translator in _translators)
            translator.Run(sb);
    }

    protected void Add(TranslatorNew translator)
        => _translators.Add(translator);
}
