using Microsoft.AspNetCore.Mvc;
using Vyracare.Api.Client.Common.Http;
using Vyracare.Api.Client.Features.Patients.Create;
using Vyracare.Api.Client.Features.Patients.GetByCpf;
using Vyracare.Api.Client.Features.Patients.GetById;
using Vyracare.Api.Client.Features.Patients.List;

namespace Vyracare.Api.Client.Features.Patients;

[ApiController]
[Route("api/client/patients")]
/// <summary>
/// Exp?e os endpoints HTTP desta feature e delega o processamento para os handlers da aplica??o.
/// </summary>
public sealed class PatientsController : ControllerBase
{
    [HttpGet]
/// <summary>
/// Executa a responsabilidade associada a g et al l.
/// </summary>
    public async Task<IActionResult> GetAll([FromServices] ListPatientsHandler handler)
    {
        var result = await handler.HandleAsync();
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("{id}")]
/// <summary>
/// Executa a responsabilidade associada a g et by id.
/// </summary>
    public async Task<IActionResult> GetById(string id, [FromServices] GetPatientByIdHandler handler)
    {
        var result = await handler.HandleAsync(id);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("cpf/{cpf}")]
/// <summary>
/// Executa a responsabilidade associada a g et by cp f.
/// </summary>
    public async Task<IActionResult> GetByCpf(string cpf, [FromServices] GetPatientByCpfHandler handler)
    {
        var result = await handler.HandleAsync(cpf);
        return this.ToActionResult(result, Ok);
    }

    [HttpPost]
/// <summary>
/// Executa a responsabilidade associada a c re at e.
/// </summary>
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest request, [FromServices] CreatePatientHandler handler)
    {
        var result = await handler.HandleAsync(request);
        return this.ToActionResult(result, value => CreatedAtAction(nameof(GetById), new { id = value.Id }, value));
    }
}
