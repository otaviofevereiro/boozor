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

        public override string GetValue(Expression<Func<TModel, object>> expression)
        {
            return expression.Compile()
                             ?.Invoke(item)
                             ?.ToString();
        }
    }
}
