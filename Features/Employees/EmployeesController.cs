using Microsoft.AspNetCore.Mvc;
using Vyracare.Api.Client.Common.Http;
using Vyracare.Api.Client.Features.Employees.Create;
using Vyracare.Api.Client.Features.Employees.GetByEmail;
using Vyracare.Api.Client.Features.Employees.GetById;
using Vyracare.Api.Client.Features.Employees.List;

namespace Vyracare.Api.Client.Features.Employees;

[ApiController]
[Route("api/client/employees")]
public sealed class EmployeesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] ListEmployeesHandler handler)
    {
        var result = await handler.HandleAsync();
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, [FromServices] GetEmployeeByIdHandler handler)
    {
        var result = await handler.HandleAsync(id);
        return this.ToActionResult(result, Ok);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email, [FromServices] GetEmployeeByEmailHandler handler)
    {
        var result = await handler.HandleAsync(email);
        return this.ToActionResult(result, Ok);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request, [FromServices] CreateEmployeeHandler handler)
    {
        var result = await handler.HandleAsync(request);
        return this.ToActionResult(result, value => CreatedAtAction(nameof(GetById), new { id = value.Id }, value));
    }
}
