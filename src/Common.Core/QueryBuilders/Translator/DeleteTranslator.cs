using System.Text;


namespace Common.Core.QueryBuilders.Translator;

public interface IDeleteTranslator<T>
    where T : class
{ }

public class DeleteTranslator<T> : Translator<T>, IDeleteTranslator<T>
    where T : class
{
    public DeleteTranslator(StringBuilder sb) : base(sb) { }

    public static implicit operator DeleteTranslator<T>(StringBuilder sb)
        => (DeleteTranslator<T>)new DeleteTranslator<T>(sb).Delete();
}
