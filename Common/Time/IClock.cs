namespace Vyracare.Api.Client.Common.Time;

public interface IClock
{
    DateTime UtcNow { get; }
}
