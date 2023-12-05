using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Product");
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(300);
            builder.Property(x => x.PictureUrl).IsRequired().HasMaxLength(300);
            builder.HasOne(x => x.Brand).WithMany().HasForeignKey(x => x.ProductBrandId);
            builder.HasOne(x => x.Type).WithMany().HasForeignKey(x => x.ProductTypeId);
        }
    }
}
