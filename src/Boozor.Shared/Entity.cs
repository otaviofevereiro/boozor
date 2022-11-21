using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
namespace Boozor.Shared;


public interface IEntity : IValidatableObject
{
    [Key]
    public Id Id { get; set; }

    public static abstract string EntityName { get; }
}

