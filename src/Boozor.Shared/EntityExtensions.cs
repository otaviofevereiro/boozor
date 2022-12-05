namespace Boozor.Shared;

public class EntityExtensions
{
    public static string GetContainerId<TEntity>()
        where TEntity : IEntity
    {
        var type = typeof(TEntity);

        return type.FullName!.Replace(type.Assembly.FullName!, "");
    }
}
