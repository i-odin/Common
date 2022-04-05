using Common.Core.Models;

namespace Common.Core.Tests.Models;

public class MapperTest
{
    [Fact]
    public void MapperMap_TypeToType_ReturnTrue()
    {
        var entityOne = new EntityOne { Id = 1 };

        var result = entityOne.Map();
        
        Assert.Equal(expected: "1", actual: result.Id);
        Assert.Equal(expected: typeof(EntityTwo), result.GetType());
    }

    public class EntityOne : IMapper<EntityTwo>
    {
        public int Id { get; set; }

        public EntityTwo Map()
        {
            return new EntityTwo { Id = Id.ToString() };
        }
    }

    public class EntityTwo
    {
        public string Id { get; set; }
    }
}