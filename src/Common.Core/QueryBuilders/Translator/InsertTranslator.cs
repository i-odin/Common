using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public interface IInsertTranslator<T>
    where T : class
{
    IInsertTranslator<T> Values<TField>(Expression<Func<T, TField>> field, TField value);
}

public class InsertTranslator<T> : Translator<T>, IInsertTranslator<T>
    where T : class
{
    public InsertTranslator(StringBuilder sb) : base(sb) { }
    
    IInsertTranslator<T> IInsertTranslator<T>.Values<TField>(Expression<Func<T, TField>> field, TField value) 
        => (IInsertTranslator<T>)Values(field, value);

    public static implicit operator InsertTranslator<T>(StringBuilder sb)
        => new InsertTranslator<T>(sb);
}
