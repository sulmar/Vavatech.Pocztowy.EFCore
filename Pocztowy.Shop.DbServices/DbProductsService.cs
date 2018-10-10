using Microsoft.EntityFrameworkCore;
using Pocztowy.Shop.IServices;
using Pocztowy.Shop.Models;
using Pocztowy.Shop.Models.SearchCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pocztowy.Shop.DbServices
{
    public class DbProductsService : IProductsService
    {
        private readonly ShopContext context;

        public DbProductsService(ShopContext context)
        {
            this.context = context;
        }

        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Add(IList<Product> entities)
        {
            throw new NotImplementedException();
        }

        public IList<Product> Get(ProductSearchCriteria searchCriteria)
        {
            var products = context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.Color))
            {
               products = products.Where(p => p.Color == searchCriteria.Color);
            }

            if (!string.IsNullOrEmpty(searchCriteria.Barcode))
            {
                products = products.Where(p => p.Barcode == searchCriteria.Barcode);
            }

            if (searchCriteria.UnitPrice.From.HasValue)
            {
                products = products.Where(p => p.UnitPrice >= searchCriteria.UnitPrice.From);
            }

            if (searchCriteria.UnitPrice.To.HasValue)
            {
                products = products.Where(p => p.UnitPrice <= searchCriteria.UnitPrice.To);
            }

            var colors = new List<string> { "red", "blue" };

            products = products.Where(p => colors.Contains(p.Color));

            return products.ToList();


        }

        public IList<Product> Get()
        {
            return context.Products.ToList();
        }

        public Product Get(int id)
        {
            return context.Products.Find(id);
        }

        public IList<Product> GetByColor(string color)
        {
            return context.Products.Where(p => p.Color == color).ToList();
        }

        public void Remove(int id)
        {
            Product product = new Product { Id = id };

            Console.WriteLine(context.Entry(product).State);

            context.Products.Attach(product);

            Console.WriteLine(context.Entry(product).State);

            //  context.Entry(product).State = EntityState.Deleted;
            // var product = context.Products.Find(id);

            context.Products.Remove(product);
            Console.WriteLine(context.Entry(product).State);

            context.SaveChanges();
        }

        public void Update(Product entity)
        {
            // context.Entry(entity).State = EntityState.Modified;
            context.Products.Update(entity);
            context.SaveChanges();
        }
    }
}
