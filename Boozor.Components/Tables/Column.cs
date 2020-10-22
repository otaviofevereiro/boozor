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
        [Parameter(CaptureUnmatchedValues = true)]
        public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

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
            object objValue = DataTableContext.GetValue(ValueExpression);
            string formatedValue = FormatValue(objValue);

            builder.OpenElement(0, "th");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => OnClickTableHeader()));
            builder.AddAttribute(3, "style", "cursor:pointer");
            builder.AddContent(4, formatedValue);
            builder.CloseElement();
        }

        private string FormatValue(object objValue)
        {
            if (objValue == null)
                return string.Empty;

            if (string.IsNullOrEmpty(Format))
                return objValue.ToString();

            var dateTime = objValue as DateTime?;

            if (dateTime.HasValue)
                return dateTime.Value.ToString(Format);

            return objValue.ToString();
        }

        private void OnClickTableHeader()
        {
            DataTableContext.OnClick(ValueExpression);
        }
    }
}
