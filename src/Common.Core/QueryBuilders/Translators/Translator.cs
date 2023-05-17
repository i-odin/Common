using Common.Core.QueryBuilders.Queris;

namespace Common.Core.QueryBuilders.Translators;

public abstract class Translator
{
    public abstract void Run(QueryBuilderOptions options);
    protected string GetColumnParameterName(string fieldName, int index)
        => string.Format("{0}{1}", fieldName, index);
}

public abstract class CommandTranslator : Translator
{
    protected readonly string _command;
    public CommandTranslator(string command) { _command = command; }
    public override void Run(QueryBuilderOptions options)
    {
        options.StringBuilder.Append("\r\n").Append(_command).Append(" ");
    }
}