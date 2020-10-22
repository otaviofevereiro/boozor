using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Boozor.Components.Tables
{
    public class DataTable<TModel> : ComponentBase, IDisposable
        where TModel : class
    {
        private bool disposedValue;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public List<TModel> Items { get; set; }

        [Parameter]
        public EventCallback<List<TModel>> ItemsChanged { get; set; }

        [Parameter]
        public bool Multiselect { get; set; } = false;

        [Parameter]
        public TModel SelectedItem { get; set; }

        [Parameter]
        public EventCallback<TModel> SelectedItemChanged { get; set; }


        private readonly List<TModel> selectedItens = new List<TModel>();
        public IReadOnlyCollection<TModel> SelectedItens => selectedItens;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Items == null)
                return;

            builder.OpenElement(1, "table");
            builder.AddAttribute(2, "class", "table table-striped");
            {
                builder.OpenElement(2, "thead");
                {
                    builder.OpenElement(3, "tr");
                    {
                        var titleContext = new TitleContext<TModel>();
                        titleContext.OnClickEvent += TitleContext_OnClickEvent;

                        builder.OpenComponent<CascadingValue<DataTableContext<TModel>>>(4);
                        builder.AddAttribute(0, "Value", titleContext);
                        builder.AddAttribute(1, "ChildContent", ChildContent);
                        builder.AddAttribute(2, "Name", "DataTableContext");
                        builder.CloseComponent();
                    }
                    builder.CloseElement();
                }
                builder.CloseElement();


                builder.OpenElement(8, "tbody");
                {
                    foreach (var item in Items)
                    {
                        builder.OpenElement(9, "tr");
                        {
                            builder.AddAttribute(1, "onclick", EventCallback.Factory.Create(this, () => OnRowClick(item)));

                            if (selectedItens.Contains(item))
                                builder.AddAttribute(0, "class", "table-primary");

                            builder.OpenComponent<CascadingValue<DataTableContext<TModel>>>(10);
                            builder.AddAttribute(0, "Value", new RowContext<TModel>(item));
                            builder.AddAttribute(1, "ChildContent", ChildContent);
                            builder.AddAttribute(2, "Name", "DataTableContext");
                            builder.CloseComponent();
                        }
                        builder.CloseElement();
                    }
                }
                builder.CloseElement();
            }
            builder.CloseElement();
        }

        private void TitleContext_OnClickEvent(System.Linq.Expressions.Expression<Func<TModel, object>> valueExpression)
        {
            Items = Items.OrderBy(valueExpression.Compile()).ToList();
            StateHasChanged();
        }

        public async Task OnRowClick(TModel item)
        {
            Debug.WriteLine($" Item Selecionado - {item}");

            if (Multiselect)
            {
                if (selectedItens.Remove(item))
                {
                    await OnSelectedItemChange(Activator.CreateInstance<TModel>());
                    return;
                }
            }
            else
            {
                await OnSelectedItemChange(Activator.CreateInstance<TModel>());
                selectedItens.Clear();
            }

            selectedItens.Add(item);
            await OnSelectedItemChange(item);
        }

        private async Task OnSelectedItemChange(TModel item)
        {
            SelectedItem = item;

            if (SelectedItemChanged.HasDelegate)
                await SelectedItemChanged.InvokeAsync(item);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                ChildContent = null;
                Items = null;
                disposedValue = true;
            }
        }

        ~DataTable()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
