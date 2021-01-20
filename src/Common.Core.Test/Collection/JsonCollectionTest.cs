using Common.Core.Collection;
using Common.Core.Model;

namespace Common.Core.Test.Collection
{
    public class JsonCollectionTest
    {
    }

    public class EntityTest : HasIdBase<int> { }
    public class EntityCollectionTest : JsonCollection<EntityTest>
    {
        public override string Path { get; }
    }
}
