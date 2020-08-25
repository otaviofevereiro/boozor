using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace Boozor.Components.Tables
{
    public class TitleContext<TModel> : DataTableContext<TModel>
    {
        public TitleContext(IEnumerable Itens)
        {
            this.Itens = Itens;
        }

        public IEnumerable Itens { get; }

        public override string GetValue(Expression<Func<TModel, object>> expression)
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
