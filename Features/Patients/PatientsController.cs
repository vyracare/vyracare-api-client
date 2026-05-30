using Microsoft.AspNetCore.Mvc;
using Vyracare.Api.Client.Common.Http;
using Vyracare.Api.Client.Features.Patients.Create;
using Vyracare.Api.Client.Features.Patients.GetByCpf;
using Vyracare.Api.Client.Features.Patients.GetById;
using Vyracare.Api.Client.Features.Patients.List;

namespace Vyracare.Api.Client.Features.Patients;

[ApiController]
[Route("api/client/patients")]
public sealed class PatientsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] ListPatientsHandler handler)
    {
        var result = await handler.HandleAsync();
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, [FromServices] GetPatientByIdHandler handler)
    {
        var result = await handler.HandleAsync(id);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("cpf/{cpf}")]
    public async Task<IActionResult> GetByCpf(string cpf, [FromServices] GetPatientByCpfHandler handler)
    {
        var result = await handler.HandleAsync(cpf);
        return this.ToActionResult(result, Ok);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest request, [FromServices] CreatePatientHandler handler)
    {
        var result = await handler.HandleAsync(request);
        return this.ToActionResult(result, value => CreatedAtAction(nameof(GetById), new { id = value.Id }, value));
    }
}
