using Pocztowy.Shop.Models;
using System.Collections.Generic;

namespace Pocztowy.Shop.IServices
{
    public interface IEntitiesService<TEntity>
         where TEntity : Base
    {
        IList<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Add(IList<TEntity> entities);
        void Update(TEntity entity);
        void Remove(int id);
    }

}
