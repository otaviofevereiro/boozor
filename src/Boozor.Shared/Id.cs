namespace Boozor.Shared;

public static class Id
{
    public static string Create()
    {
        return Guid.NewGuid().ToString();
    }
}