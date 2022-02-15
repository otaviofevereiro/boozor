//using Boozor.Components.Common;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Components.Forms;
//using Microsoft.AspNetCore.Components.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;

//namespace Boozor.Components.Forms
//{
//    /// <summary>
//    /// Renders a form element that cascades an <see cref="EditContext"/> to descendants.
//    /// </summary>
//    public class EntityForm<TEntity> : ComponentBase
//    {
//        private readonly Func<Task> _handleSubmitDelegate;
//        private EditContext _fixedEditContext;

//        // Cache to avoid per-render allocations
//        /// <summary>
//        /// Constructs an instance of <see cref="EditForm"/>.
//        /// </summary>
//        public EntityForm()
//        {
//            _handleSubmitDelegate = HandleSubmitAsync;
//        }

//        /// <summary>
//        /// Gets or sets a collection of additional attributes that will be applied to the created <c>form</c> element.
//        /// </summary>
//        [Parameter(CaptureUnmatchedValues = true)]
//        public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

//        [Inject]
//        public AppState AppState { get; set; }

//        /// <summary>
//        /// Specifies the content to be rendered inside this <see cref="EditForm"/>.
//        /// </summary>
//        [Parameter]
//        public RenderFragment<EditContext> ChildContent { get; set; }

//        [Inject]
//        public HttpClient Client { get; set; }

//        /// <summary>
//        /// Supplies the edit context explicitly. If using this parameter, do not
//        /// also supply <see cref="Entity"/>, since the model value will be taken
//        /// from the <see cref="EditContext.Model"/> property.
//        /// </summary>
//        [Parameter]
//        public EditContext EditContext { get; set; }

//        /// <summary>
//        /// Specifies the top-level model object for the form. An edit context will
//        /// be constructed for this model. If using this parameter, do not also supply
//        /// a value for <see cref="EditContext"/>.
//        /// </summary>
//        [Parameter]
//        public TEntity Entity { get; set; }


//        [Parameter]
//        public string EntityId { get; set; }

//        /// <summary>
//        /// A callback that will be invoked when the form is submitted and the
//        /// <see cref="EditContext"/> is determined to be invalid.
//        /// </summary>
//        [Parameter]
//        public EventCallback<EditContext> OnInvalidSubmit { get; set; }

//        /// <summary>
//        /// A callback that will be invoked when the form is submitted.
//        ///
//        /// If using this parameter, you are responsible for triggering any validation
//        /// manually, e.g., by calling <see cref="EditContext.Validate"/>.
//        /// </summary>
//        [Parameter]
//        public EventCallback<EditContext> OnSubmit { get; set; }

//        /// <summary>
//        /// A callback that will be invoked when the form is submitted and the
//        /// <see cref="EditContext"/> is determined to be valid.
//        /// </summary>
//        [Parameter]
//        public EventCallback<EditContext> OnValidSubmit { get; set; }

//        [Parameter]
//        public string RequestUri { get; set; }

//        [Parameter]
//        public string Title { get; set; }

//        /// <inheritdoc />
//        protected override void BuildRenderTree(RenderTreeBuilder builder)
//        {
//            if (_fixedEditContext == null)
//                return;

//            builder.OpenRegion(_fixedEditContext.GetHashCode());
//            {
//                builder.OpenElement(0, "form");
//                builder.AddMultipleAttributes(1, AdditionalAttributes);
//                builder.AddAttribute(2, "onsubmit", _handleSubmitDelegate);
//                {
//                    RenderFluntValidation(builder);
//                    RenderAlert(builder);

//                    builder.OpenComponent<CascadingValue<EditContext>>(3);
//                    builder.AddAttribute(4, "IsFixed", true);
//                    builder.AddAttribute(5, "Value", _fixedEditContext);
//                    builder.AddAttribute(7, "ChildContent", ChildContent?.Invoke(_fixedEditContext));
//                    builder.CloseComponent();

//                    RenderToolbar(builder);
//                }
//                builder.CloseElement();
//            }
//            builder.CloseRegion();
//        }


//        protected override async Task OnAfterRenderAsync(bool firstRender)
//        {
//            if (firstRender)
//            {
//                if (EntityId is not null &&
//                    Entity.Id is not null)
//                {
//                    await GetEntity(EntityId);
//                    StateHasChanged();
//                }

//            }

//            await base.OnAfterRenderAsync(firstRender);
//        }

//        /// <inheritdoc />
//        protected override void OnParametersSet()
//        {
//            if ((EditContext == null) == (Entity == null))
//                throw new InvalidOperationException($"{nameof(EditForm)} requires a {nameof(Entity)} parameter, or an {nameof(EditContext)} parameter, but not both.");

