namespace Boozor;

public static class EntityExtensions
{
    public static string GetContainerId(this Type type)
    {
        return type.FullName!.Replace($"{type.Assembly.GetName().Name}."!, "");
    }
}
