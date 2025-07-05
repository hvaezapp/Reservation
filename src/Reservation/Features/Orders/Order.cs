using Reservation.Common;
using Reservation.Features.Rooms;

namespace Reservation.Features.Orders
{
    public class Order : BaseDomainEntity
    {
        public string? RequesterName { get; set; }
        public string? RequesterPhoneNom { get; set; }
        public string? RequesterNationalCode { get; set; }
        public long RoomId { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }


        public virtual Room Room { get; set; }
    }
}
