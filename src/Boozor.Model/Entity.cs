using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Boozor.Model;

public abstract class Entity<TEntity> : IValidatableObject
        where TEntity : Entity<TEntity>
{
    private List<ValidationResult> _validations = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        _validations.Clear();
        Validate();

        return _validations;
    }

    protected void AddError<TKey>(Expression<Func<TEntity, TKey>> field, string errorMessage)
    {
        _validations.Add(new(errorMessage, new[] { GetName(field) }));
    }

    protected abstract void Validate();

    //TODO: remove to devpack
    private string GetName<TKey>(Expression<Func<TEntity, TKey>> action)
    {
        return GetNameFromMemberExpression(action.Body);
    }

    //TODO: remove to devpack
    private string GetNameFromMemberExpression(Expression expression)
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
