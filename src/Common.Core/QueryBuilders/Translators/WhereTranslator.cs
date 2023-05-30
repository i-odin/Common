using Common.Core.QueryBuilders.Queris;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.Core.QueryBuilders.Translators;

public abstract class WhereTranslator<T> : CommandTranslator
{
    public WhereTranslator(string command, QueryBuilderSource source) : base(command, source) { }
    
    public WhereTranslator<T> EqualTo<TField>([NotNull] Expression<Func<T, TField>> column, TField value)
    {
        new EqualToTranslator<T>(CommonExpression.GetColumnName(column), value, source).Run();
        return this;
    }

    public WhereTranslator<T> EqualTo(string column, object value)
    {
        new EqualToTranslator<T>(column, value, source).Run();
        return this;
    }

    public WhereTranslator<T> And()
    {
        new AndTranslator(source).Run();
        return this;
    }
}

public class MsWhereTranslator<T> : WhereTranslator<T>
{
    public MsWhereTranslator(string command, QueryBuilderSource source) : base(command, source) { }
    public static MsWhereTranslator<T> Make(QueryBuilderSource source, Action<WhereTranslator<T>> inner)
    {
        var obj = new MsWhereTranslator<T>("where", source);
        obj.Run();
        inner?.Invoke(obj);
        return obj;
    }
}

public class EqualToTranslator<T> : Translator
{
    private string _columnName;
    private object _value;
    
    public EqualToTranslator(string columnName, object value, QueryBuilderSource source) : base(source)
    {
        _value = value;
        _columnName = columnName;
    }

    public override void Run()
    {
        source.Parameters.Add(_value, out string name);
        source.Query.Append(_columnName).Append(" = @").Append(name);
    }
}

public class AndTranslator : Translator
{
    public AndTranslator(QueryBuilderSource source) : base(source) { }

    public override void Run()
    {
        source.Query.Append(" and ");
    }
}