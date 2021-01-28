using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Boozor.Components.Tables
{
    public class TitleContext<TModel> : DataTableContext<TModel>
    {
        public override object GetValue(Expression<Func<TModel, object>> expression)
        {
            var memberExpression = GetMemberExpression(expression);
            var property = typeof(TModel).GetProperty(memberExpression.Member.Name);

            var displayAttribute = property?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), true)
                                           ?.FirstOrDefault() as System.ComponentModel.DataAnnotations.DisplayAttribute;

            if (displayAttribute == null || string.IsNullOrEmpty(displayAttribute.Name))
                return memberExpression.Member.Name;
            else
                return displayAttribute.Name;
        }

        public override void Render(RenderTreeBuilder builder, Column<TModel> column)
        {
            object objValue = GetValue(column.ValueExpression);

            builder.OpenElement(0, "th");
            builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => OnClick(column.ValueExpression)));
            builder.AddAttribute(3, "style", "cursor:pointer");
            builder.AddContent(4, objValue);
            builder.CloseElement();
        }

        private MemberExpression GetMemberExpression(Expression<Func<TModel, object>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }

            return memberExpression;
        }
    }
}
