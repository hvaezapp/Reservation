namespace Reservation.Shared;

public sealed class BrokerSetting
{
    public const string SectionName = "BrokerSetting";

    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}
