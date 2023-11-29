using System.Linq.Expressions;
using System.Reflection;

namespace System;

//TODO:refactor to devpack
public static class Extensions
{
    public static IEnumerable<KeyValuePair<TKey, TValue>> Merge<TKey, TValue>(
        this IEnumerable<KeyValuePair<TKey, TValue>> baseDictionary,
        IEnumerable<KeyValuePair<TKey, TValue>> overwriteDictionary)
    {
        return overwriteDictionary.Union(baseDictionary).DistinctBy(x => x.Key);
    }

    public static PropertyInfo GetPropertyInfo<TReturn>(this Expression<Func<TReturn>> property)
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
