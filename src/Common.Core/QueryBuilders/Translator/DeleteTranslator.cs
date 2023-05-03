using System.Text;


namespace Common.Core.QueryBuilders.Translator;

public abstract class TranslatorNew
{
    public void Run()
    {

    }
}

public abstract class DeleteTranslator<T> : TranslatorNew
{

}

public class MsDeleteTranslator<T> : DeleteTranslator<T>
{

}

/*
public interface IDeleteTranslator<T>
{ }

public class DeleteTranslator<T> : Translator<T>, IDeleteTranslator<T>
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
*/