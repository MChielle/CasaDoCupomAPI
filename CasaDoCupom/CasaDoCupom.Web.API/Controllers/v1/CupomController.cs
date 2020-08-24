using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Services;
using CasaDoCupom.Domain.Models;
using CasaDoCupom.Web.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCupom.Web.API.Controllers.v1
{
    [Route("cupom")]
    public class CupomController : ControllerBase<Cupom, CupomModel, Guid>
    {
        readonly private ICupomService _service;

        public CupomController(ICupomService service) : base(service)
        {
            _service = service;
        }

        [Route("disponivel"), HttpPost]
        public async Task<IActionResult> BuscarDisponivelPeloEmpresaId([FromForm] Guid empresaId)
        {
            try
            {
                var cuponsDisponiveis = await _service.GetAvailableByEmpresaId(empresaId);
                if (cuponsDisponiveis != null)
                    return Ok(cuponsDisponiveis);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Route("reservar"), HttpPost]
        public async Task<IActionResult> ResarvarPeloId([FromForm] Guid cupomId)
        {
            try
            {
                var cupom = await _service.GetByIdAsNoTrackingAsync(cupomId);
                if (cupom != null && cupom.Reservado) throw new Exception($"Cupom {cupom.Codigo} não pode ser RESERVADO uma segunda vez.");
                if (cupom != null && cupom.Validado) throw new Exception($"Cupom {cupom.Codigo} VALIDADO não pode ser RESERVADO.");
                if (cupom != null && !cupom.Reservado && !cupom.Validado)
                {
                    cupom.Reservado = true;
                    return Ok(await _service.AddOrUpdateAsync(cupom));
                }
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Route("validar"), HttpPost]
        public async Task<IActionResult> ValidarCodigo([FromForm] string codigo)
        {
            try
            {
                var cupom = await _service.GetByCodigo(codigo);
                if (cupom != null && !cupom.Reservado) throw new Exception($"Cupom {cupom.Codigo} não foi RESERVADO.");
                if (cupom != null && cupom.Validado) throw new Exception($"Cupom {cupom.Codigo} não pode ser usado uma segunda vez.");
                if (cupom != null && cupom.Reservado && !cupom.Validado)
                {
                    cupom.Validado = true;
                    return Ok(await _service.AddOrUpdateAsync(cupom));
                }
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Route("validadosporperiodo"), HttpPost]
        public async Task<IActionResult> ValidadosPorPeriodo([FromForm] DateTime dataInicio, [FromForm] DateTime dataFim)
        {
            try
            {
                var cupons = await _service.GetByDataAlteracao(dataInicio, dataFim);
                if (cupons.Any())
                {
                    return Ok(cupons);
                }
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}