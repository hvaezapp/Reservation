using Reservation.Domain.Common;

namespace Reservation.Domain.Entities
{
    public class Order : BaseDomainEntity
    {
        public const string TableName = "Orders";

        public string RequesterName { get; private set; } = null!;
        public string RequesterPhoneNom { get; private set; } = null!;
        public string RequesterEmail { get; private set; } = null!;
        public string RequesterNationalCode { get; private set; } = null!;
        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }


        public long RoomId { get; private set; }
        public Room Room { get; private set; } = null!;



        private Order(string requesterName, string requesterPhoneNom, 
                      string requesterEmail, string requesterNationalCode ,
                      DateOnly fromDate, DateOnly toDate, long roomId)

        {
            RequesterName = requesterName;
            RequesterPhoneNom = requesterPhoneNom;
            RequesterNationalCode = requesterNationalCode;
            RequesterEmail = requesterEmail;
            FromDate = fromDate;
            ToDate = toDate;
            RoomId = roomId;
        }



        public static Order Create(string requesterName, string requesterPhoneNom,
                                   string requesterEmail,string requesterNationalCode,
                                   DateOnly fromDate, DateOnly toDate, long roomId)

        {
            //validation

            #region some logic
            /*
             
            if (string.IsNullOrWhiteSpace(requesterName))
                throw new ArgumentException("RequesterName cannot be null or empty.", nameof(requesterName));


            else if (string.IsNullOrWhiteSpace(requesterPhoneNom))
                throw new ArgumentException("RequesterPhoneNom cannot be null or empty.", nameof(requesterPhoneNom));


            else if (string.IsNullOrWhiteSpace(requesterNationalCode))
                throw new ArgumentException("RequesterNationalCode cannot be null or empty.", nameof(requesterNationalCode));


            else if (fromDate >= toDate)
                throw new ArgumentException("FromDate must be earlier than ToDate.", nameof(fromDate));

            */
            #endregion

            return new Order(requesterName, requesterPhoneNom , 
                             requesterEmail , requesterNationalCode,
                             fromDate, toDate, roomId);
        }
    }


}
