using System.Linq.Expressions;
using System.Reflection;

namespace Boozor;

public static class Extensions
{
    //TODO:refactor
    public static PropertyInfo GetPropertyInfo<TType, TReturn>(this Expression<Func<TType, TReturn>> property)
    {
        LambdaExpression lambda = property;
        MemberExpression expression;

        if (lambda.Body is UnaryExpression unaryExpression)
        {
            expression = (MemberExpression)unaryExpression.Operand;
        }
        else if (lambda.Body is MemberExpression memberExpression)
        {
            expression = memberExpression;
        }
        else
        {
            expression = (MemberExpression)lambda.Body;
        }

        return (PropertyInfo)expression.Member;
    }
}
