using Common.Core.Model;
using Xunit;

namespace Common.Core.Test.Model
{
    public class HasIdBaseTest
    {
        [Fact]
        public void HasIdBaseEquals()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 1 };

            bool entityEqual1 = entity1.Equals(entity2);
            bool entityEqual2 = entity2.Equals(entity1);

            Assert.True(entityEqual1);
            Assert.True(entityEqual2);
        }

        [Fact]
        public void HasIdBaseNotEquals()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 2 };

            bool entityEqual1 = entity1.Equals(entity2);
            bool entityEqual2 = entity2.Equals(entity1);

            Assert.False(entityEqual1);
            Assert.False(entityEqual2);
        }

        [Fact]
        public void HasIdBaseEqualTo()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 1 };

            bool entityEqual1 = entity1 == entity2;
            bool entityEqual2 = entity2 == entity1;

            Assert.True(entityEqual1);
            Assert.True(entityEqual2);
        }

        [Fact]
        public void HasIdBaseNotEqualTo()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 2 };

            bool entityEqual1 = entity1 == entity2;
            bool entityEqual2 = entity2 == entity1;

            Assert.False(entityEqual1);
            Assert.False(entityEqual2);
        }

        [Fact]
        public void HasIdBaseEqualHashCode()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 1 };

            bool entityEqual1 = entity1.GetHashCode() == entity2.GetHashCode();
            bool entityEqual2 = entity2.GetHashCode() == entity1.GetHashCode();

            Assert.True(entityEqual1);
            Assert.True(entityEqual2);
        }

        [Fact]
        public void HasIdBaseNotEqualHashCode()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 2 };

            bool entityEqual1 = entity1.GetHashCode() == entity2.GetHashCode();
            bool entityEqual2 = entity2.GetHashCode() == entity1.GetHashCode();

            Assert.False(entityEqual1);
            Assert.False(entityEqual2);
        }
    }

    public class EntityTest : HasIdBase<int> { }
}
