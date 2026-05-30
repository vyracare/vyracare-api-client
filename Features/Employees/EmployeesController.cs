using Microsoft.AspNetCore.Mvc;
using Vyracare.Api.Client.Common.Http;
using Vyracare.Api.Client.Features.Employees.Create;
using Vyracare.Api.Client.Features.Employees.GetByEmail;
using Vyracare.Api.Client.Features.Employees.GetById;
using Vyracare.Api.Client.Features.Employees.List;

namespace Vyracare.Api.Client.Features.Employees;

[ApiController]
[Route("api/client/employees")]
/// <summary>
/// Exp?e os endpoints HTTP desta feature e delega o processamento para os handlers da aplica??o.
/// </summary>
public sealed class EmployeesController : ControllerBase
{
    [HttpGet]
/// <summary>
/// Executa a responsabilidade associada a g et al l.
/// </summary>
    public async Task<IActionResult> GetAll([FromServices] ListEmployeesHandler handler)
    {
        var result = await handler.HandleAsync();
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("{id}")]
/// <summary>
/// Executa a responsabilidade associada a g et by id.
/// </summary>
    public async Task<IActionResult> GetById(string id, [FromServices] GetEmployeeByIdHandler handler)
    {
        var result = await handler.HandleAsync(id);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("email/{email}")]
/// <summary>
/// Executa a responsabilidade associada a g et by em ai l.
/// </summary>
    public async Task<IActionResult> GetByEmail(string email, [FromServices] GetEmployeeByEmailHandler handler)
    {
        var result = await handler.HandleAsync(email);
        return this.ToActionResult(result, Ok);
    }

    [HttpPost]
/// <summary>
/// Executa a responsabilidade associada a c re at e.
/// </summary>
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request, [FromServices] CreateEmployeeHandler handler)
    {
        var result = await handler.HandleAsync(request);
        return this.ToActionResult(result, value => CreatedAtAction(nameof(GetById), new { id = value.Id }, value));
    }
}
