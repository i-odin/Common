using System.Text;
using Common.Core.QueryBuilders.Query;

namespace Common.Core.Tests.QueryBuilders.Query;

public class UpdateQueryBuilderTest
{
    /*
    [Theory]
    [InlineData(@"update TestClass
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'")]

    public void Update_BuildSql(string expected)
    {
        var builder = new UpdateQueryBuilder<TestClass>(new StringBuilder())
            .Update(x => x.Set(y => y.Id, Guid.Empty)
                                    .Set(y => y.Name, null)
                                    .Set(y => y.Age, 10)
                                    .Set(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, builder.ToString());
    }

    [Theory]
    [InlineData(@"update TestClass
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'
where Id = '00000000-0000-0000-0000-000000000000' and Name = null or Age = 10 and Timespan = '2023-04-23T00:00:00.0000000'")]
    public void UpdateWhere_BuildSql(string expected)
    {
        var builder = ((IUpdateQueryBuilder<TestClass>)new UpdateQueryBuilder<TestClass>(new StringBuilder()))
            .Update(x => x.Set(y => y.Id, Guid.Empty)
                                     .Set(y => y.Name, null)
                                     .Set(y => y.Age, 10)
                                     .Set(y => y.Timespan, new DateTime(2023, 04, 23)))
            .Where(x => x.Equal(y => y.Id, Guid.Empty).And()
                                    .Equal(y => y.Name, null).Or()
                                    .Equal(y => y.Age, 10).And()
                                    .Equal(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, builder.ToString());
    }

    [Theory]
    [InlineData(@"update TestClass
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'
from TestClass
join TestClass2 on TestClass.Id = TestClass2.Id2, TestClass.Age = TestClass2.Age2
where Id = '00000000-0000-0000-0000-000000000000' and Name = null or Age = 10 and Timespan = '2023-04-23T00:00:00.0000000'")]
    public void UpdateJoinWhere_BuildSql(string expected)
    {
        var builder = ((IUpdateQueryBuilder<TestClass>)new UpdateQueryBuilder<TestClass>(new StringBuilder()))
            .Update(x => x.Set(y => y.Id, Guid.Empty)
                          .Set(y => y.Name, null)
                          .Set(y => y.Age, 10)
                          .Set(y => y.Timespan, new DateTime(2023, 04, 23)))
            .Join<TestClass2>(x => x.Equal(y => y.Id, y => y.Id2)
                                    .Equal( y => y.Age, y => y.Age2))
            .Where(x => x.Equal(y => y.Id, Guid.Empty).And()
                         .Equal(y => y.Name, null).Or()
                         .Equal(y => y.Age, 10).And()
                         .Equal(y => y.Timespan, new DateTime(2023, 04, 23)));

        Assert.Equal(expected, builder.ToString());
    }*/
}
