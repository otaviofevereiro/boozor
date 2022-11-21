using System.Collections.Concurrent;

namespace Boozor.Data;

//TODO:send to DevPack
public sealed class AsyncValueFactory<TKey, TValue>
    where TKey : notnull
{
    private readonly ConcurrentDictionary<TKey, Lazy<ValueTask<TValue>>> _values = new();
    private readonly Func<TKey, ValueTask<TValue>> _createAsync;

    public AsyncValueFactory(Func<TKey, ValueTask<TValue>> factory)
    {
        _createAsync = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public ValueTask<TValue> GetOrCreateAsync(TKey key)
    {
        var lazy = _values.GetOrAdd(key, new Lazy<ValueTask<TValue>>(async () => await _createAsync(key)));

        return lazy.Value;
    }
}
