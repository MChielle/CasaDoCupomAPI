using AutoMapper;
using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Repository;
using CasaDoCupom.Domain.Interface.Services;
using CasaDoCupom.Domain.Models;
using CasaDoCupom.Domain.Services.Base;
using System;
using System.Threading.Tasks;

namespace CasaDoCupom.Domain.Services
{
    public class EmpresaService : ServiceBase<Empresa, EmpresaModel, Guid>, IEmpresaService
    {
        private readonly IEmpresaRepository empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository,
            IMapper mapper)
            : base(empresaRepository, mapper)
        {
            this.empresaRepository = empresaRepository;
        }

        public override async Task<EmpresaModel> AddOrUpdateAsync(EmpresaModel model)
        {
            return await base.BuildModelAsync(await empresaRepository.AddOrUpdateAsync(await base.BuildEntityAsync(model)));
        }
    }
}