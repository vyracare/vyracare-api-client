namespace Vyracare.Api.Client.DTOS;

public record CreateEmployeeRequest(
    string FullName,
    string Email,
    string Role,
    string? Department,
    string? Phone,
    string AccessLevel,
    bool Active
);
