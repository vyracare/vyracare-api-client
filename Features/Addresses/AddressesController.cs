using Microsoft.AspNetCore.Mvc;
using Vyracare.Api.Client.Common.Http;
using Vyracare.Api.Client.Features.Addresses.PostalCode;

namespace Vyracare.Api.Client.Features.Addresses;

[ApiController]
[Route("api/client/addresses")]
public sealed class AddressesController : ControllerBase
{
    [HttpGet("postal-code/{postalCode}")]
    public async Task<IActionResult> GetPostalCode(string postalCode, [FromServices] GetPostalCodeHandler handler, CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(postalCode, cancellationToken);
        return this.ToActionResult(result, Ok);
    }
}
