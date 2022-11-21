using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Boozor.Shared;

public static class Test
{
    public static ValidationResult NewValidation<TType, TKey>(this TType type, Expression<Func<TType, TKey>> field, string errorMessage)
    {
        return new ValidationResult(errorMessage, new[] { GetName(field) });
    }

    //TODO: remove to devpack
    private static string GetName<T, TKey>(Expression<Func<T, TKey>> action)
    {
        return GetNameFromMemberExpression(action.Body);
    }

    //TODO: remove to devpack
    private static string GetNameFromMemberExpression(Expression expression)
    {
        if (expression is MemberExpression memberExpression)
            return memberExpression.Member.Name;
        else if (expression is UnaryExpression unaryExpression)
        {
            return GetNameFromMemberExpression(unaryExpression.Operand);
        }

        return "MemberNameUnknown";
    }
}
