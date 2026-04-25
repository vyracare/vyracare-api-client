namespace Vyracare.Api.Client.DTOS;

public record CreateClientRequest(
    string Name,
    string? Description
);
