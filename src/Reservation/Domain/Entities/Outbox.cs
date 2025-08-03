namespace Reservation.Domain.Entities
{
    public class Outbox
    {
        public int Id { get;  set; }
        public required string Message { get;  set; }
    }
}

public enum OutboxType
{
    Notification
}

public enum MessageType
{
    OrderCreated
}

public record OrderCreatedOutboxRequest
                    (string RequesterPhoneNom , 
                     string RequesterEmail ,
                     MessageType MessageType);

