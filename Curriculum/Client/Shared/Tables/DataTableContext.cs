using System;
using System.Linq.Expressions;

namespace Curriculum.Client.Shared
{
    public abstract class DataTableContext<TModel>
    {
        public abstract string GetValue(Expression<Func<TModel, object>> expression);
    }
}
