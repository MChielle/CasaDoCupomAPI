using AutoMapper;
using CasaDoCupom.Domain.Entities.Base;
using CasaDoCupom.Domain.Interface.Model;
using CasaDoCupom.Domain.Interface.Repository.Base;
using CasaDoCupom.Domain.Interface.Services.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCupom.Domain.Services.Base
{
    public abstract class ServiceBase<TEntity, TModel, TKey> : IServiceBase<TEntity, TModel, TKey>
        where TEntity : IEntity<TKey>
        where TModel : IModel<TKey>
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryBase<TEntity, TKey> _repositoryBase;

        public ServiceBase(IRepositoryBase<TEntity, TKey> repositoryBase, IMapper mapper)
        {
            _repositoryBase = repositoryBase;
            _mapper = mapper;
        }

        public abstract Task<TModel> AddOrUpdateAsync(TModel model);

        public async Task<IEnumerable<TModel>> AddOrUpdateAsync(IEnumerable<TModel> models)
        {
            List<TModel> savedModels = new List<TModel>();
            foreach (var item in models.ToList())
            {
                savedModels.Add(await AddOrUpdateAsync(item));
            }

            return savedModels;
        }

        public virtual void Delete(TModel model)
        {
            TEntity entity = default(TEntity);

            Task.Run(async () =>
            {
                entity = await BuildEntityAsync(model);
            }).Wait();

            _repositoryBase.Delete(entity);
        }

        public virtual void Delete(IEnumerable<TModel> models)
        {
            List<TEntity> entities = new List<TEntity>();

            Task.Run(async () =>
            {
                entities = new List<TEntity>(await BuildEntityAsync(models));
            }).Wait();

            _repositoryBase.Delete(entities);
        }

        public virtual async Task<IEnumerable<TModel>> GetAllAsync()
        {
            return await BuildModelAsync(await _repositoryBase.GetAllAsync());
        }

        public virtual async Task<TModel> GetByIdAsync(TKey id)
        {
            return await BuildModelAsync(await _repositoryBase.GetByIdAsync(id));
        }

        public virtual async Task<TModel> GetByIdAsNoTrackingAsync(TKey id)
        {
            return await BuildModelAsync(await _repositoryBase.GetByIdAsNoTrackingAsync(id));
        }

        public virtual TEntity BuildEntity(TModel model)
        {
            return _mapper.Map<TModel, TEntity>(model);
        }

        public virtual async Task<TEntity> BuildEntityAsync(TModel model)
        {
            return await Task.Run(() => _mapper.Map<TModel, TEntity>(model));
        }

        public virtual IEnumerable<TEntity> BuildEntity(IEnumerable<TModel> models)
        {
            List<TEntity> entities = new List<TEntity>();

            foreach (var item in models.ToList())
            {
                entities.Add(BuildEntity(item));
            }

            return entities;
        }

        public virtual async Task<IEnumerable<TEntity>> BuildEntityAsync(IEnumerable<TModel> models)
        {
            List<TEntity> entities = new List<TEntity>();

            foreach (var item in models.ToList())
            {
                entities.Add(await BuildEntityAsync(item));
            }

            return entities;
        }

        public virtual async Task<TModel> BuildModelAsync(TEntity entity)
        {
            return await Task.Run(() => _mapper.Map<TEntity, TModel>(entity));
        }

        public virtual async Task<IEnumerable<TModel>> BuildModelAsync(IEnumerable<TEntity> entities)
        {
            List<TModel> models = new List<TModel>();

            foreach (var item in entities.ToList())
            {
                models.Add(await BuildModelAsync(item));
            }

            return models;
        }
    }
}