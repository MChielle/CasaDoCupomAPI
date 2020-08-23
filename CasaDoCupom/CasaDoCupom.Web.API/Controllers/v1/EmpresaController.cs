using CasaDoCupom.Domain.Entities;
using CasaDoCupom.Domain.Interface.Services;
using CasaDoCupom.Domain.Models;
using CasaDoCupom.Web.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CasaDoCupom.Web.API.Controllers.v1
{
    [Route("empresa")]
    public class EmpresaController : ControllerBase<Empresa, EmpresaModel, Guid>
    {
        public EmpresaController(IEmpresaService service) : base(service)
        {
        }
    }
}