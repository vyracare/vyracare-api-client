namespace Vyracare.Api.Client.Features.Addresses.PostalCode;

public sealed record PostalCodeResponse(string PostalCode, string Street, string? Complement, string Neighborhood, string City, string State);
