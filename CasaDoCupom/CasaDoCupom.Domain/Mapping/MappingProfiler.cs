using AutoMapper;
using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Models;

namespace CasaDoCupom.Domain.Mapping
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<Empresa, EmpresaModel>().ReverseMap();
            CreateMap<Cupom, CupomModel>().ReverseMap();
        }
    }
}