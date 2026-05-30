using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Features.Employees.Create;

/// <summary>
/// Implementa a regra de neg?cio do caso de uso representado por esta pasta.
/// </summary>
public sealed class CreateEmployeeHandler
{
    private readonly IEmployeeRepository _repository;
    private readonly IClock _clock;

/// <summary>
/// Inicializa uma nova inst?ncia de CreateEmployeeHandler.
/// </summary>
    public CreateEmployeeHandler(IEmployeeRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da opera??o.
/// </summary>
    public async Task<UseCaseResult<Employee>> HandleAsync(CreateEmployeeRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName) || string.IsNullOrWhiteSpace(request.Email))
        {
            return UseCaseResult<Employee>.Failure(UseCaseErrorType.Validation, "FullName and Email are required");
        }

        if (await _repository.ExistsByEmailAsync(request.Email.Trim()))
        {
            return UseCaseResult<Employee>.Failure(UseCaseErrorType.Conflict, "Ja existe um colaborador cadastrado com este e-mail.");
        }

        var timestamp = _clock.UtcNow;
        var employee = new Employee
        {
            FullName = request.FullName.Trim(),
            Email = request.Email.Trim(),
            Role = request.Role.Trim(),
            Department = request.Department?.Trim(),
            Phone = request.Phone?.Trim(),
            AccessLevel = request.AccessLevel.Trim(),
            Active = request.Active,
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };

        var created = await _repository.AddAsync(employee);
        return UseCaseResult<Employee>.Success(created);
    }
}
