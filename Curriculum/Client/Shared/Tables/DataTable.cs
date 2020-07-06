using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Generic;

namespace Curriculum.Client.Shared
{
    public class DataTable<TModel> : ComponentBase //, IDisposable
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public IEnumerable<TModel> Itens { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
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
    }
}
