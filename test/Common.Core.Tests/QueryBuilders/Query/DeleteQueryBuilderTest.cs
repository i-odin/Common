using System.Text;
using Common.Core.QueryBuilders.Query;

namespace Common.Core.Tests.QueryBuilders.Query;

public class DeleteQueryBuilderTest
{
    [Theory]
    [InlineData("delete TestClass")]
    public void Delete_BuildDeleteSql(string expected)
    {
        var builder = new DeleteQueryBuilder<TestClass>(new StringBuilder()).Delete();

        Assert.Equal(expected, builder.ToString());
    }
}