using AutoMapper;
using Curriculum.Api.Common;
using Curriculum.Business;
using Curriculum.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Curriculum.Server.Controllers
{
    public class EntityController<TViewModel, TEntity> : ControllerBase
        where TEntity : Entity
    {
        private readonly EntityService<TEntity> entityService;
        private readonly IMapper mapper;

        public EntityController(EntityService<TEntity> entityService, IMapper mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }

        [HttpDelete]
        public async Task<Result<TViewModel>> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = ConvertToView(await entityService.Delete(id, cancellationToken));
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        [HttpGet]
        public async Task<ListResult<TViewModel>> Get(CancellationToken cancellationToken = default)
        {
            var result = new ListResult<TViewModel>();

            try
            {
                foreach (var entity in await entityService.All(cancellationToken))
                {
                    result.Add(ConvertToView(entity));
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        [HttpGet("{id}")]
        public async Task<Result<TViewModel>> Get(int id, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = ConvertToView(await entityService.Find(id, cancellationToken));
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;

        }

        [HttpPost]
        public async Task<Result<TViewModel>> Post([FromBody] TViewModel view, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = ConvertToView(await entityService.Add(ConvertToEntity(view), cancellationToken));
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }

        [HttpPost]
        public async Task<Result<TViewModel>> Put([FromBody] TViewModel view, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = ConvertToView(await entityService.Update(ConvertToEntity(view), cancellationToken));
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }

            return result;
        }
        protected virtual TEntity ConvertToEntity(TViewModel view)
        {
            return mapper.Map<TViewModel, TEntity>(view);

        }

        protected virtual TViewModel ConvertToView(TEntity entity)
        {
            return mapper.Map<TEntity, TViewModel>(entity);
        }
    }
}
