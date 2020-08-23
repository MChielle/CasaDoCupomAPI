using AutoMapper.Configuration;
using CasaDoCupom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CasaDoCupom.Data.Context
{
    public class DataContext : DbContext
    {
        private static IConfiguration Configuration { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            CreateDatabase();

            if (!this.AllMigrationsApplied())
            {
                Database.Migrate();
                this.EnsureSeeded();
            }
        }

        public DataContext(DbContextOptions options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;

            if (!this.AllMigrationsApplied())
            {
                Database.Migrate();
                this.EnsureSeeded();
            }
        }

        public void CreateDatabase()
        {

            if (Database.GetPendingMigrations().Count() > 1)
                Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Cupom> Cupons { get; set; }

        public DbSet<Empresa> Empresas { get; set; }
    }
}