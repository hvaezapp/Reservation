using Reservation.Domain.Common;

namespace Reservation.Domain.Entities
{
    public class Room : BaseDomainEntity
    {
        public const string TableName = "Rooms";

        public string Name { get; private set; } = null!;

        public ICollection<Order> Orders { get; private set; } = [];


        public Room(string name)
        {
            Name = name;
        }
    }
}
