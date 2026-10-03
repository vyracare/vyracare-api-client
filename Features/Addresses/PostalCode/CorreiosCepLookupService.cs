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
        if (string.IsNullOrWhiteSpace(_options.BearerToken))
            throw new InvalidOperationException("Correios:BearerToken nao configurado.");

        using var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_options.AddressPathTemplate, postalCode));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.BearerToken);
        using var response = await _httpClient.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) return null;
        response.EnsureSuccessStatusCode();
        var address = await response.Content.ReadFromJsonAsync<CorreiosAddress>(cancellationToken: cancellationToken);
        return address is null ? null : new PostalCodeResponse(address.PostalCode, address.Street ?? string.Empty,
            address.Complement, address.Neighborhood ?? string.Empty, address.City ?? string.Empty, address.State ?? string.Empty);
    }

    private sealed record CorreiosAddress(
        [property: JsonPropertyName("cep")] string PostalCode,
        [property: JsonPropertyName("logradouro")] string? Street,
        [property: JsonPropertyName("complemento")] string? Complement,
        [property: JsonPropertyName("bairro")] string? Neighborhood,
        [property: JsonPropertyName("localidade")] string? City,
        [property: JsonPropertyName("uf")] string? State);
}
