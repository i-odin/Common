using System.Text;
using Common.Core.QueryBuilders.Translator;

namespace Common.Core.Tests.QueryBuilders.Translator;

public class WhereTranslatorTest
{
    [Theory]
    [InlineData("where Id = '00000000-0000-0000-0000-000000000000' and Name = null and Age = 10 or Timespan = '2023-04-23T00:00:00.0000000'")]
    public void Where_BuildSql(string expected)
    {
        var builder = (IWhereTranslator<TestClass>)new WhereTranslator<TestClass>(new StringBuilder()).Where();
        builder.Equal(y => y.Id, Guid.Empty).And()
               .Equal(y => y.Name, null).And()
               .Equal(y => y.Age, 10).Or()
               .Equal(y => y.Timespan, new DateTime(2023, 04, 23));
            
        Assert.Equal(expected, builder.ToString());
    }

    [Theory]
    [InlineData("where (Id = '00000000-0000-0000-0000-000000000000' and Name <> null) or (Age = 10 and Timespan <> '2023-04-23T00:00:00.0000000')")]
    public void WhereBracket_BuildSql(string expected)
    {
        var builder = (IWhereTranslator<TestClass>)new WhereTranslator<TestClass>(new StringBuilder()).Where();
        builder.Bracket(y => y.Equal(y => y.Id, Guid.Empty)
                              .And()
               .NotEqual(y => y.Name, null))
                              .Or()
               .Bracket(y => y.Equal(y => y.Age, 10)
                              .And()
               .NotEqual(y => y.Timespan, new DateTime(2023, 04, 23)));

        Assert.Equal(expected, builder.ToString());
    }
}
