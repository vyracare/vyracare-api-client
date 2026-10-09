using Vyracare.Api.Client.Common.Results;

namespace Vyracare.Api.Client.Features.Addresses.PostalCode;

public sealed class GetPostalCodeHandler
{
    private readonly ICepLookupService _service;
    public GetPostalCodeHandler(ICepLookupService service) => _service = service;

    public async Task<UseCaseResult<PostalCodeResponse>> HandleAsync(string postalCode, CancellationToken cancellationToken = default)
    {
        var normalized = new string((postalCode ?? string.Empty).Where(char.IsDigit).ToArray());
        if (normalized.Length != 8)
            return UseCaseResult<PostalCodeResponse>.Failure(UseCaseErrorType.Validation, "CEP deve conter 8 digitos.");
        try
        {
            var address = await _service.GetAsync(normalized, cancellationToken);
            return address is null
                ? UseCaseResult<PostalCodeResponse>.Failure(UseCaseErrorType.NotFound, "CEP nao encontrado.")
                : UseCaseResult<PostalCodeResponse>.Success(address);
        }
        catch (Exception exception) when (exception is HttpRequestException or InvalidOperationException or TaskCanceledException)
        {
            return UseCaseResult<PostalCodeResponse>.Failure(UseCaseErrorType.Unavailable, "Consulta aos Correios temporariamente indisponivel.");
        }
    }
}
