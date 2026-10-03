namespace Vyracare.Api.Client.Features.Addresses.PostalCode;

public interface ICepLookupService
{
    Task<PostalCodeResponse?> GetAsync(string postalCode, CancellationToken cancellationToken = default);
}
