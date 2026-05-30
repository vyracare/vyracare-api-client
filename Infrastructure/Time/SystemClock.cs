using Vyracare.Api.Client.Common.Time;

namespace Vyracare.Api.Client.Infrastructure.Time;

public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
