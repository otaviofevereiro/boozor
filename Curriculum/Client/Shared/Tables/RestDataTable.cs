using Curriculum.Api.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Curriculum.Client.Shared
{
    public class RestDataTable<TModel> : DataTable<TModel>
        where TModel : class
    {
        private Result<IReadOnlyCollection<TModel>> result = new Result<IReadOnlyCollection<TModel>>();

        [Inject]
        public HttpClient Client { get; set; }

        [Parameter]
        public string RequestUri { get; set; }

        [Inject]
        public AppState AppState { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            RenderAlert(builder);
            base.BuildRenderTree(builder);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await GetItens();
        }

        RenderFragment CreateAlert() => builder =>
        {
            builder.OpenComponent<Alert>(0);
            builder.CloseComponent();
        };

        private async Task GetItens()
        {
            AppState.SetLoading(true);

            try
            {
                var response = await Client.GetAsync(RequestUri);
                response.EnsureSuccessStatusCode();

                result = await response.Content.ReadFromJsonAsync<Result<IReadOnlyCollection<TModel>>>();

                if (result.IsValid)
                    Itens = result.Item;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                AppState.SetLoading(false);
            }
        }

        private void RenderAlert(RenderTreeBuilder builder)
        {
            builder.OpenComponent<CascadingValue<IResult>>(3);
            builder.AddAttribute(0, "IsFixed", true);
            builder.AddAttribute(1, "Value", result);
            builder.AddAttribute(2, "ChildContent", CreateAlert());
            builder.CloseComponent();
        }
    }
}
