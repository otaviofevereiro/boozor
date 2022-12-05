using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boozor.Shared;

namespace Boozor.Components.Forms;

public class EntityServer : ComponentBase
{
    private const string _entitiesPath = "api/entities";

    private Uri _entitiesUri;

    private ValidationMessageStore _messages;

    [Inject]
    public HttpClient HttpClient { get; set; }

    [CascadingParameter]
    public EditContext CurrentEditContext { get; set; }

    [Parameter]
    public EventCallback<EditContext> OnSubmit { get; set; }

    public async Task SubmitAsync<TEntity>(TEntity entity)
        where TEntity : IEntity
    {
        HttpRequestMessage request = new();

        request.RequestUri = _entitiesUri;
        request.Headers.Add("type", typeof(TEntity).FullName);
        request.Method = entity.Id is null ? HttpMethod.Post : HttpMethod.Put;
        request.Content = JsonContent.Create(entity);

        var result = await HttpClient.SendAsync(request);

        await Validate(result);
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (this.CurrentEditContext is null)
        {
            throw new InvalidOperationException($"{nameof(EntityServer)} requires a cascading " +
                $"parameter of type {nameof(EditContext)}. For example, you can use {nameof(EntityServer)} " +
                $"inside an EditForm.");
        }

        _messages = new(this.CurrentEditContext);
        _entitiesUri = GetEntitiesUri();

        CurrentEditContext.OnValidationRequested += (s, e) => _messages?.Clear();
        CurrentEditContext.OnFieldChanged += (s, e) => _messages?.Clear(e.FieldIdentifier);        
    }

    private async Task<bool> Validate(HttpResponseMessage response)
    {
        if (response.StatusCode is not HttpStatusCode.BadRequest)
        {
            response.EnsureSuccessStatusCode();

            return true;
        };

        var validationProblemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        if (validationProblemDetails.Errors is null)
            return false;

        _messages.Clear();

        foreach (var error in validationProblemDetails.Errors)
            _messages?.Add(CurrentEditContext.Field(error.Key), error.Value);

        CurrentEditContext.NotifyValidationStateChanged();

        return false;
    }

    private Uri GetEntitiesUri()
    {
        var uriBuilder = new UriBuilder(HttpClient.BaseAddress);
        uriBuilder.Path = _entitiesPath;

        return uriBuilder.Uri;
    }
}

file sealed record ValidationProblemDetails
{

    public int? Status { get; set; }


    public string Title { get; set; }

    public string Type { get; set; }

    public IDictionary<string, string[]> Errors { get; set; }
}