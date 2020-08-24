using CasaDoCupom.Data.Context;
using CasaDoCupom.Data.Repository.Base;
using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCupom.Data.Repository
{
    public class CupomRepository : RepositoryBase<DataContext, Cupom, Guid>, ICupomRepository
    {
        private readonly DataContext context;

        public CupomRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<Cupom> AddOrUpdateAsync(Cupom entity)
        {
            if (entity.Id != default(Guid))
            {
                await base.UpdateAsync(entity);
            }
            else
            {
                await base.AddAsync(entity);
            }
            return entity;
        }

        public async Task<Cupom> GetByCodigoAsNoTrackingAsync(string codigo, Guid empresaId)
        {
            return await context.Cupons.Where(x => (x.Codigo == codigo && x.EmpresaId == empresaId)).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cupom>> GetByDataUltimaAlteracaoAsNoTrackingAsync(DateTime dataInicial, DateTime dataFinal, Guid empresaId)
        {
            return await context.Cupons.Where(x => (x.Validado && x.DataUltimaAlteracao >= dataInicial && x.DataUltimaAlteracao <= dataFinal && x.EmpresaId == empresaId)).ToArrayAsync();
        }

        public async Task<Cupom> GetDisponivelByEmpresaIdAsNoTrackingAsync(Guid empresaId)
        {
            return await context.Cupons.Where(x => (!x.Reservado && !x.Validado && x.EmpresaId == empresaId)).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}