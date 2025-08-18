using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RKSoft.eShop.Domain.Entities;


namespace RKSoft.eShop.Infra.Config
{
    public class StoreConfig : IEntityTypeConfiguration<EStore>
    {
        public void Configure(EntityTypeBuilder<EStore> builder)
        {
            builder.ToTable("Stores");
            builder.HasKey(n => n.Id);
            builder.Property(n => n.StoreName).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Description).HasMaxLength(500);
            builder.Property(n => n.IsActive).IsRequired();
            builder.Property(n => n.CreatedAt).IsRequired();
            builder.Property(n => n.UpdatedAt).IsRequired();
        }
    }
}
