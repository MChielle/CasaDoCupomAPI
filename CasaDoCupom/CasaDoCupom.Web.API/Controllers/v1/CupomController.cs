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
    [Route("v1/cupons")]
    public class CupomController : ControllerBase<Cupom, CupomModel, Guid>
    {
        readonly private ICupomService _service;

        public CupomController(ICupomService service) : base(service)
        {
            _service = service;
        }

        [Route("v1/empresa/{empresaId}/cupom.json:disponivel"), HttpPost]
        public async Task<IActionResult> BuscarDisponivelPeloEmpresaId([FromRoute] Guid empresaId)
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

        [Route("v1/cupom/{cupomId}/cupom.json:reservar"), HttpPost]
        public async Task<IActionResult> ReservarPeloId([FromRoute] Guid cupomId)
        {
            try
            {
                var cupom = await _service.GetByIdAsNoTrackingAsync(cupomId);
                if (cupom != null && cupom.Reservado) return BadRequest($"Cupom {cupom.Codigo} não pode ser RESERVADO uma segunda vez.");
                if (cupom != null && cupom.Validado) return BadRequest($"Cupom {cupom.Codigo} VALIDADO não pode ser RESERVADO.");
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

        [Route("v1/empresa/{empresaId}/codigocupom/{codigo}/cupom.json:validar"), HttpPost]
        public async Task<IActionResult> ValidarCodigo([FromRoute] string codigo, [FromRoute] Guid empresaId)
        {
            try
            {
                var cupom = await _service.GetByCodigo(codigo, empresaId);
                if (cupom != null && !cupom.Reservado) return BadRequest($"Cupom {cupom.Codigo} não foi RESERVADO.");
                if (cupom != null && cupom.Validado) return BadRequest($"Cupom {cupom.Codigo} não pode ser usado uma segunda vez.");
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

        [Route("v1/empresa/{empresaId}/datainicio/{dataInicio}/datafim/{dataFim}/cupons.json:validados"), HttpPost]
        public async Task<IActionResult> ValidadosPorPeriodo([FromRoute] DateTime dataInicio, [FromRoute] DateTime dataFim, [FromRoute] Guid empresaId)
        {
            try
            {
                var cupons = await _service.GetByDataAlteracao(dataInicio, dataFim, empresaId);
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