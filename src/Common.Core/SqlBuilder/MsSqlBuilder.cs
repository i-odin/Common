using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.SqlBuilder
{
    public class QueryBuilder
    {
        private Action<UpdateWriter> _execute; 
        //private ActionWriter<Test> _execute;
        public void Update<T>(Action<UpdateWriter> inner) where T : class
        {
            var qwe = new SyntaxWriter();
            inner?.Invoke((UpdateWriter)qwe);
            //var qwe = new QueryGeneration().Update(x => x.Write(""));
            _execute += inner;
            //return this;
        }

        public void Where<T>(Action<WhereWriter> inner) where T : class
        {

        }
    }

    public class MsSqlBuilder : QueryBuilder
    {
        
    }
}
