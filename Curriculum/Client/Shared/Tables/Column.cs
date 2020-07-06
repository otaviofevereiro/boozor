using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Curriculum.Client.Shared
{
    public class Column<TModel> : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        [Parameter]
        public string Title { get; set; }

        [CascadingParameter]
        public DataTableContext<TModel> DataTableContext { get; set; }

        [Parameter]
        public Expression<Func<TModel, object>> ValueExpression { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "th");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddContent(2, DataTableContext.GetValue(ValueExpression));
            builder.CloseElement();
        }
    }
}
