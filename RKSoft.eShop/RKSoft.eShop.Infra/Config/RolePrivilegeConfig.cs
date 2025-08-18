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
    public class RolePrivilegeConfig : IEntityTypeConfiguration<RolePrivilege>
    {
        public void Configure(EntityTypeBuilder<RolePrivilege> builder)
        {
            builder.ToTable("RolePrivileges");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.RolePrivilegeName).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Description).IsRequired();
            builder.Property(n => n.IsActive).IsRequired();
            builder.Property(n => n.IsDeleted).IsRequired();
            builder.Property(n => n.CreatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(n => n.UpdatedAt).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(n => n.Role)
                   .WithMany(r => r.RolePrivileges)
                   .HasForeignKey(n => n.RoleId)
                   .HasConstraintName("FK_RolePrivileges_Roles");
                   //.OnDelete(DeleteBehavior.Cascade); // Assuming you want to cascade delete
        }
    }
}
