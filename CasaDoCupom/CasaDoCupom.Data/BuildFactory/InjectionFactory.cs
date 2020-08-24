using CasaDoCupom.Data.Context;
using CasaDoCupom.Data.Repository;
using CasaDoCupom.Domain.Interface.Repository;
using CasaDoCupom.Domain.Interface.Services;
using CasaDoCupom.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CasaDoCupom.Data.BuildFactory
{
    public static class InjectionFactory
    {
        private static IServiceCollection Services { get; set; }

        private static IConfiguration Configuration { get; set; }

        public static IServiceCollection ConfigureAPI(IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;

            Services = services;

            Services.AddDbContext<DataContext>(options => options.UseMySql(Configuration["ConnectionStrings:local"]));

            LoadServices();
            LoadRepositories();

            return Services;
        }

        private static void LoadServices()
        {
            Services.AddScoped<ICupomService, CupomService>();
            Services.AddScoped<IEmpresaService, EmpresaService>();
        }

        private static void LoadRepositories()
        {
            Services.AddScoped<ICupomRepository, CupomRepository>();
            Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        }
    }
}