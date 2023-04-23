using Common.Core.SqlBuilder;

namespace Common.Core.Profiler.SqlBuilder.Ms
{
    public static class MsSqlBuilderProfiler
    {
        public static void Run()
        {
            //for (int i = 0; i < 10000; i++)
            {
                var builder = new MsSqlBuilder().Update<ProfilerClass>(x => x.Set(y => y.Id, Guid.Empty)
                                                                                .Set(y => y.Name, null)
                                                                                .Set(y => y.Age, 10)
                                                                                .Set(y => y.Timespan, new DateTime(2023, 04, 23)));
                builder.ToString();
            }
        }
    }

    public class ProfilerClass
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Timespan { get; set; }
    }
}
