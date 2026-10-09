using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Vyracare.Api.Client.Common.Configuration;

namespace Vyracare.Api.Client.Features.Addresses.PostalCode;

public sealed class CorreiosCepLookupService : ICepLookupService
{
    private readonly HttpClient _httpClient;
    private readonly CorreiosOptions _options;

    public CorreiosCepLookupService(HttpClient httpClient, IOptions<CorreiosOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _httpClient.BaseAddress = new Uri(_options.BaseUrl);
    }

    public async Task<PostalCodeResponse?> GetAsync(string postalCode, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrWhiteSpace(_options.BearerToken))
        {
            try
            {
                return await GetFromCorreiosAsync(postalCode, cancellationToken);
            }
            catch (HttpRequestException)
            {
                // The public fallback keeps address lookup available when the contracted
                // Correios token expires or the primary provider is temporarily unavailable.
            }
        }

        return await GetFromFallbackAsync(postalCode, cancellationToken);
    }

    /// <summary>Queries the contracted Correios API when a bearer token is configured.</summary>
    private async Task<PostalCodeResponse?> GetFromCorreiosAsync(string postalCode, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_options.AddressPathTemplate, postalCode));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.BearerToken);
        using var response = await _httpClient.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        var address = await response.Content.ReadFromJsonAsync<CepAddress>(cancellationToken: cancellationToken);
        return MapAddress(postalCode, address);
    }

    /// <summary>Queries ViaCEP when Correios credentials are absent or the primary provider fails.</summary>
    private async Task<PostalCodeResponse?> GetFromFallbackAsync(string postalCode, CancellationToken cancellationToken)
    {
        var fallbackBaseUrl = new Uri(_options.FallbackBaseUrl, UriKind.Absolute);
        var path = string.Format(_options.FallbackAddressPathTemplate, postalCode);
        using var response = await _httpClient.GetAsync(new Uri(fallbackBaseUrl, path), cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        var address = await response.Content.ReadFromJsonAsync<CepAddress>(cancellationToken: cancellationToken);
        return address?.Error == true ? null : MapAddress(postalCode, address);
    }

    /// <summary>Maps provider-specific JSON fields to the stable API contract.</summary>
    private static PostalCodeResponse? MapAddress(string postalCode, CepAddress? address) =>
        address is null ? null : new PostalCodeResponse(postalCode, address.Street ?? string.Empty,
            address.Complement, address.Neighborhood ?? string.Empty, address.City ?? string.Empty, address.State ?? string.Empty);

    private sealed record CepAddress(
        [property: JsonPropertyName("cep")] string PostalCode,
        [property: JsonPropertyName("logradouro")] string? Street,
        [property: JsonPropertyName("complemento")] string? Complement,
        [property: JsonPropertyName("bairro")] string? Neighborhood,
        [property: JsonPropertyName("localidade")] string? City,
        [property: JsonPropertyName("uf")] string? State,
        [property: JsonPropertyName("erro")] bool? Error);
}
