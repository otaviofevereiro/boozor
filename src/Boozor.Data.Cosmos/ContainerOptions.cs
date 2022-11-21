
namespace Boozor.Data;

public record ContainerOptions
{
    public required string Id { get; init; }
    public string PartitionKeyPath { get; init; } = "/id";
}