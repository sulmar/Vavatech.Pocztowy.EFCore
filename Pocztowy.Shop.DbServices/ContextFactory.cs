using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pocztowy.Shop.DbServices
{
    public class ContextFactory : IDesignTimeDbContextFactory<ShopContext>
    {
        public ShopContext CreateDbContext(string[] args)
        {
            // Install-Package Microsoft.Extensions.Configuration.Json
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            string connectionString = configuration.GetConnectionString("ShopConnection");

            // Install-Package Microsoft.EntityFrameworkCore
            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();

            // Install-Package Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder.UseSqlServer(connectionString);

            ShopContext context = new ShopContext(optionsBuilder.Options);

            return context;
        }
    }
}
