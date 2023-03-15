using System.Text.Json;

namespace System;

public static class CommonExtensions
{
    public static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };

    public static string GetId(this HttpResponseMessage response)
    {
        var content = JsonSerializer.Deserialize<JsonElement>(response.Content.ReadAsStream(), Options);

        Assert.NotNull(content);

        var id = content.GetProperty("id").GetString();

        Assert.NotNull(id);
        Assert.NotEmpty(id);
        Assert.True(Guid.TryParse(id, out Guid _));

        return id;
    }
}
