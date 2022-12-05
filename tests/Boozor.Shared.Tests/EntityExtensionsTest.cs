using System.ComponentModel.DataAnnotations;

namespace Boozor.Shared.Tests;

public class EntityExtensionsTest
{
    [Fact]
    public void Test1()
    {
       var containerId = EntityExtensions.GetContainerId<EntityTest>();

       Assert.Equal(containerId, "EntityTest");
    }
}

file class EntityTest : IEntity
{
    public static string Title => throw new NotImplementedException();

    public string? Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        throw new NotImplementedException();
    }
}