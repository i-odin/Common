using System.Text;
using Common.Core.QueryBuilders.Query;

namespace Common.Core.Tests.SqlBuilder.Query;

/*
     UPDATE Geeks1  
SET col2 = Geeks2.col2,  
col3 = Geeks2.col3  
FROM Geeks1  
INNER JOIN Geeks2 ON Geeks1.col1 = Geeks2.col1  
WHERE Geeks1.col1 IN (21, 31);
     */

public class UpdateQueryBuilderTest
{
    [Theory]
    [InlineData(@"update TestClass
set Id = '00000000-0000-0000-0000-000000000000', Name = null, Age = 10, Timespan = '2023-04-23T00:00:00.0000000'")]

    public void Update_BuildUpdateSql(string expected)
    {
        var builder = new UpdateQueryBuilder(new StringBuilder())
            .Update<TestClass>(x => x.Set(y => y.Id, Guid.Empty)
                                    .Set(y => y.Name, null)
                                    .Set(y => y.Age, 10)
                                    .Set(y => y.Timespan, new DateTime(2023, 04, 23)));
        Assert.Equal(expected, builder.ToString());
    }
}
