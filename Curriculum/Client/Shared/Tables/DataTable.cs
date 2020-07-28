using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;

namespace Curriculum.Client.Shared
{
    public class DataTable<TModel> : ComponentBase, IDisposable
    {
        private bool disposedValue;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public IEnumerable<TModel> Itens { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Itens == null)
                return;

            builder.OpenElement(0, "table");
            builder.AddAttribute(1, "class", "table");
            {
                builder.OpenElement(2, "thead");
                {
                    builder.OpenElement(3, "tr");
                    {
                        builder.OpenComponent<CascadingValue<DataTableContext<TModel>>>(4);
                        builder.AddAttribute(5, "Value", new TitleContext<TModel>(Itens));
                        builder.AddAttribute(6, "ChildContent", ChildContent);
                        builder.CloseComponent();
                    }
                    builder.CloseElement();
                }
                builder.CloseElement();

                builder.OpenElement(8, "tbody");
                {
                    foreach (var item in Itens)
                    {
                        builder.OpenElement(9, "tr");
                        {
                            builder.OpenComponent<CascadingValue<DataTableContext<TModel>>>(10);
                            builder.AddAttribute(11, "Value", new RowContext<TModel>(item));
                            builder.AddAttribute(12, "ChildContent", ChildContent);
                            builder.CloseComponent();
                        }
                        builder.CloseElement();
                    }
                }
                builder.CloseElement();

            }
            builder.CloseElement();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)

                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                ChildContent = null;
                Itens = null;
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DataTable()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
