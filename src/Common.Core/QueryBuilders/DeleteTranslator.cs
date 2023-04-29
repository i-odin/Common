using System.Text;

namespace Common.Core.QueryBuilder
{
    public interface IDeleteTranslator<T>
        where T : class {}

    public class DeleteTranslator<T> : Translator<T>, IDeleteTranslator<T>
        where T : class
    {
        public DeleteTranslator(StringBuilder sb) : base(sb) { }

        public DeleteTranslator<T> Delete()
        {
            AppendLine("delete ");
            Append(typeEntity.Name);
            return this;
        }

        public static implicit operator DeleteTranslator<T>(StringBuilder sb)
            => new DeleteTranslator<T>(sb).Delete();
    }
}
