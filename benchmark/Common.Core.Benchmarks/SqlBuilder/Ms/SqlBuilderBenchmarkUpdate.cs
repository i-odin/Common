using Common.Core.QueryBuilders;
using System.Text;

namespace Common.Core.Benchmarks.SqlBuilder.Ms
{
    [RankColumn]
    [MemoryDiagnoser]
    public class SqlBuilderBenchmarkUpdate
    {
        [Benchmark]
        public string QueryBuilder() 
        {
            var guid = Guid.Empty;
            var date = new DateTime(2023, 04, 30);
            var age = 10;
            string name = null;
            var builder = new MsCommonQueryBuilder();
            builder.Insert<BenchmarkClass>()
                   .Values(y => y.Value(x => x.Id, guid)
                                 .Value(x => x.Timespan, date)
                                 .Value(x => x.Name, name)
                                 .Value(x => x.Age, age));
            builder.Update<BenchmarkClass>()
                   .Where(x => x.EqualTo(y => y.Id, guid).And()
                                .EqualTo(y => y.Name, name).And()
                                .EqualTo(y => y.Age, age).And()
                                .EqualTo(y => y.Timespan, date));
            builder.Delete<BenchmarkClass>()
                   .Where(x => x.EqualTo(y => y.Id, guid).And()
                                .EqualTo(y => y.Name, name).And()
                                .EqualTo(y => y.Age, age).And()
                                .EqualTo(y => y.Timespan, date));
            return builder.ToString();
        }

        [Benchmark]
        public string QueryBuilderNoExpression()
        {
            var guid = Guid.Empty;
            var date = new DateTime(2023, 04, 30);
            var age = 10;
            string name = null;
            var builder = new MsCommonQueryBuilder();
            builder.Insert<BenchmarkClass>()
                   .Values(y => y.Value(nameof(BenchmarkClass.Id), guid)
                                 .Value(nameof(BenchmarkClass.Timespan), date)
                                 .Value(nameof(BenchmarkClass.Name), name)
                                 .Value(nameof(BenchmarkClass.Age), age));
            builder.Update<BenchmarkClass>()
                   .Where(x => x.EqualTo(nameof(BenchmarkClass.Id), guid).And()
                                .EqualTo(nameof(BenchmarkClass.Name), name).And()
                                .EqualTo(nameof(BenchmarkClass.Age), age).And()
                                .EqualTo(nameof(BenchmarkClass.Timespan), date));
            builder.Delete<BenchmarkClass>()
                   .Where(x => x.EqualTo(nameof(BenchmarkClass.Id), guid).And()
                                .EqualTo(nameof(BenchmarkClass.Name), name).And()
                                .EqualTo(nameof(BenchmarkClass.Age), age).And()
                                .EqualTo(nameof(BenchmarkClass.Timespan), date));
            return builder.ToString();
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

            builder.AppendLine("update BenchmarkClass");
            builder.AppendLine("set ");
            builder.Append("Id = ");
            builder.Append("'").Append(Guid.Empty).Append("'").Append(", ");
            builder.Append("Name = ").Append(name).Append(", ");
            builder.Append("Age = ").Append(age).Append(", ");
            builder.Append("Timespan = ").Append("'").Append(new DateTime(2023, 04, 23)).Append("'");

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
