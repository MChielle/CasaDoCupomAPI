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

        public async Task<Cupom> GetByCodigoAsNoTrackingAsync(string codigo)
        {
            return await context.Cupons.Where(x => x.Codigo == codigo).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cupom>> GetByDataUltimaAlteracaoAsNoTrackingAsync(DateTime dataInicial, DateTime dataFinal)
        {
            return await context.Cupons.Where(x => (x.Validado && x.DataUltimaAlteracao >= dataInicial && x.DataUltimaAlteracao <= dataFinal)).ToArrayAsync();
        }

        public async Task<Cupom> GetDisponivelByEmpresaIdAsNoTrackingAsync(Guid empresaId)
        {
            return await context.Cupons.Where(x => (!x.Reservado && x.EmpresaId == empresaId)).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}