using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Features.Employees.GetByEmail;

/// <summary>
/// Implementa o caso de uso correspondente a esta feature.
/// </summary>
public sealed class GetEmployeeByEmailHandler
{
    private readonly IEmployeeRepository _repository;

/// <summary>
/// Inicializa uma nova instância de GetEmployeeByEmailHandler.
/// </summary>
    public GetEmployeeByEmailHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da operação.
/// </summary>
    public async Task<UseCaseResult<Employee>> HandleAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return UseCaseResult<Employee>.Failure(UseCaseErrorType.Validation, "Email is required");
        }

        var employee = await _repository.GetByEmailAsync(email.Trim());
        return employee is null
            ? UseCaseResult<Employee>.Failure(UseCaseErrorType.NotFound, "Employee not found")
            : UseCaseResult<Employee>.Success(employee);
    }
}
