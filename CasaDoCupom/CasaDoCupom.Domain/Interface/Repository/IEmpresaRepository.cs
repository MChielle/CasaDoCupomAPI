using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Repository.Base;
using System;

namespace CasaDoCupom.Domain.Interface.Repository
{
    public interface IEmpresaRepository : IRepositoryBase<Empresa, Guid>
    {
    }
}