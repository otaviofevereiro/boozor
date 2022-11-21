namespace Boozor.Shared;

public record Id
{
    public Id(string value)
    {
        Value = value;
    }

    public static implicit operator Id(string id) => new Id(id);


    public static implicit operator string(Id id) => id.Value;

    public string Value { get; set; }

    public static Id Create()
    {
        return new(Guid.NewGuid().ToString());
    }
}