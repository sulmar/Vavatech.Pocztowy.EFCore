using Pocztowy.Shop.IServices;
using Pocztowy.Shop.Models;
using System;
using System.Text;

namespace Pocztowy.Shop.DbServices
{

    public class DbServicesService : DbEntitiesService<Service>, IServicesService
    {
        public DbServicesService(ShopContext context) : base(context)
        {
        }
    }
}
