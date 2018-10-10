using Microsoft.EntityFrameworkCore;
using Pocztowy.Shop.DbServices.Configurations;
using Pocztowy.Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pocztowy.Shop.DbServices
{
    // migracje
    // PM> Install-Package Microsoft.EntityFrameworkCore.Design
    // PM> Install-Package Microsoft.EntityFrameworkCore.Tools

    public class ShopContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            #region 
            //modelBuilder.Entity<Customer>()
            //    .HasKey(p => p.Id);

            //modelBuilder.Entity<Customer>()
            //    .Property(p => p.FirstName)
            //    .HasMaxLength(40);

            //modelBuilder.Entity<Customer>()
            //    .Property(p => p.LastName)
            //    .HasMaxLength(40)
            //    .IsRequired();

            //modelBuilder.Entity<Item>()
            //    .Property(p => p.Name)
            //    .HasMaxLength(100)
            //    .IsRequired();

            //modelBuilder.Entity<Product>()
            //    .Property(p => p.Color)
            //    .HasMaxLength(20);

            #endregion

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());

            base.OnModelCreating(modelBuilder);
        }

    }
}
