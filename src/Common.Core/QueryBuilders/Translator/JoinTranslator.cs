using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.QueryBuilders.Translator
{
    public interface IJoinTranslator<T>
        where T : class
    {
    }

    internal class JoinTranslator<T> : Translator<T>, IJoinTranslator<T>
        where T : class
    {
        public JoinTranslator(StringBuilder sb) : base(sb) { }

        public JoinTranslator<T> Join()
        {
            /*
             FROM Geeks1  
INNER JOIN Geeks2 ON Geeks1.col1 = Geeks2.col1  

             */
            /*AppendNewLine("from ");
            _sb.Append(typeEntity.Name);
            _sb.Append(" ");
            BracketLeft();
            _indexInsert = _sb.Length;

            AppendNewLine("values ");
            BracketLeft();*/
            return this;
        }

        public static implicit operator JoinTranslator<T>(StringBuilder sb)
            => new JoinTranslator<T>(sb);
    }
}
