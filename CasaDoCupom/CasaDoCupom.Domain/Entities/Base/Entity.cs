using CasaDoCupom.Domain.Entities.Base;

namespace CasaDoCupom.Domain.Entities
{
    public class Entity<TKey> : IEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}