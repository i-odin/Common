using Common.Core.SqlBuilder;
using System.Text;
using System.Xml.Linq;

namespace Common.Core.Benchmarks.SqlBuilder.Ms
{
    [RankColumn]
    [MemoryDiagnoser]
    public class SqlBuilderBenchmarkUpdate
    {
        [Benchmark]
        public string QueryBuilderNew()
        {
            return new QueryBuilder().Update<BenchmarkClass>(x => x.SetNew(y => y.Id, Guid.Empty)
                                                                   .SetNew(y => y.Name, null)
                                                                   .SetNew(y => y.Age, 10)
                                                                   .SetNew(y => y.Timespan, new DateTime(2023, 04, 23)))
                                     .ToString();
        }

        [Benchmark]
        public string QueryBuilder() {
            return new QueryBuilder().Update<BenchmarkClass>(x => x.Set(y => y.Id, Guid.Empty)
                                                                   .Set(y => y.Name, null)
                                                                   .Set(y => y.Age, 10)
                                                                   .Set(y => y.Timespan, new DateTime(2023, 04, 23)))
                                     .ToString();
        }

        [Benchmark]
        public string StringBuilder()
        {
            string name = null;
            int age = 10;
            var builder = new StringBuilder();
            builder.AppendLine("update BenchmarkClass");
            builder.AppendLine("set ");
            builder.Append("Id = ");
            builder.Append("'").Append(Guid.Empty).Append("'").Append(", ");
            builder.Append("Name = ").Append(name).Append(", ");
            builder.Append("Age = ").Append(age).Append(", ");
            builder.Append("Timespan = ").Append("'").Append(new DateTime(2023, 04, 23)).Append("'");
            return builder.ToString();
        }
    }

    public class BenchmarkClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime Timespan { get; set; }
    }
}
