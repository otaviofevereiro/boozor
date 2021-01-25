using FluentValidation;
using System;
using System.Text.Json;

namespace DevPack.Data.Core
{
    public abstract class Entity<TEntity, TId> : Entity<TId>, IValidatableEntity
        where TEntity : Entity<TEntity, TId>
    {
        private readonly Lazy<EntityValidator<TEntity>> validatorLazy;
        protected Entity()
        {
            validatorLazy = new Lazy<EntityValidator<TEntity>>(CreateValidator());
        }

        public IValidator Validator => validatorLazy.Value;

        public new TEntity Clone()
        {
            return JsonSerializer.Deserialize<TEntity>(JsonSerializer.Serialize((TEntity)this));
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
