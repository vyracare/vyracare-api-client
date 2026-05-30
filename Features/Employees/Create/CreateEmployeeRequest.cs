namespace Vyracare.Api.Client.Features.Employees.Create;

/// <summary>
/// Define o contrato de entrada esperado por este caso de uso.
/// </summary>
public sealed record CreateEmployeeRequest(
    string FullName,
    string Email,
    string Role,
    string? Department,
    string? Phone,
    string AccessLevel,
    bool Active
);
