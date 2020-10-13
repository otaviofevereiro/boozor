using AutoMapper;
using Boozor.Common;
using Curriculum.Business;
using Curriculum.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
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
                var entity = await entityService.Delete(id, cancellationToken);

                result.Item = mapper.Map<TEntity, TViewModel>(entity);

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
                var entities = entityService.AsQueryable();

                result.Item = entities.Select(entity => mapper.Map<TEntity, TViewModel>(entity))
                                      .ToImmutableArray();

                //Não utilizado projecto por que da erro com enums
                //result.Item = mapper.ProjectTo<TViewModel>(item).ToImmutableArray();

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
                var entity = await entityService.Find(id, cancellationToken);

                result.Item = mapper.Map<TEntity, TViewModel>(entity);

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
                var entity = mapper.Map<TViewModel, TEntity>(view);

                await entityService.Add(entity, cancellationToken);

                result.Item = mapper.Map<TEntity, TViewModel>(entity);

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
                var entity = mapper.Map<TViewModel, TEntity>(view);

                await entityService.Update(entity, cancellationToken);

                result.Item = mapper.Map<TEntity, TViewModel>(entity);

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return BadRequest(result);
            }
        }
    }
}
