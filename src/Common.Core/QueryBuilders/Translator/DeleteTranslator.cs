using System.Text;


namespace Common.Core.QueryBuilders.Translator;

public interface IDeleteTranslator<T>
    where T : class
{ }

public class DeleteTranslator<T> : Translator<T>, IDeleteTranslator<T>
    where T : class
{
    public DeleteTranslator(StringBuilder sb) : base(sb) { }

    public DeleteTranslator<T> Delete()
    {
        AppendNewLine("delete ").Append(typeEntity.Name);
        return this;
    }

    public static DeleteTranslator<T> Delete(StringBuilder sb) 
        => new DeleteTranslator<T>(sb).Delete();

    public static implicit operator DeleteTranslator<T>(StringBuilder sb)
        => new DeleteTranslator<T>(sb);
}
