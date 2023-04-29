using Common.Core.QueryBuilder;

namespace Common.Core.Tests.SqlBuilder.Ms
{
    public class MsSqlBuilderDeleteTest
    {
        [Theory]
        [InlineData("delete TestClass")]
        public void Delete_BuildDeleteSql(string expected)
        {
            var builder = new MsSqlQueryBuilder().Delete<TestClass>();
                
            Assert.Equal(expected, builder.ToString());
        }
    }
}
