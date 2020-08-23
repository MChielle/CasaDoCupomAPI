using CasaDoCupom.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CasaDoCupom.Data.Context
{
    public static class DataContextBuilder
    {
        public static bool AllMigrationsApplied(this DataContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(x => x.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(x => x.Key);

            return !total.Except(applied).Any();
        }

        public static void EnsureSeeded(this DataContext context)
        {
            SeedDataBase(context);
        }

        private static void SeedDataBase(DataContext context)
        {
            if (!context.Empresas.Any())
            {
                ExecuteSeeder<Empresa, Guid>(context, "01-empresas.json");
            }

            if (!context.Cupons.Any())
            {
                ExecuteSeeder<Cupom, Guid>(context, "02-cupons.json");
            }
        }

        private static void ExecuteSeeder<TEntity, TKey>(DataContext context, string entityJson) where TEntity : Entity<TKey>, new()
        {
            var seedDirectory = Path.Combine(AppContext.BaseDirectory, "Context", "Seed");
            var entities = JsonConvert.DeserializeObject<List<TEntity>>(File.ReadAllText(seedDirectory + Path.DirectorySeparatorChar + entityJson));
            context.Set<TEntity>().AddRange(entities);
            context.SaveChanges();
        }
    }
}