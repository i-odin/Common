using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.SqlBuilder
{
    public interface IUpdateTranslator<T>
        where T : class 
    {
        IUpdateTranslator<T> Set<TField>([NotNull] Expression<Func<T, TField>> field, TField value);
    }

    public class UpdateTranslator<T> : Translator<T>, IUpdateTranslator<T>
        where T : class
    {
        private bool _isComma;
        public UpdateTranslator(StringBuilder sb) : base(sb) { }

        IUpdateTranslator<T> IUpdateTranslator<T>.Set<TField>(Expression<Func<T, TField>> field, TField value)
        {   
            if (_isComma) Comma();
            else _isComma = true;

            Field(field).Equal();
            if (value is null)
                AppendNull();
            else //dynemic оказался быстрее, чем каст к типу (value is string)
                Value((dynamic)value);

            return this;
        }

        private UpdateTranslator<T> Update()
        {
            AppendLine("update ");
            Append(typeEntity.Name);
            AppendLine("set ");
            return this;
        }

        public static implicit operator UpdateTranslator<T>(StringBuilder sb)
            => new UpdateTranslator<T>(sb).Update();
    }
}