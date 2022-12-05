namespace Boozor.Shared;

public class TitleAttribute : Attribute
{
    public TitleAttribute(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public string Title { get; }
}
