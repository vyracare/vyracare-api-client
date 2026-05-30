namespace Vyracare.Api.Client.Features.Employees.Create;

public sealed record CreateEmployeeRequest(
    string FullName,
    string Email,
    string Role,
    string? Department,
    string? Phone,
    string AccessLevel,
    bool Active
);
