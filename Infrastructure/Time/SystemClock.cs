using Vyracare.Api.Client.Common.Time;

namespace Vyracare.Api.Client.Infrastructure.Time;

/// <summary>
/// Representa o componente respons?vel por fornecer a data e hora atual para a aplica??o.
/// </summary>
public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
