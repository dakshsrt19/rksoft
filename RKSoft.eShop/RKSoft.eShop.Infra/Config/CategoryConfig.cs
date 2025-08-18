using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using RKSoft.eShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.Infra.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.CategoryName).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Description).HasMaxLength(500);
            builder.Property(n => n.IsActive).IsRequired();
            builder.Property(n => n.CreatedAt).IsRequired();
            builder.Property(n => n.UpdatedAt).IsRequired();

            builder.HasOne(n => n.EStore)
                   .WithMany(n => n.Categories)
                   .HasForeignKey(n => n.StoreId)
                   .HasConstraintName("FK_Categories_Stores");
                   //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}

