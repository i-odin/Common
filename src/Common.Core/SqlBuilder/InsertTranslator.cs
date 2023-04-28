using System.Linq.Expressions;
using System.Text;

namespace Common.Core.SqlBuilder
{
    public interface IInsertTranslator<T>
        where T : class
    {
        InsertTranslator<T> Field<TField>(Expression<Func<T, TField>> field);
    }

    public class InsertTranslator<T> : Translator<T>, IInsertTranslator<T>
        where T : class
    {
        private bool _isComma;
        public InsertTranslator(StringBuilder sb) : base(sb) {}

        InsertTranslator<T> IInsertTranslator<T>.Field<TField>(Expression<Func<T, TField>> field)
        {
            if (_isComma) Comma();
            else _isComma = true;

            Field(field);
            return this;
        }

        private InsertTranslator<T> Insert()
        {
            AppendLine("insert ");
            Append(typeEntity.Name);
            AppendLine(" ");
            return this;
        }

        public static implicit operator InsertTranslator<T>(StringBuilder sb)
            => new InsertTranslator<T>(sb).Insert();
    }
}
