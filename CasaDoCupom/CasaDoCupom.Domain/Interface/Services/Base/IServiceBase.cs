using CasaDoCupom.Domain.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CasaDoCupom.Domain.Interface.Services.Base
{
    public interface IServiceBase<TEntity, TModel, TKey> where TEntity : IEntity<TKey>
    {
        Task<TModel> AddOrUpdateAsync(TModel model);

        Task<IEnumerable<TModel>> AddOrUpdateAsync(IEnumerable<TModel> entities);

        Task<TModel> GetByIdAsync(TKey id);

        Task<TModel> GetByIdAsNoTrackingAsync(TKey id);

        Task<IEnumerable<TModel>> GetAllAsync();

        void Delete(TModel model);

        void Delete(IEnumerable<TModel> models);

        TEntity BuildEntity(TModel model);

        Task<TEntity> BuildEntityAsync(TModel model);

        IEnumerable<TEntity> BuildEntity(IEnumerable<TModel> models);

        Task<IEnumerable<TEntity>> BuildEntityAsync(IEnumerable<TModel> models);

        Task<TModel> BuildModelAsync(TEntity entity);

        Task<IEnumerable<TModel>> BuildModelAsync(IEnumerable<TEntity> entities);
    }
}