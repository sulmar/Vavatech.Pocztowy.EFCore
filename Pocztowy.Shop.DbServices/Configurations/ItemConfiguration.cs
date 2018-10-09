using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pocztowy.Shop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pocztowy.Shop.DbServices.Configurations
{

    

    class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {

            // PM> Install-Package Microsoft.EntityFrameworkCore.Relational
            builder.ToTable("Items");

            builder
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
