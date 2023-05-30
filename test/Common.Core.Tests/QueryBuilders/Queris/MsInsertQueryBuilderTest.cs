﻿using Common.Core.QueryBuilders.Queris;

namespace Common.Core.Tests.QueryBuilders.Queris;

public class MsInsertQueryBuilderTest
{
    [Theory]
    [InlineData("\r\ninsert into dbo.TestClass (Id,Timespan,Name,Age)\r\nvalues (@Id0,@Timespan1,@Name2,@Age3)")]
    public void Insert_BuildSql(string expected)
    {
        var source = new QueryBuilderSource();
        new MsInsertQueryBuilder<TestClass>(source)
            .Insert()
            .Values(y => y.Value(x => x.Id, Guid.Empty)
                          .Value(x => x.Timespan, new DateTime(2023, 04, 30))
                          .Value(x => x.Name, null)
                          .Value(x => x.Age, 10));
        Assert.Equal(expected, source.ToString());
    }
}
