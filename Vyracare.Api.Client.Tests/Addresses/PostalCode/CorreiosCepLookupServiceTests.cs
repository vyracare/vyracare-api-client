using System.Net;
using System.Text;
using Microsoft.Extensions.Options;
using Vyracare.Api.Client.Common.Configuration;
using Vyracare.Api.Client.Features.Addresses.PostalCode;

namespace Vyracare.Api.Client.Tests.Addresses.PostalCode;

public sealed class CorreiosCepLookupServiceTests
{
    [Fact]
    public async Task UsesPublicFallbackWhenCorreiosTokenIsNotConfigured()
    {
        var handler = new StubHandler(request =>
        {
            Assert.Equal("https://viacep.com.br/ws/01519000/json/", request.RequestUri?.ToString());
            return Json(HttpStatusCode.OK,
                """{"cep":"01519-000","logradouro":"Rua do Lavapes","complemento":"","bairro":"Cambuci","localidade":"Sao Paulo","uf":"SP"}""");
        });
        var service = CreateService(handler, new CorreiosOptions());

        var result = await service.GetAsync("01519000");

        Assert.NotNull(result);
        Assert.Equal("01519000", result.PostalCode);
        Assert.Equal("Rua do Lavapes", result.Street);
        Assert.Equal("SP", result.State);
    }

    [Fact]
    public async Task FallsBackWhenCorreiosRejectsConfiguredToken()
    {
        var requests = 0;
        var handler = new StubHandler(request =>
        {
            requests++;
            if (request.RequestUri?.Host == "api.correios.com.br")
            {
                Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            return Json(HttpStatusCode.OK,
                """{"cep":"01001-000","logradouro":"Praca da Se","complemento":"lado impar","bairro":"Se","localidade":"Sao Paulo","uf":"SP"}""");
        });
        var options = new CorreiosOptions { BearerToken = "expired-token" };
        var service = CreateService(handler, options);

        var result = await service.GetAsync("01001000");

        Assert.Equal(2, requests);
        Assert.Equal("Praca da Se", result?.Street);
    }

    [Fact]
    public async Task ReturnsNullWhenFallbackReportsUnknownPostalCode()
    {
        var handler = new StubHandler(_ => Json(HttpStatusCode.OK, """{"erro":true}"""));
        var service = CreateService(handler, new CorreiosOptions());

        Assert.Null(await service.GetAsync("99999999"));
    }

    private static CorreiosCepLookupService CreateService(HttpMessageHandler handler, CorreiosOptions options) =>
        new(new HttpClient(handler), Options.Create(options));

    private static HttpResponseMessage Json(HttpStatusCode status, string content) => new(status)
    {
        Content = new StringContent(content, Encoding.UTF8, "application/json")
    };

    private sealed class StubHandler(Func<HttpRequestMessage, HttpResponseMessage> responseFactory) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) =>
            Task.FromResult(responseFactory(request));
    }
}
