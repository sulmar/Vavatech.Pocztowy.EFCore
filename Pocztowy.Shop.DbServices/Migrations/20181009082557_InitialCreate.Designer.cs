﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pocztowy.Shop.DbServices;

namespace Pocztowy.Shop.DbServices.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20181009082557_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pocztowy.Shop.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasMaxLength(40);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Pocztowy.Shop.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");
                });

            modelBuilder.Entity("Pocztowy.Shop.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId");

                    b.Property<DateTime?>("DeliveryDate");

                    b.Property<string>("Number");

                    b.Property<DateTime>("OrderDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Pocztowy.Shop.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ItemId");

                    b.Property<int?>("OrderId");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Pocztowy.Shop.Models.Product", b =>
                {
                    b.HasBaseType("Pocztowy.Shop.Models.Item");

                    b.Property<string>("Color")
                        .HasMaxLength(30);

                    b.Property<string>("Description");

                    b.ToTable("Product");

                    b.HasDiscriminator().HasValue("Product");
                });

            modelBuilder.Entity("Pocztowy.Shop.Models.Service", b =>
                {
                    b.HasBaseType("Pocztowy.Shop.Models.Item");

                    b.Property<long?>("Duration")
                        .HasColumnType("bigint");

                    b.ToTable("Service");

                    b.HasDiscriminator().HasValue("Service");
                });

            modelBuilder.Entity("Pocztowy.Shop.Models.Order", b =>
                {
                    b.HasOne("Pocztowy.Shop.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Pocztowy.Shop.Models.OrderDetail", b =>
                {
                    b.HasOne("Pocztowy.Shop.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId");

                    b.HasOne("Pocztowy.Shop.Models.Order")
                        .WithMany("Details")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
