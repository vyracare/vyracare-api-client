namespace Vyracare.Api.Client.Common.Configuration;

public sealed class CorreiosOptions
{
    public const string SectionName = "Correios";
    public string BaseUrl { get; set; } = "https://api.correios.com.br/cep/";
    public string AddressPathTemplate { get; set; } = "v2/enderecos/{0}";
    public string BearerToken { get; set; } = string.Empty;
}
