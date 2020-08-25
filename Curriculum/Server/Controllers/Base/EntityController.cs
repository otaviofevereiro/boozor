using AutoMapper;
using Boozor.Common;
using Curriculum.Business;
using Curriculum.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace Curriculum.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityController<TViewModel, TEntity> : ControllerBase
        where TEntity : Entities.Base.Entity
    {
        private readonly IEntityService<TEntity> entityService;
        private readonly IMapper mapper;

        public EntityController(IEntityService<TEntity> entityService, IMapper mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = ConvertToView(await entityService.Delete(id, cancellationToken));

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
                
                return BadRequest(result);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = new Result<IReadOnlyList<TViewModel>>();

            try
            {
                result.Item = mapper.ProjectTo<TViewModel>(entityService.AsQueryable()).ToImmutableArray();

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return BadRequest(result);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = ConvertToView(await entityService.Find(id, cancellationToken));

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return BadRequest(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TViewModel view, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = ConvertToView(await entityService.Add(ConvertToEntity(view), cancellationToken));

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return BadRequest(result);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TViewModel view, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = ConvertToView(await entityService.Update(ConvertToEntity(view), cancellationToken));

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return BadRequest(result);
            }
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
