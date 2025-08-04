namespace Reservation.Domain.Entities
{
    public class Outbox
    {
        public long Id { get; set; }
        public string Message { get; set; } = null!;
        public TemplateType TemplateType { get; set; }
        public EventType EventType { get; set; }
        public bool IsProcessed { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ProcessOn { get; set; }
        public int RetryCount { get; set; }
        public string ErrorMessage { get; set; } = null!;

    }
}

public enum EventType
{
    Notification,
    Integration,
    Logging
}

public enum TemplateType
{
    ReservationCompleted
}

