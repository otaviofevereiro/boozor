using System;
using System.Linq.Expressions;

namespace Boozor.Components.Tables
{
    public abstract class DataTableContext<TModel>
    {
        public event Action<Expression<Func<TModel, object>>> OnClickEvent;

        public abstract object GetValue(Expression<Func<TModel, object>> expression);

        public void OnClick(Expression<Func<TModel, object>> expression)
        {
            OnClickEvent?.Invoke(expression);
        }
    }
}
