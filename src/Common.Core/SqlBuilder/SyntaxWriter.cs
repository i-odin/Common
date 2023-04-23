using Common.Core.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text;

namespace Common.Core.SqlBuilder
{
    public class SyntaxWriter 
    {
        private StringWriter _writer;
        public SyntaxWriter(StringBuilder sb) 
        {
            _writer = new StringWriter(sb);
        }

        protected void WriteLine(string value)
        {
            if(_writer.GetStringBuilder().Length > 0)
                _writer.Write(_writer.NewLine); 
            _writer.Write(value);
        }
        protected void Write(string value)
            => _writer.Write(value);
        protected void WriteWhitespace()
            => _writer.Write(" ");
        protected void WriteLineWhitespaceBefore(string value)
        {
            WriteLine(value);
            WriteWhitespace();
        }
        protected void WriteWhitespaceBefore(string value)
        {
            Write(value);
            WriteWhitespace();
        }

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
    }

    public class UpdateWriter<T> : SyntaxWriter
        where T : class
    {
        private bool _isComma;
        public UpdateWriter(StringBuilder sb) : base(sb) { }

        public UpdateWriter<T> Set<TField>([NotNull] Expression<Func<T, TField>> field, [NotNull] TField value)
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

        public UpdateWriter<T> Field<TField>(Expression<Func<T, TField>> field)
        {
            var member = (field.Body as MemberExpression)?.Member;
            if (member is null) throw new InvalidOperationException("Please provide a valid field expression");

            WriteWhitespaceBefore(member.Name);
            return this;
        }

        public UpdateWriter<T> Equal()
        {
            WriteWhitespaceBefore("=");
            return this;
        }

        public UpdateWriter<T> Comma()
        {
            WriteWhitespaceBefore(",");
            return this;
        }
        

        public UpdateWriter<T> Value(string value)
        {
            if (value != null) WriteString(value);
            else WriteNull();
            return this;
        }

        public UpdateWriter<T> Value(Guid? value)
        {   
            if (value.HasValue) Value(value.Value);
            else WriteNull();
            return this;
        }

        public UpdateWriter<T> Value(Guid value)
        {
            WriteString(value.ToString());
            return this;
        }

        public UpdateWriter<T> Value(DateTime? value)
        {
            if (value.HasValue) Value(value.Value);
            else WriteNull();
            return this;
        }

        public UpdateWriter<T> Value(DateTime value)
        {
            WriteString(value.ToStringIso8601());
            return this;
        }

        public UpdateWriter<T> Value(int? value)
        {
            if (value.HasValue) Value(value.Value);
            else WriteNull();
            return this;
        }

        public UpdateWriter<T> Value(int value)
        {
            Write(value.ToString());
            return this;
        }

        private UpdateWriter<T> Update()
        {
            WriteLineWhitespaceBefore("update");
            WriteWhitespaceBefore(typeof(T).Name);
            WriteLineWhitespaceBefore("set");
            return this;
        }

        public static implicit operator UpdateWriter<T>(StringBuilder sb) 
            => new UpdateWriter<T>(sb).Update();
    }

    public class WhereWriter : SyntaxWriter
    {
        public WhereWriter(StringBuilder sb) : base(sb) { }

        public void Test()
        {
            Write("Test");
        }

        private WhereWriter Where()
        {
            Write("where");
            return this;
        }

        public static implicit operator WhereWriter(StringBuilder sb)
            => new WhereWriter(sb).Where();
    }
}
