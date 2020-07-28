using System;
using System.Linq.Expressions;

namespace Curriculum.Client.Shared
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
