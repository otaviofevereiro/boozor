using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Boozor.Components.Tables
{
    public class Column<TModel> : ComponentBase
    {
        //[Parameter(CaptureUnmatchedValues = true)]
        //public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

        [CascadingParameter(Name = "DataTableContext")]
        public DataTableContext<TModel> DataTableContext { get; set; }

        [Parameter]
        public string Format { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public Expression<Func<TModel, object>> ValueExpression { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            DataTableContext.Render(builder, this);
        }
    }
}
