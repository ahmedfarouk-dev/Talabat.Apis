using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.Core.Entities.Order;

namespace Talabat.Repositories.Data.Configuration
{
    public class OrederConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(C => C.ShippingAddress, D => D.WithOwner());

            builder.Property(o => o.Status)
                .HasConversion(

                os => os.ToString(),
                os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os));

            builder.Property(t => t.SubTotal)
                .HasColumnType("decimal(18,2)");

            //builder.HasOne(C => C.DeliveryMethod)
            //     .WithMany();

        }
    }
}
