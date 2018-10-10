using Microsoft.EntityFrameworkCore;
using Pocztowy.Shop.IServices;
using Pocztowy.Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pocztowy.Shop.DbServices
{
    public class DbOrdersService : DbEntitiesService<Order>, IOrdersService
    {
        public DbOrdersService(ShopContext context) : base(context)
        {
        }

        public override IList<Order> Get()
        {
            //var orders = context.Orders
            //    .Include(p=>p.Customer)
            //    .Include(p=>p.Details)
            //        .ThenInclude(p=>p.Item)
            //    .ToList();

            var orders = context.Orders
                .Include(p => p.Customer)
                .ToList();

            return orders;
        }

        public override Order Get(int id)
        {

            var order = base.Get(id);

            //var order = context.Orders
            //        .Include(p => p.Customer)
            //        .SingleOrDefault(p => p.Id == id);

            // order.Customer;

            // jawne ładowanie

            // pobranie pojedynczego obiektu
            // context.Entry(order).Reference(p => p.Customer).Load();

            // pobranie kolekcji obiektów
            // context.Entry(order).Collection(p => p.Details).Load();


           // context.Entry(order).State = EntityState.Detached;

            return order;
        }
    }
}
