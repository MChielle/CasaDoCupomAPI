using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Repository.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCupom.Domain.Interface.Repository
{
    public interface ICupomRepository : IRepositoryBase<Cupom, Guid>
    {
        Task<Cupom> GetByCodigoAsNoTrackingAsync(string codigo, Guid empresaId);

        Task<Cupom> GetDisponivelByEmpresaIdAsNoTrackingAsync(Guid empresaId);

        Task<IEnumerable<Cupom>> GetByDataUltimaAlteracaoAsNoTrackingAsync(DateTime dataInicial, DateTime dataFinal, Guid empresaId);
    }
}