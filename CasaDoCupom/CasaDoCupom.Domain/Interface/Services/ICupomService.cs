using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Services.Base;
using CasaDoCupom.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCupom.Domain.Interface.Services
{
    public interface ICupomService : IServiceBase<Cupom, CupomModel, Guid>
    {
        Task<CupomModel> GetByCodigo(string descricao);

        Task<CupomModel> GetAvailableByEmpresaId(Guid empresaId);

        Task<IEnumerable<CupomModel>> GetByDataAlteracao(DateTime dataInicial, DateTime dataFinal);
    }
}