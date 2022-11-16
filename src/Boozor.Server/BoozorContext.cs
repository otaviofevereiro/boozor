using System.Reflection;

namespace Boozor.Server;

public sealed class BoozorContext
{
    private readonly Assembly _modelAssembly;

    public BoozorContext(Assembly modelAssembly)
    {
        _modelAssembly = modelAssembly;
    }

    public Type GetModelType(string typeName)
    {
        return _modelAssembly.GetType(typeName) ?? throw new InvalidOperationException($"Type {typeName} not found."); ;
    }
}