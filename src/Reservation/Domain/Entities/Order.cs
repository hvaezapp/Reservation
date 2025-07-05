using Reservation.Domain.Common;

namespace Reservation.Domain.Entities
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


}
