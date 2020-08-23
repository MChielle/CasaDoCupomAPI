using CasaDoCupom.Data.Context;
using CasaDoCupom.Domain.Entities.Base;
using CasaDoCupom.Domain.Entities.CRUDBase;
using CasaDoCupom.Domain.Interface.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCupom.Data.Repository.Base
{
    public abstract class RepositoryBase<TContext, TEntity, TKey> : IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TContext : DataContext
    {
        private TContext _context;

        public RepositoryBase(TContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await InternalAdd(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        private async Task InternalAdd(TEntity entity)
        {
            await Task.Run(() =>
            {
                if (entity is EntityCRUD<TKey>)
                {
                    (entity as EntityCRUD<TKey>).DataUltimaAlteracao = DateTime.Now;
                    (entity as EntityCRUD<TKey>).DataCriacao = DateTime.Now;
                }

                _context.Entry(entity).State = EntityState.Added;
            });
        }

        protected virtual async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities)
        {
            List<TEntity> list = new List<TEntity>();
            await Task.Run(() =>
            {
                entities.ToList().ForEach(async x => list.Add(await AddAsync(x)));
            });
            return list;
        }

        protected virtual async Task UpdateAsync(TEntity entity)
        {
            await InternalUpdate(entity);
            await _context.SaveChangesAsync();
        }

        private async Task InternalUpdate(TEntity entity)
        {
            await Task.Run(() =>
            {
                if (entity is EntityCRUD<TKey>)
                    (entity as EntityCRUD<TKey>).DataUltimaAlteracao = DateTime.Now;

                _context.Entry(entity).State = EntityState.Modified;
            });
        }

        protected virtual async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() =>
            {
                entities.ToList().ForEach(async x => await UpdateAsync(x));
            });
        }

        public abstract Task<TEntity> AddOrUpdateAsync(TEntity entity);

        public async Task<IEnumerable<TEntity>> AddOrUpdateAsync(IEnumerable<TEntity> entities)
        {
            List<TEntity> persistedEntities = new List<TEntity>();

            foreach (var item in entities.ToList())
            {
                persistedEntities.Add(await AddOrUpdateAsync(item));
            }
            return persistedEntities;
        }

        public virtual void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(x => Delete(x));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var retorno = await _context.Set<TEntity>().ToListAsync();
            return retorno;
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            var retorno = await _context.Set<TEntity>().FindAsync(id);
            return retorno;
        }

        public virtual async Task<TEntity> GetByIdAsNoTrackingAsync(TKey id)
        {
            var retorno = await _context.Set<TEntity>().AsNoTracking().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return retorno;
        }
    }
}