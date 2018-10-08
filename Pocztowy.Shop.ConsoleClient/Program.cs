using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pocztowy.Shop.DbServices;
using Pocztowy.Shop.FakeServices;
using Pocztowy.Shop.Generator;
using Pocztowy.Shop.IServices;
using Pocztowy.Shop.Models;
using System;
using System.IO;
using System.Linq;

namespace Pocztowy.Shop.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // CreateOrderTest();

            // PM> Install-Package Microsoft.Extensions.Configuration.Json

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.development.json", optional: true)
                .AddCommandLine(args) // Microsoft.Extensions.Configuration.CommandLine
                //  .AddXmlFile("appsettings.xml")
                .Build();

            string connectionString = configuration.GetConnectionString("ShopConnection");

            // Install-Package Microsoft.EntityFrameworkCore
            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();

            // Install-Package Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder.UseSqlServer(connectionString);

            ShopContext context = new ShopContext(optionsBuilder.Options);

            context.Database.EnsureCreated();


            string customers = configuration["Generator:Customers"];

            // Generowanie danych
            Generator.Generator generator = new Generator.Generator();
            var products = generator.GetProducts(100);
            var services = generator.GetServices(100);

            // Zapis danych
            context.Products.AddRange(products);
            context.Services.AddRange(services);
            context.SaveChanges();



            Console.WriteLine(customers);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();


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
