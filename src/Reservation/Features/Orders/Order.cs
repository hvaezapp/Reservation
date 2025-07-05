using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Reservation.Common;
using Reservation.Features.Rooms;

namespace Reservation.Features.Orders
{
    public class Order : BaseDomainEntity
    {
        public const string TableName = "Orders";

        public string RequesterName { get; private set; } = null!;
        public string RequesterPhoneNom { get; private set; } = null!;
        public string RequesterNationalCode { get; private set; } = null!;
        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }


        public long RoomId { get; private set; }
        public Room Room { get; private set; } = null!;
    }


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
