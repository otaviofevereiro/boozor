using System.ComponentModel.DataAnnotations;

namespace Boozor.Shared.Tests
{

    public class EntityExtensionsTest
    {
        [Fact]
        public void GetContainerId_MainDomain_ExpectedCorretName()
        {
            var containerId = EntityExtensions.GetContainerId(typeof(EntityTest));

            Assert.Equal(containerId, "EntityTest");
        }

        [Fact]
        public void GetContainerId_SubDomain_ExpectedCorretName()
        {
            var containerId = EntityExtensions.GetContainerId(typeof(Custom.SubDomainEntityTest));

            Assert.Equal(containerId, "Custom.SubDomainEntityTest");
        }
    }
}

namespace Boozor.Shared.Tests
{
    public class EntityTest : IEntity
    {
        public string? Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}

namespace Boozor.Shared.Tests.Custom
{
    public class SubDomainEntityTest : IEntity
    {
        public string? Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}