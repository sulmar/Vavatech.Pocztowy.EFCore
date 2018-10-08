using Pocztowy.Shop.IServices;
using Pocztowy.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocztowy.Shop.FakeServices
{

    public class FakeCustomersService : FakeEntitiesService<Customer>, ICustomersService
    {
        public IList<Customer> Get(string name)
        {
            return entities.Where(e => e.FullName == name).ToList();
        }
    }

    public class FakeItemsService : FakeEntitiesService<Item>, IItemsService
    {

    }

    public class FakeEntitiesService<TEntity> : IEntitiesService<TEntity>
        where TEntity : Base
    {
        protected IList<TEntity> entities = new List<TEntity>();

        public virtual void Add(TEntity entity) => entities.Add(entity);

        public virtual void Add(IList<TEntity> entities) => entities.ToList().ForEach(entity => Add(entity));

        public virtual IList<TEntity> Get() => entities;

        public virtual TEntity Get(int id) => entities.SingleOrDefault(e => e.Id == id);

        public virtual void Remove(int id)
        {
            var entity = Get(id);
            entities.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }

    //public class FakeCustomersService : ICustomersService
    //{
    //    private IList<Customer> _customers = new List<Customer>();

    //    public void Add(Customer customer) => _customers.Add(customer);

    //    public void Add(IList<Customer> customers)
    //    {
    //        customers.ToList().ForEach(customer => Add(customer));
    //    }

    //    public IList<Customer> Get() => _customers;

    //    public Customer Get(int id) => _customers.SingleOrDefault(c => c.Id == id);

    //    public void Remove(int id)
    //    {
    //        var customer = Get(id);
    //        _customers.Remove(customer);
    //    }

    //    public void Update(Customer customer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
