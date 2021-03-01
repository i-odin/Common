using Common.Core.Models;
using Xunit;

namespace Common.Core.Test.Models
{
    public class HasIdBaseTests
    {
        [Fact]
        public void Equals_CompareTwoObjects_ReturnTrue()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 1 };

            bool entityEqual1 = entity1.Equals(entity2);
            bool entityEqual2 = entity2.Equals(entity1);
            bool entityEqual3 = entity1 == entity2;
            bool entityEqual4 = entity2 == entity1;

            Assert.True(entityEqual1);
            Assert.True(entityEqual2);
            Assert.True(entityEqual3);
            Assert.True(entityEqual4);
        }

        [Fact]
        public void Equals_CompereTwoObjects_ReturnFalse()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 2 };

            bool entityEqual1 = entity1.Equals(entity2);
            bool entityEqual2 = entity2.Equals(entity1);
            bool entityEqual3 = entity1 != entity2;
            bool entityEqual4 = entity2 != entity1;

            Assert.False(entityEqual1);
            Assert.False(entityEqual2);
            Assert.True(entityEqual3);
            Assert.True(entityEqual4);
        }
        
        [Fact]
        public void GetHashCode_CompareTwoObjects_ReturnTrue()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 1 };

            bool entityEqual1 = entity1.GetHashCode() == entity2.GetHashCode();
            bool entityEqual2 = entity2.GetHashCode() == entity1.GetHashCode();

            Assert.True(entityEqual1);
            Assert.True(entityEqual2);
        }

        [Fact]
        public void GetHashCode_CompareTwoObjects_ReturnFalse()
        {
            var entity1 = new EntityTest { Id = 1 };
            var entity2 = new EntityTest { Id = 2 };

            bool entityEqual1 = entity1.GetHashCode() == entity2.GetHashCode();
            bool entityEqual2 = entity2.GetHashCode() == entity1.GetHashCode();

            Assert.False(entityEqual1);
            Assert.False(entityEqual2);
        }

        private class EntityTest : HasId<int> { }
    }
}
