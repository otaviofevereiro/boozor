using System;
using System.ComponentModel.DataAnnotations;
namespace Boozor.Shared;


public interface IEntity
{
    public string? Id { get; set; }
}

public interface IValidatableEntity : IEntity, IValidatableObject
{

}

