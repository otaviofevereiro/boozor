using AutoMapper;
using Boozor.Core;
using DevPack.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Curriculum.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntityController<TViewModel, TEntity> : ControllerBase
        where TEntity : Entity
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TEntity> _repository;
        public EntityController(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                await _repository.DeleteAsync(id, cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new Result<IReadOnlyList<TViewModel>>();

            try
            {
                var entities = await _repository.AllAsync();
                result.Item = entities.Select(entity => _mapper.Map<TEntity, TViewModel>(entity))
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
        public async Task<IActionResult> Get(string id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.FindAsync(id, cancellationToken);

            return EnsureToView(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TViewModel view, CancellationToken cancellationToken = default)
        {
            var result = new Result<TViewModel>();

            try
            {
                var entity = _mapper.Map<TViewModel, TEntity>(view);

                await _repository.SaveAsync(entity, cancellationToken);

                result.Item = _mapper.Map<TEntity, TViewModel>(entity);

                return Ok(result);
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);

                return BadRequest(result);
            }
        }

        protected IActionResult EnsureToView(TEntity entity)
        {
            var result = new Result<TViewModel>();

            try
            {
                result.Item = _mapper.Map<TEntity, TViewModel>(entity);

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
