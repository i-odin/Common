using Common.Core.QueryBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Tests.SqlBuilder;

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
    [Theory]
    [InlineData(@"update TestClass
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'
where Id = '00000000-0000-0000-0000-000000000000' and Name = null or Age = 10 and Timespan = '2023-04-23T00:00:00.0000000'")]
    public void UpdateWhere_BuildUpdateWhereSql(string expected)
    {
        var builder = new MsQueryBuilder()
            .Update<TestClass>(x => x.Set(y => y.Id, Guid.Empty)
                                     .Set(y => y.Name, null)
                                     .Set(y => y.Age, 10)
                                     .Set(y => y.Timespan, new DateTime(2023, 04, 23)))
            .Where<TestClass>(x => x.Equal(y => y.Id, Guid.Empty).And()
                                    .Equal(y => y.Name, null).Or()
                                    .Equal(y => y.Age, 10).And()
                                    .Equal(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, builder.ToString());
    }

    [Theory]
    [InlineData(@"delete TestClass
where Id = '00000000-0000-0000-0000-000000000000' and Name = null or Age = 10 and Timespan = '2023-04-23T00:00:00.0000000'")]
    public void DeleteWhere_BuildUpdateWhereSql(string expected)
    {
        var builder = new MsQueryBuilder()
            .Delete<TestClass>()
            .Where<TestClass>(x => x.Equal(y => y.Id, Guid.Empty).And()
                                    .Equal(y => y.Name, null).Or()
                                    .Equal(y => y.Age, 10).And()
                                    .Equal(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, builder.ToString());
    }
    /*
    [Theory]
    [InlineData(@"delete TestClass
where Id = '00000000-0000-0000-0000-000000000000' and Name = null or Age = 10 and Timespan = '2023-04-23T00:00:00.0000000'")]
    public void UpdateJoinWhere_BuildUpdateJoinWhereSql(string expected)
    {
        var builder = new MsQueryBuilder()
            .Update<TestClass>(x => x.Set(y => y.Id, Guid.Empty)
                                    .Set(y => y.Name, null)
                                    .Set(y => y.Age, 10)
                                    .Set(y => y.Timespan, new DateTime(2023, 04, 23)))
            .Join<TestClass, TestClass2>();
        Assert.Equal(expected, builder.ToString());
    }
    */
}
