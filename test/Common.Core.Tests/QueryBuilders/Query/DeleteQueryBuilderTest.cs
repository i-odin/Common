using System.Text;
using Common.Core.QueryBuilders;
using Common.Core.QueryBuilders.Query;

namespace Common.Core.Tests.QueryBuilders.Query;

public class DeleteQueryBuilderTest
{
    [Theory]
    [InlineData("delete TestClass")]
    public void Delete_BuildSql(string expected)
    {
        var builder = new DeleteQueryBuilder<TestClass>(new StringBuilder()).Delete();
        Assert.Equal(expected, builder.ToString());
    }

    [Theory]
    [InlineData(@"delete TestClass
where Id = '00000000-0000-0000-0000-000000000000' and Name = null or Age = 10 and Timespan = '2023-04-23T00:00:00.0000000'")]
    public void DeleteWhere_BuildSql(string expected)
    {
        var builder = ((IDeleteQueryBuilder<TestClass>)new DeleteQueryBuilder<TestClass>(new StringBuilder()))
            .Delete()
            .Where(x => x.Equal(y => y.Id, Guid.Empty).And()
                                    .Equal(y => y.Name, null).Or()
                                    .Equal(y => y.Age, 10).And()
                                    .Equal(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, builder.ToString());
    }
}