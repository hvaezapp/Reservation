using Reservation.Domain.Common;

namespace Reservation.Domain.Entities
{
    public class Room : BaseDomainEntity
    {
        public const string TableName = "Rooms";

        public string Name { get; private set; } = null!;

        public bool IsReserved { get; set; }

        public ICollection<Order> Orders { get; private set; } = [];


        private Room(string name)
        {
            Name = name;
            IsReserved = false;
        }

        public static Room Create(string name)
        {
            //validation

            #region some logic
            //if (string.IsNullOrWhiteSpace(name))
            //    throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            #endregion

            return new Room(name);
        }

        public void Reserve() => IsReserved = true;
    }
}
