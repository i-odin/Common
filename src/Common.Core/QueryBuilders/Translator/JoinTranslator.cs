using System.Text;

namespace Common.Core.QueryBuilders.Translator;

public interface IJoinTranslator<TJoin>
    where TJoin : class
{
}

public class JoinTranslator<TJoin> : Translator<TJoin>, IJoinTranslator<TJoin>
    where TJoin : class
{
    public JoinTranslator(StringBuilder sb) : base(sb) { }

    public static implicit operator JoinTranslator<TJoin>(StringBuilder sb)
        => new JoinTranslator<TJoin>(sb);
}
