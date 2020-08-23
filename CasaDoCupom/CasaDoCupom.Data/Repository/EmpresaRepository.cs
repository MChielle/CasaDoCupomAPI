using CasaDoCupom.Data.Context;
using CasaDoCupom.Data.Repository.Base;
using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Repository;
using System;
using System.Threading.Tasks;

namespace CasaDoCupom.Data.Repository
{
    public class EmpresaRepository : RepositoryBase<DataContext, Empresa, Guid>, IEmpresaRepository
    {
        public EmpresaRepository(DataContext context) : base(context)
        {
        }

        public override async Task<Empresa> AddOrUpdateAsync(Empresa entity)
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
    }
}