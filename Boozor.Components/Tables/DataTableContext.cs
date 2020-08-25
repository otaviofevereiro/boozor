using System;
using System.Linq.Expressions;

namespace Boozor.Components.Tables
{
    public abstract class DataTableContext<TModel>
    {
        public abstract string GetValue(Expression<Func<TModel, object>> expression);
    }
}
