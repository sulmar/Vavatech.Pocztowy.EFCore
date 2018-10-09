using Pocztowy.Shop.Models;
using Pocztowy.Shop.Models.SearchCriteria;
using System;
using System.Collections.Generic;

namespace Pocztowy.Shop.IServices
{
    public interface ICustomersService : IEntitiesService<Customer>
    {
        IList<Customer> Get(string name);
    }

    public interface IItemsService : IEntitiesService<Item>
    {
    }

    public interface IProductsService : IEntitiesService<Product>
    {
        IList<Product> Get(ProductSearchCriteria searchCriteria);
    }

    public interface IOrdersService : IEntitiesService<Order>
    {
    }

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
