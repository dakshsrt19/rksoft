using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RKSoft.eShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.Infra.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.UserName).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Password).IsRequired();
            builder.Property(n => n.PasswordHash).IsRequired();
            builder.Property(n => n.UserTypeId).IsRequired();
            builder.Property(n => n.IsActive).IsRequired();
            builder.Property(n => n.IsDeleted).IsRequired();
            builder.Property(n => n.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(n => n.UpdatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            // Configure the relationship with UserType
            builder.HasOne(n => n.UserType)
                   .WithMany(n => n.Users)
                   .HasForeignKey(u => u.UserTypeId)
                   .HasConstraintName("FK_Users_UserTypes");
        }
    }
}
