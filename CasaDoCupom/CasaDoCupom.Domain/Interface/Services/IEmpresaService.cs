using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Services.Base;
using CasaDoCupom.Domain.Models;
using System;

namespace CasaDoCupom.Domain.Interface.Services
{
    public interface IEmpresaService : IServiceBase<Empresa, EmpresaModel, Guid>
    {
    }
}