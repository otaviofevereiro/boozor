using FluentValidation;
using System;

namespace Curriculum.Shared.Base
{
    public abstract class Entity<TEntity> : Entity, IValidatableEntity
        where TEntity : Entity<TEntity>
    {
        private readonly Lazy<EntityValidator<TEntity>> validatorLazy;

        protected Entity()
        {
            validatorLazy = new Lazy<EntityValidator<TEntity>>(CreateValidator());
        }

        public IValidator GetValidator()
        {
            return validatorLazy.Value;
        }

        protected abstract void Configure(EntityValidator<TEntity> validator);

        private EntityValidator<TEntity> CreateValidator()
        {
            var validator = new EntityValidator<TEntity>();

            Configure(validator);

            return validator;
        }
    }
}
