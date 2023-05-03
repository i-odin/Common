using System.Text;
using Common.Core.QueryBuilders.Query;

namespace Common.Core.Tests.QueryBuilders.Query;

public class InsertQueryBuilderTest
{
    /*[Theory]
    [InlineData("insert into TestClass (Id, Timespan, Name, Age)\r\nvalues ('00000000-0000-0000-0000-000000000000', '2023-04-30T00:00:00.0000000', null, 10)")]
    public void Insert_BuildSql(string expected)
    {
        var builder = new InsertQueryBuilder<TestClass>(new StringBuilder())
            .Insert(x => x.Values(y => y.Id, Guid.Empty)
                          .Values(y => y.Timespan, new DateTime(2023, 04, 30))
                          .Values(y => y.Name, null)
                          .Values(y => y.Age, 10));

        Assert.Equal(expected, builder.ToString());
    }*/
}
