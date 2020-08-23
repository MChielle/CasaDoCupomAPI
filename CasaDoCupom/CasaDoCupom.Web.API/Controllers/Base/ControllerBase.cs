using CasaDoCupom.Domain.Entities.Base;
using CasaDoCupom.Domain.Interface.Model;
using CasaDoCupom.Domain.Interface.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCupom.Web.API.Controllers.Base
{
    [Route("api/[controller]")]
    public abstract class ControllerBase<TEntity, TModel, TKey> : Controller
        where TEntity : IEntity<TKey>
        where TModel : IModel<TKey>
    {
        private readonly IServiceBase<TEntity, TModel, TKey> _service;

        public ControllerBase(IServiceBase<TEntity, TModel, TKey> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(TKey id)
        {
            try
            {
                var result = await _service.GetByIdAsync(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromForm] TModel model)
        {
            try
            {
                if (ModelState.IsValid)
                    return Created(Request.Path, await _service.AddOrUpdateAsync(model));

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put([FromForm] TModel model)
        {
            try
            {
                if (ModelState.IsValid)
                    return Ok(await _service.AddOrUpdateAsync(model));

                return BadRequest("Ocorreu um erro ao executar a solicitação");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        public virtual IActionResult Delete([FromBody] IEnumerable<TModel> models)
        {
            try
            {
                _service.Delete(models);
                return Ok(new { message = "Deletado com sucesso" });
            }
            catch (Exception)
            {
                return new NotFoundResult();
            }
        }
    }
}