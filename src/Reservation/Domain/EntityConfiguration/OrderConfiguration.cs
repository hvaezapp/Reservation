using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservation.Domain.Entities;

namespace Reservation.Domain.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(Order.TableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RequesterName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.RequesterPhoneNom).IsRequired().HasMaxLength(11);
            builder.Property(x => x.RequesterNationalCode).IsRequired().HasMaxLength(10);


            builder.Property(x => x.FromDate)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToDateTime(TimeOnly.MinValue),
                        v => DateOnly.FromDateTime(v)
                    );

            builder.Property(x => x.ToDate)
                    .IsRequired()
                    .HasConversion(
                        v => v.ToDateTime(TimeOnly.MinValue),
                        v => DateOnly.FromDateTime(v)
                    );
        }
    }


}
