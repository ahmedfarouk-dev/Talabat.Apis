using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities;

namespace Talabat.Repositories.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired()
            /*    .HasMaxLength(100)*/;
            builder.Property(p => p.Description).IsRequired()
              /*.HasMaxLength(100)*/;
            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.PictureUrl).IsRequired();

            builder.HasOne(o => o.ProductBrand).WithMany()
                .HasForeignKey(o => o.BrandId);
            builder.HasOne(o => o.productCategory).WithMany()
                 .HasForeignKey(o => o.CategoryId);
        }
    }
}

