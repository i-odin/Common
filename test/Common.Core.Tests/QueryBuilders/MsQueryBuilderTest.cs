using Common.Core.QueryBuilders;
using System.Runtime.CompilerServices;

namespace Common.Core.Tests.QueryBuilders;

public class TestClass
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int? Age { get; set; }
    public DateTime Timespan { get; set; }
}

public class TestClass2
{
    public Guid Id2 { get; set; }
    public string Name2 { get; set; }
    public int? Age2 { get; set; }
    public DateTime Timespan2 { get; set; }
}

public class MsQueryBuilderTest
{
   
}
