using Vyracare.Api.Client.Common.Time;

namespace Vyracare.Api.Client.Infrastructure.Time;

/// <summary>
/// Implementa o relógio padrão da aplicação usando a data e hora do sistema.
/// </summary>
public sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
