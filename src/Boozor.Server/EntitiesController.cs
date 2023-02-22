using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Boozor.Data;
using Boozor.Shared;

namespace Boozor.Server;

[ApiController]
[Route("api/entities")]
public sealed class EntitiesController : ControllerBase
{
    private static readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly BoozorContext boozorContext;
    private readonly ILogger<EntitiesController> logger;

    private readonly IRepository repository;

    public EntitiesController(BoozorContext boozorContext,
                              IRepository repository,
                              ILogger<EntitiesController> logger)
    {
        this.boozorContext = boozorContext;
        this.repository = repository;
        this.logger = logger;
    }

    [HttpGet]
    public string Get()
    {
        return "Hello World!";
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var entityType = GetEntityType();
        var entity = await repository.GetAsync<object>(entityType, id);

        if (entity is null)
            return NotFound(new { Id = id });

        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Post(CancellationToken cancellationToken = default)
    {
        Type entityType = GetEntityType();
        var entity = await DeserializeAsync(Request.Body, entityType);

        if (!Validate(entity))
            return ValidationProblem();

        await repository.CreateAsync(entityType, entity, cancellationToken);

        return CreatedAtAction(nameof(Get), new { id = entity.Id }, new { id = entity.Id });
    }

    [HttpPut]
    public async Task<IActionResult> Put()
    {
        Type entityType = GetEntityType();
        var entity = await DeserializeAsync(Request.Body, entityType);

        if (!Validate(entity))
            return ValidationProblem();

        await repository.UpdateAsync(entityType, entity);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
    {
        var entityType = GetEntityType();

        await repository.DeleteAsync(entityType, id, cancellationToken);

        return Ok();
    }

    private bool Validate(IValidatableObject entity)
    {
        ModelState.Clear();

        return TryValidateModel(entity);
    }

    private Type GetEntityType()
    {
        string? type = this.Request.Headers["type"];

        if (type is null)
            throw new InvalidOperationException("The header 'type' is required.");

        return boozorContext.GetModelType(type);
    }

    private async Task<IValidatableEntity> DeserializeAsync(Stream utf8Json, Type type)
    {
        var value = await JsonSerializer.DeserializeAsync(utf8Json, type, jsonOptions) ?? throw new InvalidOperationException();

        return (IValidatableEntity)value;
    }
}
