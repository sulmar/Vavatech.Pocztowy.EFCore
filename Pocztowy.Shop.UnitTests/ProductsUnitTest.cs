using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Pocztowy.Shop.DbServices;
using Pocztowy.Shop.Generator;
using Pocztowy.Shop.IServices;
using System;
using System.Transactions;
using Xunit;

namespace Pocztowy.Shop.UnitTests
{
    public class ProductsUnitTest
    {

        [Fact]
        public void NativeTransactionsTest()
        {
            var options = new DbContextOptionsBuilder<ShopContext>()
               .UseInMemoryDatabase(databaseName: "getproducts")
               .Options;


            Generator.Generator generator = new Generator.Generator();
            using (var context = new ShopContext(options))
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    context.Products.AddRange(generator.GetProducts(50));
                    context.SaveChanges();

                    context.Products.AddRange(generator.GetProducts(10));
                    context.SaveChanges();

                    transaction.Commit();
                }

            }
        }

        [Fact]
        public void DistributedTransactionsTest()
        {
            var options = new DbContextOptionsBuilder<ShopContext>()
               .UseInMemoryDatabase(databaseName: "getproducts")
               .Options;


            Generator.Generator generator = new Generator.Generator();

            using (var transactionScope = new TransactionScope())
            {
                using (var context1 = new ShopContext(options))
                {
                    context1.Products.AddRange(generator.GetProducts(50));
                    context1.SaveChanges();
                }

                using (var context2 = new ShopContext(options))
                {
                    context2.Products.AddRange(generator.GetProducts(10));
                    context2.SaveChanges();
                }

                transactionScope.Complete();
            }
        }

        [Fact]
        public void GetProductsTest()
        {
            // Arrange

            // PM> Install-Package Microsoft.EntityFrameworkCore.InMemory
            var options = new DbContextOptionsBuilder<ShopContext>()
                .UseInMemoryDatabase(databaseName: "getproducts")
                .Options;

            using (var context = new ShopContext(options))
            {
                Generator.Generator generator = new Generator.Generator();
                context.Products.AddRange(generator.GetProducts(100));
                context.SaveChanges();
            }

            using (var context = new ShopContext(options))
            {
                IProductsService productsService = new DbProductsService(context);

                // Acts
                var products = productsService.Get();

                // Asserts
                Assert.NotEmpty(products);

                // PM> Install-Package FluentAssertions

                products.Should().NotBeNullOrEmpty();


            }

        }

        [Fact]
        public void GetProductTest()
        {

            var options = new DbContextOptionsBuilder<ShopContext>()
               .UseInMemoryDatabase(databaseName: "getproducts")
               .Options;

            //using (var context = new ShopContext(options))
            //{
            //    Generator.Generator generator = new Generator.Generator();
            //    context.Products.AddRange(generator.GetProducts(100));
            //    context.SaveChanges();
            //}

            using (var context = new ShopContext(options))
            {
                IProductsService productsService = new DbProductsService(context);

                // Acts
                var product = productsService.Get(1);

                // Asserts
                Assert.NotNull(product);
            }
        }

    }
}
