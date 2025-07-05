using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reservation.Common;

namespace Reservation.Domain
{
    public class Room : BaseDomain<long>
    {
        public const string TableName = "Rooms";

        public string Name { get; private set; } = null!;

        public ICollection<Order> Orders { get; private set; } = [];
    }

    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable(Room.TableName);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name) .IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.Orders)
                   .WithOne(z => z.Room)
                   .HasForeignKey(x => x.RoomId);
        }
    }
}
