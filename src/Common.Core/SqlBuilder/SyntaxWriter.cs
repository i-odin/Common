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

        protected void WriteLine(string str)
        {
            if(_writer.GetStringBuilder().Length > 0)
                _writer.Write(_writer.NewLine); 
            _writer.Write(str);
        }
        protected void Write(string str)
            => _writer.Write(str);
        protected void WriteWhitespace()
            => _writer.Write(" ");
        protected void WriteLineWhitespaceBefore(string str)
        {
            WriteLine(str);
            WriteWhitespace();
        }
        protected void WriteWhitespaceBefore(string str)
        {
            Write(str);
            WriteWhitespace();
        }
    }

    public class UpdateWriter<T> : SyntaxWriter
        where T : class
    {
        public UpdateWriter(StringBuilder sb) : base(sb) { }

        public UpdateWriter<T> Set<TField>([NotNull] Expression<Func<T, TField>> field, [NotNull] TField value)
        {
            return Field(field).Equal().Value((dynamic)value);
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
        

        public UpdateWriter<T> Value(string str)
        {
            Write("'");
            Write(str);
            Write("'");
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
