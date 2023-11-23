using System.ComponentModel.DataAnnotations;

namespace Boozor.Shared;


public class Relation<TEntity> where TEntity : IEntity
{
    public Relation(TEntity entity)
    {
        Id = entity.Id;
    }

    public string? Id { get; }
}

