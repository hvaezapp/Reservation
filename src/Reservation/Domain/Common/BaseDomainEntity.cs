namespace Reservation.Domain.Common;

public class BaseDomain<T>
{
    public T Id { get; private set; }
}
public abstract class BaseDomainEntity : BaseDomain<long>
{
}
