using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Linq.Expressions;

namespace Boozor.Components.Tables
{
    public class RowContext<TModel> : DataTableContext<TModel>
    {
        private readonly TModel item;

        public RowContext(TModel item)
        {
            this.item = item;
        }

        public override object GetValue(Expression<Func<TModel, object>> expression)
        {
            return expression.Compile()
                             ?.Invoke(item);
        }

        public override void Render(RenderTreeBuilder builder, Column<TModel> column)
        {
            object objValue = GetValue(column.ValueExpression);
            string formatedValue = FormatValue(objValue, column.Format);

            builder.OpenElement(0, "td");
            builder.AddContent(1, formatedValue);
            builder.CloseElement();
        }

        private string FormatValue(object objValue, string format)
        {
            if (objValue == null)
                return string.Empty;

            if (string.IsNullOrEmpty(format))
                return objValue.ToString();

            var dateTime = objValue as DateTime?;

            if (dateTime.HasValue)
                return dateTime.Value.ToString(format);

            return objValue.ToString();
        }
    }
}
