using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vyracare.Api.Client.Common.Http;
using Vyracare.Api.Client.Features.Patients.Create;
using Vyracare.Api.Client.Features.Patients.GetByCpf;
using Vyracare.Api.Client.Features.Patients.GetById;
using Vyracare.Api.Client.Features.Patients.List;
using Vyracare.Api.Client.Features.Patients.Notes;
using Vyracare.Api.Client.Features.Patients.Update;

namespace Vyracare.Api.Client.Features.Patients;

[ApiController]
[Route("api/client/patients")]
/// <summary>
/// Expõe os endpoints HTTP da feature e delega o processamento aos handlers da aplicação.
/// </summary>
public sealed class PatientsController : ControllerBase
{
    [HttpGet]
/// <summary>
/// Executa a responsabilidade do método G et Al l.
/// </summary>
    public async Task<IActionResult> GetAll([FromQuery] string? search, [FromServices] ListPatientsHandler handler)
    {
        var result = await handler.HandleAsync(search);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("{id}")]
/// <summary>
/// Executa a responsabilidade do método G et By Id.
/// </summary>
    public async Task<IActionResult> GetById(string id, [FromServices] GetPatientByIdHandler handler)
    {
        var result = await handler.HandleAsync(id);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("cpf/{cpf}")]
/// <summary>
/// Executa a responsabilidade do método G et By Cp f.
/// </summary>
    public async Task<IActionResult> GetByCpf(string cpf, [FromServices] GetPatientByCpfHandler handler)
    {
        var result = await handler.HandleAsync(cpf);
        return this.ToActionResult(result, Ok);
    }

    [HttpPost]
/// <summary>
/// Executa a responsabilidade do método C re at e.
/// </summary>
    public async Task<IActionResult> Create([FromBody] CreatePatientRequest request, [FromServices] CreatePatientHandler handler)
    {
        var result = await handler.HandleAsync(request);
        return this.ToActionResult(result, value => CreatedAtAction(nameof(GetById), new { id = value.Id }, value));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdatePatientRequest request, [FromServices] UpdatePatientHandler handler)
    {
        var result = await handler.HandleAsync(id, request);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("{id}/notes")]
    public async Task<IActionResult> GetNotes(string id, [FromServices] ListPatientNotesHandler handler)
    {
        var result = await handler.HandleAsync(id);
        return this.ToActionResult(result, Ok);
    }

    [HttpPost("{id}/notes")]
    public async Task<IActionResult> AddNote(string id, [FromBody] AddPatientNoteRequest request, [FromServices] AddPatientNoteHandler handler)
    {
        var authorId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub") ?? "unknown";
        var authorName = User.FindFirstValue(ClaimTypes.Name) ?? User.FindFirstValue("name") ?? User.FindFirstValue(ClaimTypes.Email) ?? "Funcionario";
        var result = await handler.HandleAsync(id, request, authorId, authorName);
        return this.ToActionResult(result, value => CreatedAtAction(nameof(GetNotes), new { id }, value));
    }
}
