using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;

namespace Example.Server.Controllers;

[ApiController]
[Route("[controller]")]
//:TODO extract to lib
// Configure AddBoozor with default assembly
public class EntitiesController : ControllerBase
{
    private readonly BoozorContext _boozorContext;
    private readonly ILogger<EntitiesController> _logger;

    public EntitiesController(BoozorContext boozorContext, ILogger<EntitiesController> logger)
    {
        _boozorContext = boozorContext;
        _logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        return "Hello World!";
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromHeader] string type)
    {
        Type entityType = _boozorContext.GetModelType(type);
        var entity = await DeserializeAsync(Request.Body, entityType);

        if (!TryValidateModel(entity, entityType.Name))
            return ValidationProblem();

        return Ok();
    }

    private async Task<IValidatableObject> DeserializeAsync(Stream utf8Json, Type type)
    {
        var value = await JsonSerializer.DeserializeAsync(utf8Json, type) ?? throw new InvalidOperationException();

        return (IValidatableObject)value;
    }
}
