using Common.Core.QueryBuilders.Queris;

namespace Common.Core.QueryBuilders.Translators;

public abstract class Translator
{
    public abstract void Run(QueryBuilderSource options);
    protected string GetColumnParameterName(string fieldName, int index)
        => string.Format("{0}{1}", fieldName, index);   
}

public class ChainTranslator : Translator
{
    private Translator _next;
    private readonly Translator _translator;
    public ChainTranslator(Translator translator)
    {
        _translator = translator;
    }

    public ChainTranslator RegisterNext(ChainTranslator translator)
    {
        _next = translator;
        return translator;
    }

    public override void Run(QueryBuilderSource options)
    {
        _translator.Run(options);
        _next?.Run(options);
    }
}

public abstract class CommandTranslator : Translator
{
    protected readonly string _command;
    public CommandTranslator(string command) { _command = command; }
    public override void Run(QueryBuilderSource source)
    {
        source.Query.Append("\r\n").Append(_command).Append(" ");
    }
}

public class TranslatorManager
{
    private readonly List<Translator> translators = new List<Translator>();
    //private ChainTranslator _root;
    //private ChainTranslator _current;

    public Translator Add(Translator translator)
    {
        translators.Add(translator);
        /*var chain = new ChainTranslator(translator);
        if (_root == null) _current = _root = chain;
        else _current = _current.RegisterNext(chain);*/
        return translator;
    }

    public TranslatorManager Run(QueryBuilderSource source)
    {
        foreach (var item in translators)
        {
            item.Run(source);
        }
        //_root?.Run(source);
        return this;
    }
}