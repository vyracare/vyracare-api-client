using Microsoft.AspNetCore.Mvc;
using Vyracare.Api.Client.DTOS;
using Vyracare.Api.Client.Models;
using Vyracare.Api.Client.Services;

namespace Vyracare.Api.Client.Controllers;

[ApiController]
[Route("api/client/employees")]
public class EmployeesController : ControllerBase
{
    private readonly EmployeeService _service;

    public EmployeesController(EmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var item = await _service.GetByEmailAsync(email);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeRequest request)
    {
        if (await _service.ExistsByEmailAsync(request.Email))
        {
            return Conflict(new { message = "Ja existe um colaborador cadastrado com este e-mail." });
        }

        var item = new EmployeeModel
        {
            FullName = request.FullName,
            Email = request.Email,
            Role = request.Role,
            Department = request.Department,
            Phone = request.Phone,
            AccessLevel = request.AccessLevel,
            Active = request.Active
        };

        await _service.CreateAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }
}
