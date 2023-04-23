using Common.Core.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.SqlBuilder
{
    public class StringWriterWrap
    {
        private StringBuilder _sb;
        public StringWriterWrap(StringBuilder sb) 
            => _sb = sb;

        protected void WriteLine(string value)
        {
            if (_sb.Length > 0)
                _sb.Append("\r\n");
            _sb.Append(value);
        }
        protected void Write(string value)
            => _sb.Append(value);
        protected void WriteWhitespace()
            => _sb.Append(" ");
    }

    public class SyntaxWriter<T> : StringWriterWrap
         where T : class   
    {
        private bool _isComma;
        public SyntaxWriter(StringBuilder sb) : base(sb) { }
        
        protected void WriteString(string value)
        {
            Write("'");
            Write(value);
            Write("'");
        }

        protected void WriteNull()
        {
            Write("null");
        }

        public SyntaxWriter<T> Set<TField>([NotNull] Expression<Func<T, TField>> field, [NotNull] TField value)
        {
            if (_isComma) Comma();
            else _isComma = true;

            var w = Field(field).Equal();
            if (value == null)
                w.WriteNull();
            else
                w.Value((dynamic)value);

            return w;
        }

        public SyntaxWriter<T> Field<TField>(Expression<Func<T, TField>> field)
        {
            var member = (field.Body as MemberExpression)?.Member;
            if (member is null) throw new InvalidOperationException("Please provide a valid field expression");

            Write(member.Name);
            return this;
        }

        public SyntaxWriter<T> Equal()
        {
            Write(" = ");
            return this;
        }

        public SyntaxWriter<T> Comma()
        {
            Write(", ");
            return this;
        }


        public SyntaxWriter<T> Value(string value)
        {
            if (value != null) WriteString(value);
            else WriteNull();
            return this;
        }

        public SyntaxWriter<T> Value(Guid? value)
        {
            if (value.HasValue) Value(value.Value);
            else WriteNull();
            return this;
        }

        public SyntaxWriter<T> Value(Guid value)
        {
            WriteString(value.ToString());
            return this;
        }

        public SyntaxWriter<T> Value(DateTime? value)
        {
            if (value.HasValue) Value(value.Value);
            else WriteNull();
            return this;
        }

        public SyntaxWriter<T> Value(DateTime value)
        {
            WriteString(value.ToStringIso8601());
            return this;
        }

        public SyntaxWriter<T> Value(int? value)
        {
            if (value.HasValue) Value(value.Value);
            else WriteNull();
            return this;
        }

        public SyntaxWriter<T> Value(int value)
        {
            Write(value.ToString());
            return this;
        }
    }

    public class UpdateWriter<T> : SyntaxWriter<T>
        where T : class
    {
        public UpdateWriter(StringBuilder sb) : base(sb) { }

        private UpdateWriter<T> Update()
        {
            WriteLine("update ");
            Write(typeof(T).Name);
            WriteLine("set ");
            return this;
        }

        public static implicit operator UpdateWriter<T>(StringBuilder sb) 
            => new UpdateWriter<T>(sb).Update();
    }

    public class WhereWriter<T> : SyntaxWriter<T>
        where T : class
    {
        public WhereWriter(StringBuilder sb) : base(sb) { }

        private WhereWriter<T> Where()
        {
            WriteLine("where ");
            return this;
        }

        public static implicit operator WhereWriter<T>(StringBuilder sb)
            => new WhereWriter<T>(sb).Where();
    }
}
