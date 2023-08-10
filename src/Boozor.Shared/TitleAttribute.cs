namespace Boozor.Shared;

public class TitleAttribute : Attribute
{
    public TitleAttribute(string title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
    }

    public string Title { get; }

    public static string GetTitle<T>()
    {
        var entityType = typeof(T);
        var attribute = Attribute.GetCustomAttribute(entityType, typeof(TitleAttribute));

        if (attribute is TitleAttribute titleAttribute)
            return titleAttribute.Title;

        return entityType.Name;
    }
}
