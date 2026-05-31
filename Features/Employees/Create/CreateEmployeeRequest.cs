namespace Vyracare.Api.Client.Features.Employees.Create;

/// <summary>
/// Define o contrato de entrada ou saída usado por esta feature.
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
