namespace Reservation.Common
{
    public class BaseDomain<T>
    {
        public T Id { get; set; }
    }
    public abstract class BaseDomainEntity : BaseDomain<long>
    {
    }
}
