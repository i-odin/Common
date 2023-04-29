using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.QueryBuilder
{
    public interface IWhereTranslator<T>
        where T : class
    {
        IWhereTranslator<T> Equal<TField>([NotNull] Expression<Func<T, TField>> field, TField value);
        IWhereTranslator<T> NotEqual<TField>([NotNull] Expression<Func<T, TField>> field, TField value);
        IWhereTranslator<T> Bracket(Action<IWhereTranslator<T>> inner);
        IWhereTranslator<T> And();
        IWhereTranslator<T> Or();
    }

    public class WhereTranslator<T> : Translator<T>, IWhereTranslator<T>
        where T : class
    {
        public WhereTranslator(StringBuilder sb) : base(sb) { }

        IWhereTranslator<T> IWhereTranslator<T>.NotEqual<TField>([NotNull] Expression<Func<T, TField>> field, TField value)
        {
            NotEqual(field, value);
            return this;
        }

        IWhereTranslator<T> IWhereTranslator<T>.Equal<TField>(Expression<Func<T, TField>> field, TField value)
        {
            Equal(field, value);
            return this;
        }

        IWhereTranslator<T> IWhereTranslator<T>.And()
        {
            And();
            return this;
        }

        IWhereTranslator<T> IWhereTranslator<T>.Or()
        {
            Or();
            return this;
        }

        IWhereTranslator<T> IWhereTranslator<T>.Bracket(Action<IWhereTranslator<T>> inner) 
            => Bracket(inner);

        public WhereTranslator<T> Bracket(Action<WhereTranslator<T>> inner)
        {
            BracketLeft();
            inner(_sb);
            BracketRitht();
            return this;
        }

        public WhereTranslator<T> Where()
        {
            AppendLine("where ");
            return this;
        }

        public static implicit operator WhereTranslator<T>(StringBuilder sb)
            => new WhereTranslator<T>(sb);
    }
}
