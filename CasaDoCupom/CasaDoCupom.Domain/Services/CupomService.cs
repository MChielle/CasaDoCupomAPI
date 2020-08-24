using AutoMapper;
using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Repository;
using CasaDoCupom.Domain.Interface.Services;
using CasaDoCupom.Domain.Models;
using CasaDoCupom.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCupom.Domain.Services
{
    public class CupomService : ServiceBase<Cupom, CupomModel, Guid>, ICupomService
    {
        private readonly ICupomRepository cupomRepository;

        public CupomService(ICupomRepository itemRepository,
            IMapper mapper)
            : base(itemRepository, mapper)
        {
            this.cupomRepository = itemRepository;
        }

        public override async Task<CupomModel> AddOrUpdateAsync(CupomModel model)
        {
            return await base.BuildModelAsync(await cupomRepository.AddOrUpdateAsync(await base.BuildEntityAsync(model)));
        }

        public async Task<CupomModel> GetByCodigo(string codigo, Guid empresaId)
        {
            return await BuildModelAsync(await cupomRepository.GetByCodigoAsNoTrackingAsync(codigo, empresaId));
        }

        public async Task<CupomModel> GetAvailableByEmpresaId(Guid empresaId)
        {
            return await BuildModelAsync(await cupomRepository.GetDisponivelByEmpresaIdAsNoTrackingAsync(empresaId));
        }

        public async Task<IEnumerable<CupomModel>> GetByDataAlteracao(DateTime dataInicial, DateTime dataFinal, Guid empresaId)
        {
            return await BuildModelAsync(await cupomRepository.GetByDataUltimaAlteracaoAsNoTrackingAsync(dataInicial, dataFinal, empresaId));
        }
    }
}