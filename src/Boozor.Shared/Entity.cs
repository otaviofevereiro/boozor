using System;
using System.ComponentModel.DataAnnotations;
namespace Boozor.Shared;


public interface IEntity : IValidatableObject
{
    public string? Id { get; set; }

    public static abstract string EntityName { get; }
}

