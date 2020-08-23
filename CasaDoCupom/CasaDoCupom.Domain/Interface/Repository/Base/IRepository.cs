using CasaDoCupom.Domain.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCupom.Domain.Interface.Repository.Base
{
    public interface IRepositoryBase<TEntity, TKey>
    where TEntity : IEntity<TKey>
    {
        Task<TEntity> AddOrUpdateAsync(TEntity entity);

        Task<IEnumerable<TEntity>> AddOrUpdateAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(TKey id);

        Task<TEntity> GetByIdAsNoTrackingAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);
    }
}