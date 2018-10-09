using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pocztowy.Shop.DbServices;
using Pocztowy.Shop.FakeServices;
using Pocztowy.Shop.Generator;
using Pocztowy.Shop.IServices;
using Pocztowy.Shop.Models;
using Pocztowy.Shop.Models.SearchCriteria;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pocztowy.Shop.ConsoleClient
{
    /*
     - wygenerowanie skryptu od podanej migracji
        Script-Migration -from AddProductBarcode -to InitialCreate 
      
    */

    class Program
    {
        static void Main(string[] args)
        {
            // CreateOrderTest();

            ContextFactory contextFactory = new ContextFactory();
            ShopContext context = contextFactory.CreateDbContext(args);

            var product = context.Products.First();


            var updateproducts = context.Products.OrderBy(p=>p.Id).Take(10).ToList();

            context.Products.RemoveRange(updateproducts);
            context.SaveChanges();

            context.Dispose();

            Console.WriteLine(context.Entry(product).State);

            product.UnitPrice = 400.00m;
            product.Color = "red";

            Console.WriteLine(context.Entry(product).State);

            product.UnitPrice = 300.00m;

            Console.WriteLine(context.Entry(product).State);

            Console.WriteLine(context.Entry(product).Property(p => p.UnitPrice).OriginalValue);

            context.SaveChanges();

            Console.WriteLine(context.Entry(product).State);

            
          //  context.Products.Remove(product);
            Console.WriteLine(context.Entry(product).State);

            context.Entry(product).State = EntityState.Unchanged;

            context.SaveChanges();


            IProductsService productsService = new DbProductsService(context);

            productsService.Remove(20);

            ProductSearchCriteria searchCriteria = new ProductSearchCriteria
            {
            };

            var colorProducts = productsService.Get(searchCriteria);

            if (colorProducts.Any())
            {
            }

            //  context.Database.EnsureDeleted();
            // context.Database.EnsureCreated();

            context.Database.Migrate();

            var customers = context.Customers.ToList();

            var products = context.Products
               .Where(p => p.UnitPrice > 100)
               .Where(p => p.Name.StartsWith("N"))
               .OrderBy(p => p.Name)
               .ThenBy(p => p.Color)
               .Select(p => new { p.Name, p.Color })
               .ToList();


            var productsByColor = context.Products
                .GroupBy(p => p.Color)
                .ToList();

            var productsQtyByColor = context.Products
               .GroupBy(p => p.Color)
               .Select(g => new { Color = g.Key, Qty = g.Count() })
               .ToList();

            //var productsOver100 = new List<Product>();

            //foreach (var product in context.Products)
            //{
            //    if (product.UnitPrice > 100)
            //    {
            //        productsOver100.Add(product);
            //    }
            //}



            // string customers = configuration["Generator:Customers"];

            // Generowanie danych
            // CreateSampleData(context);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            var tuple = GetTuple2();

            

        }

        private static (string name, string color) GetTuple2()
        {
            return ("cola", "black");
        }

        private static Tuple<string, string> GetTuple()
        {
            return new Tuple<string, string>("cola", "black");
        }

        private static void CreateSampleData(ShopContext context)
        {
            Generator.Generator generator = new Generator.Generator();
            var products = generator.GetProducts(100);
            var services = generator.GetServices(100);

            // Zapis danych
            context.Products.AddRange(products);
            context.Services.AddRange(services);
            context.SaveChanges();
        }

        private static void CreateOrderTest()
        {
            Console.WriteLine("Witaj w naszym sklepie!");

            Console.WriteLine("Podaj id klienta:");
            int customerId = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj id produktu:");
            int productId = int.Parse(Console.ReadLine());

            Console.WriteLine("Podaj ilość:");
            int quantity = int.Parse(Console.ReadLine());

            // TODO: pobrac klienta i produkt ze zrodla danych

            ICustomersService customersService = new FakeCustomersService();
            IItemsService itemsService = new FakeItemsService();

            // Przygotowanie danych
            Generator.Generator generator = new Generator.Generator();
            var customers = generator.GetCustomers(100);
            customersService.Add(customers);

            var products = generator.GetProducts(50);
            var services = generator.GetServices(50);

            // połączenie zbiorów
            var items = products.OfType<Item>().Concat(services).ToList();

            itemsService.Add(items);


            // Pobranie
            Customer customer = customersService.Get(customerId);
            Item item = itemsService.Get(productId);

            Order order = new Order("ZA 001", customer);
            order.Details.Add(new OrderDetail(item, quantity));
        }
    }

  
}
