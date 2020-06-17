using Curriculum.Api.Common;
using Curriculum.Shared.Base;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Curriculum.Client.Shared
{
    public partial class EntityForm<TEntity>
        where TEntity : Entity
    {
        bool loading;

        Result<TEntity> result = new Result<TEntity>();

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

        [Parameter]
        public TEntity Entity { get; set; }

        [Parameter]
        public EventCallback<TEntity> EntityChanged { get; set; }

        [Parameter]
        public int? EntityId { get; set; }

        [Parameter]
        public string RequestUri { get; set; }

        [Parameter]
        public string Title { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (EntityId > 0)
                await GetEntity();

            await base.OnInitializedAsync();
        }

        private async Task Execute(Func<Task<HttpResponseMessage>> func)
        {
            loading = true;

            try
            {
                var response = await func.Invoke();
                result = await response.Content.ReadFromJsonAsync<Result<TEntity>>();

                if (result.IsValid)
                {
                    Entity = result.Item;

                    await EntityChanged.InvokeAsync(Entity);
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

        private async Task HandleValidSubmit()
        {
            if (Entity.Id > 0)
                await Execute(() => Client.PutAsync(RequestUri, JsonContent.Create(Entity)));
            else
                await Execute(() => Client.PostAsync(RequestUri, JsonContent.Create(Entity)));
        }
    }
}
