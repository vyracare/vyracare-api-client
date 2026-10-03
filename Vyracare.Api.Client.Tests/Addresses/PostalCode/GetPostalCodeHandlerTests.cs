using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Addresses.PostalCode;

namespace Vyracare.Api.Client.Tests.Addresses.PostalCode;

public sealed class GetPostalCodeHandlerTests
{
    [Fact]
    public async Task ReturnsAddressForValidMaskedPostalCode()
    {
        var expected = new PostalCodeResponse("01001001", "Praca da Se", null, "Se", "Sao Paulo", "SP");
        var service = new FakeService(expected);
        var result = await new GetPostalCodeHandler(service).HandleAsync("01001-001");
        Assert.True(result.IsSuccess);
        Assert.Equal(expected, result.Value);
        Assert.Equal("01001001", service.ReceivedPostalCode);
    }

    [Fact]
    public async Task ValidatesNotFoundAndUnavailableResults()
    {
        var invalid = await new GetPostalCodeHandler(new FakeService(null)).HandleAsync("123");
        Assert.Equal(UseCaseErrorType.Validation, invalid.ErrorType);
        var notFound = await new GetPostalCodeHandler(new FakeService(null)).HandleAsync("01001001");
        Assert.Equal(UseCaseErrorType.NotFound, notFound.ErrorType);
        var unavailable = await new GetPostalCodeHandler(new FakeService(null, true)).HandleAsync("01001001");
        Assert.Equal(UseCaseErrorType.Unavailable, unavailable.ErrorType);
    }

    private sealed class FakeService(PostalCodeResponse? response, bool fail = false) : ICepLookupService
    {
        public string? ReceivedPostalCode { get; private set; }
        public Task<PostalCodeResponse?> GetAsync(string postalCode, CancellationToken cancellationToken = default)
        {
            ReceivedPostalCode = postalCode;
            if (fail) throw new HttpRequestException();
            return Task.FromResult(response);
        }
    }
}
