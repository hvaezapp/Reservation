using Reservation.Common;
using Reservation.Features.Orders;

namespace Reservation.Features.Rooms
{
    public class Room : BaseDomainEntity
    {
        public string? Title { get; set; }
        public string? Code { get;  set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
