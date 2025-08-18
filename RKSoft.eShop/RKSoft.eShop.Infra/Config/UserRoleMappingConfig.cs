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
    public class UserRoleMappingConfig : IEntityTypeConfiguration<UserRoleMapping>
    {
        public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.ToTable("UserRoleMappings");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasIndex(n => new { n.UserId, n.RoleId }).IsUnique().HasDatabaseName("UK_UserRoleMapping_UserId_RoleId");

            builder.Property(n => n.UserId).IsRequired();
            builder.Property(n => n.RoleId).IsRequired();

            builder.HasOne(n => n.Role)
                   .WithMany(r => r.UserRoleMappings)
                   .HasForeignKey(n => n.RoleId)
                   .HasConstraintName("FK_UserRoleMappings_Roles");
                   //.OnDelete(DeleteBehavior.Cascade); // Assuming you want to cascade delete

            builder.HasOne(n => n.User)
                   .WithMany(u => u.UserRoleMappings)
                   .HasForeignKey(n => n.UserId)
                   .HasConstraintName("FK_UserRoleMappings_Users");
            //.OnDelete(DeleteBehavior.Cascade); // Assuming you want to cascade delete
        }
    }
}
