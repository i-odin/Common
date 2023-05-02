using Common.Core.QueryBuilders;

namespace Common.Core.Tests.QueryBuilders;

public class MsQueryBuilderTest
{
    [Theory]
    [InlineData(@"insert into TestClass (Id, Timespan, Name, Age)
values ('00000000-0000-0000-0000-000000000000', '2023-04-30T00:00:00.0000000', null, 10)
insert into TestClass2 (Id2, Timespan2, Name2, Age2)
values ('00000000-0000-0000-0000-000000000000', '2023-04-30T00:00:00.0000000', null, 10)
update TestClass
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'
from TestClass
join TestClass2 on TestClass.Id = TestClass2.Id2, TestClass.Age = TestClass2.Age2
where (Id = '00000000-0000-0000-0000-000000000000' and Name <> null) or (Age = 10 and Timespan <> '2023-04-23T00:00:00.0000000')
delete TestClass
where Id = '00000000-0000-0000-0000-000000000000'
delete TestClass2
where Id2 = '00000000-0000-0000-0000-000000000000'")]
    public void InsertUpdateDelete_BuildSql(string expected)
    {
        var builder = new MsQueryBuilder();
        builder.Insert<TestClass>(x => x.Values(y => y.Id, Guid.Empty)
                                        .Values(y => y.Timespan, new DateTime(2023, 04, 30))
                                        .Values(y => y.Name, null)
                                        .Values(y => y.Age, 10));

        builder.Insert<TestClass2>(x => x.Values(y => y.Id2, Guid.Empty)
                                        .Values(y => y.Timespan2, new DateTime(2023, 04, 30))
                                        .Values(y => y.Name2, null)
                                        .Values(y => y.Age2, 10));

        builder.Update<TestClass>(x => x.Set(y => y.Id, Guid.Empty)
                                        .Set(y => y.Name, null)
                                        .Set(y => y.Age, 10)
                                        .Set(y => y.Timespan, new DateTime(2023, 04, 23)))
               .Join<TestClass2>(x => x.Equal(y => y.Id, y => y.Id2)
                                       .Equal(y => y.Age, y => y.Age2))
               .Where(x => x.Bracket(y => y.Equal(y => y.Id, Guid.Empty)
                                           .And()
                                           .NotEqual(y => y.Name, null))
                            .Or()
                            .Bracket(y => y.Equal(y => y.Age, 10)
                                           .And()
                                           .NotEqual(y => y.Timespan, new DateTime(2023, 04, 23))));

        builder.Delete<TestClass>()
               .Where(x => x.Equal(y => y.Id, Guid.Empty));

        builder.Delete<TestClass2>()
               .Where(x => x.Equal(y => y.Id2, Guid.Empty));


        Assert.Equal(expected, builder.ToString());
    }
}

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