//            if (OnSubmit.HasDelegate && (OnValidSubmit.HasDelegate || OnInvalidSubmit.HasDelegate))
//                throw new InvalidOperationException($"When supplying an {nameof(OnSubmit)} parameter to {nameof(EditForm)}, do not also supply {nameof(OnValidSubmit)} or {nameof(OnInvalidSubmit)}.");

//            if (_fixedEditContext == null || EditContext != null || Entity != _fixedEditContext.Model)
//            {
//                _fixedEditContext = EditContext ?? new EditContext(Entity!);
//            }
//        }

//        RenderFragment CreateAlert() => builder =>
//        {
//            builder.OpenComponent<Alert>(0);
//            builder.CloseComponent();
//        };


//        private async Task<HttpResponseMessage> Execute(Func<Task<HttpResponseMessage>> func)
//        {
//            AppState.SetLoading(true);

//            try
//            {
//                var response = await func.Invoke();

//                //TODO: Validar erro aqui
//                result = await response.Content.ReadFromJsonAsync<TEntity>();

//                //TODO: Validar a necessidade disso
//                if (result.IsValid)
//                    Entity.UpdateInstance(result.Item);

//                return response;
//            }
//            finally
//            {
//                AppState.SetLoading(false);
//            }
//        }

//        private async Task GetEntity(string entityId)
//        {
//            var response = await Execute(() => Client.GetAsync($"{RequestUri}\\{entityId}"));

//            if (!response.IsSuccessStatusCode &&
//                response.StatusCode == HttpStatusCode.NotFound)
//            {
//                result.AddAlert($"Not found {typeof(TEntity).GetDisplay()} with Id {entityId} ");
//            }
//        }

//        private async Task HandleSubmitAsync()
//        {
//            if (OnSubmit.HasDelegate)
//            {
//                await OnSubmit.InvokeAsync(_fixedEditContext);
//            }
//            else
//            {
//                var isValid = _fixedEditContext.Validate();

//                if (isValid)
//                {
//                    if (OnValidSubmit.HasDelegate)
//                        await OnValidSubmit.InvokeAsync(_fixedEditContext);
//                    else
//                        await HandleValidSubmit();
//                }

//                if (!isValid && OnInvalidSubmit.HasDelegate)
//                    await OnInvalidSubmit.InvokeAsync(_fixedEditContext);
//            }
//        }

//        private async Task HandleValidSubmit()
//        {
//            await Execute(() => Client.PutAsync(RequestUri, JsonContent.Create(Entity)));
//        }

//        private void RenderAlert(RenderTreeBuilder builder)
//        {
//            builder.OpenComponent<CascadingValue<IResult>>(3);
//            builder.AddAttribute(1, "Value", result);
//            builder.AddAttribute(2, "ChildContent", CreateAlert());
//            builder.CloseComponent();
//        }

//        private void RenderCancelButton(RenderTreeBuilder builder)
//        {
//            builder.OpenElement(3, "button");
//            builder.AddAttribute(0, "type", "button");
//            builder.AddAttribute(1, "class", "btn btn-light");
//            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, async () =>
//            {
//                if (Entity.Id is not null && !Entity.Id.Equals(default))
//                    await GetEntity(Entity.Id);
//                else
//                    Entity.UpdateInstance(Activator.CreateInstance<TEntity>());
//            }));

//            builder.AddContent(0, "Cancel");
//            builder.CloseElement();

//            builder.AddContent(4, " ");
//        }

//        private void RenderFluntValidation(RenderTreeBuilder builder)
//        {
//            builder.OpenComponent<CascadingValue<EditContext>>(4);
//            builder.AddAttribute(1, "Value", _fixedEditContext);
//            builder.AddAttribute(2, "ChildContent", CreateFluentValidation());
//            builder.CloseComponent();
//        }

//        private void RenderSubmitButton(RenderTreeBuilder builder)
//        {
//            builder.OpenElement(1, "button");
//            builder.AddAttribute(0, "type", "submit");
//            builder.AddAttribute(1, "class", "btn btn-primary");
//            builder.AddContent(0, "Submit");
//            builder.CloseElement();

//            builder.AddContent(2, " ");
//        }

//        private void RenderToolbar(RenderTreeBuilder builder)
//        {
//            builder.OpenElement(7, "div");
//            builder.AddAttribute(0, "class", "form-group row");
//            {
//                builder.OpenElement(8, "div");
//                builder.AddAttribute(0, "class", "offset-2 col-10");
//                {
//                    RenderSubmitButton(builder);
//                    RenderCancelButton(builder);
//                }
//                builder.CloseElement();
//            }
//            builder.CloseElement();
//        }
//    }
//}
