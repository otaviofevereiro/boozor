using Curriculum.Api.Common;
using Curriculum.Shared.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Curriculum.Client.Shared
{
    /// <summary>
    /// Renders a form element that cascades an <see cref="EditContext"/> to descendants.
    /// </summary>
    public class EntityForm<TEntity> : ComponentBase
        where TEntity : Entity
    {
        private readonly Func<Task> _handleSubmitDelegate;
        private EditContext? _fixedEditContext;
        private bool loading;

        Result<TEntity> result = new Result<TEntity>();

        // Cache to avoid per-render allocations
        /// <summary>
        /// Constructs an instance of <see cref="EditForm"/>.
        /// </summary>
        public EntityForm()
        {
            _handleSubmitDelegate = HandleSubmitAsync;
        }

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created <c>form</c> element.
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// Specifies the content to be rendered inside this <see cref="EditForm"/>.
        /// </summary>
        [Parameter] public RenderFragment<EditContext>? ChildContent { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        /// <summary>
        /// Supplies the edit context explicitly. If using this parameter, do not
        /// also supply <see cref="Model"/>, since the model value will be taken
        /// from the <see cref="EditContext.Model"/> property.
        /// </summary>
        [Parameter] public EditContext? EditContext { get; set; }

        [Parameter]
        public int? EntityId { get; set; }

        /// <summary>
        /// Specifies the top-level model object for the form. An edit context will
        /// be constructed for this model. If using this parameter, do not also supply
        /// a value for <see cref="EditContext"/>.
        /// </summary>
        [Parameter] public TEntity Model { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be invalid.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnInvalidSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted.
        ///
        /// If using this parameter, you are responsible for triggering any validation
        /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnSubmit { get; set; }

        /// <summary>
        /// A callback that will be invoked when the form is submitted and the
        /// <see cref="EditContext"/> is determined to be valid.
        /// </summary>
        [Parameter] public EventCallback<EditContext> OnValidSubmit { get; set; }

        [Parameter]
        public string RequestUri { get; set; }

        [Parameter]
        public string Title { get; set; }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenRegion(_fixedEditContext.GetHashCode());
            {
                builder.OpenElement(0, "form");
                builder.AddMultipleAttributes(1, AdditionalAttributes);
                builder.AddAttribute(2, "onsubmit", _handleSubmitDelegate);
                {
                    RenderFluntValidation(builder);
                    RenderAlert(builder);

                    builder.OpenComponent<CascadingValue<EditContext>>(3);
                    builder.AddAttribute(4, "IsFixed", true);
                    builder.AddAttribute(5, "Value", _fixedEditContext);
                    builder.AddAttribute(7, "ChildContent", ChildContent?.Invoke(_fixedEditContext));
                    builder.CloseComponent();



                    RenderSubmitButton(builder);
                }
                builder.CloseElement();

                RenderSpinner(builder);
            }
            builder.CloseRegion();
        }

        protected override async Task OnInitializedAsync()
        {
            if (EntityId > 0)
                await GetEntity();

            await base.OnInitializedAsync();
        }

        /// <inheritdoc />
        protected override void OnParametersSet()
        {
            if ((EditContext == null) == (Model == null))
                throw new InvalidOperationException($"{nameof(EditForm)} requires a {nameof(Model)} parameter, or an {nameof(EditContext)} parameter, but not both.");

            if (OnSubmit.HasDelegate && (OnValidSubmit.HasDelegate || OnInvalidSubmit.HasDelegate))
                throw new InvalidOperationException($"When supplying an {nameof(OnSubmit)} parameter to {nameof(EditForm)}, do not also supply {nameof(OnValidSubmit)} or {nameof(OnInvalidSubmit)}.");

            if (_fixedEditContext == null || EditContext != null || Model != _fixedEditContext.Model)
                _fixedEditContext = EditContext ?? new EditContext(Model!);
        }

        private static void RenderSubmitButton(RenderTreeBuilder builder)
        {
            builder.OpenElement(7, "div");
            builder.AddAttribute(0, "class", "form-group row");
            {
                builder.OpenElement(8, "div");
                builder.AddAttribute(0, "class", "offset-2 col-10");
                {
                    builder.OpenElement(9, "button");
                    builder.AddAttribute(0, "type", "submit");
                    builder.AddAttribute(1, "class", "btn btn-primary");
                    builder.AddContent(0, "Submit");
                    builder.CloseElement();
                }
                builder.CloseElement();
            }
            builder.CloseElement();
        }

        RenderFragment CreateAlert() => builder =>
        {
            builder.OpenComponent<Alert>(0);
            builder.CloseComponent();
        };

        RenderFragment CreateFluentValidation() => builder =>
        {
            builder.OpenComponent<FluentValidation>(0);
            builder.CloseComponent();
        };

        RenderFragment CreateSpinner() => builder =>
        {
            builder.OpenComponent<Spinner>(0);
            builder.CloseComponent();
        };

        private async Task Execute(Func<Task<HttpResponseMessage>> func)
        {
            loading = true;

            try
            {
                var response = await func.Invoke();
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadFromJsonAsync<Result<TEntity>>();

                if (result.IsValid)
                {
                    Model = result.Item;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                loading = false;
            }
        }

        private async Task GetEntity()
        {
            await Execute(() => Client.GetAsync($"{RequestUri}\\{EntityId}"));
        }

        private async Task HandleSubmitAsync()
        {
            if (OnSubmit.HasDelegate)
            {
                await OnSubmit.InvokeAsync(_fixedEditContext);
            }
            else
            {
                var isValid = _fixedEditContext.Validate();

                if (isValid)
                {
                    if (OnValidSubmit.HasDelegate)
                        await OnValidSubmit.InvokeAsync(_fixedEditContext);
                    else
                        await HandleValidSubmit();
                }

                if (!isValid && OnInvalidSubmit.HasDelegate)
                    await OnInvalidSubmit.InvokeAsync(_fixedEditContext);
            }
        }

        private async Task HandleValidSubmit()
        {
            if (Model.Id > 0)
                await Execute(() => Client.PutAsync(RequestUri, JsonContent.Create(Model)));
            else
                await Execute(() => Client.PostAsync(RequestUri, JsonContent.Create(Model)));
        }

        private void RenderAlert(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<IResult>>(3);
            builder.AddAttribute(0, "IsFixed", true);
            builder.AddAttribute(1, "Value", result);
            builder.AddAttribute(2, "ChildContent", CreateAlert());
            builder.CloseComponent();
        }

        private void RenderFluntValidation(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<EditContext>>(4);
            builder.AddAttribute(0, "IsFixed", true);
            builder.AddAttribute(1, "Value", _fixedEditContext);
            builder.AddAttribute(2, "ChildContent", CreateFluentValidation());
            builder.CloseComponent();
        }

        private void RenderSpinner(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<bool>>(5);
            builder.AddAttribute(0, "IsFixed", true);
            builder.AddAttribute(1, "Value", loading);
            builder.AddAttribute(2, "ChildContent", CreateSpinner());
            builder.CloseComponent();
        }
    }
}
