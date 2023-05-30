using Common.Core.QueryBuilders.Queris;

namespace Common.Core.QueryBuilders.Translators;

public abstract class Translator
{
    protected readonly QueryBuilderSource source;
    public Translator(QueryBuilderSource source) { this.source = source; }
    public abstract void Run();
}

public abstract class CommandTranslator : Translator
{
    protected readonly string _command;
    public CommandTranslator(string command, QueryBuilderSource source) : base(source) 
    {
        _command = command;
    }

    public override void Run()
    {
        source.Query.Append("\r\n").Append(_command).Append(" ");
    }
}