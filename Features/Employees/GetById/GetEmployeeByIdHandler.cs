using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Features.Employees.GetById;

/// <summary>
/// Implementa a regra de neg?cio do caso de uso representado por esta pasta.
/// </summary>
public sealed class GetEmployeeByIdHandler
{
    private readonly IEmployeeRepository _repository;

/// <summary>
/// Inicializa uma nova inst?ncia de GetEmployeeByIdHandler.
/// </summary>
    public GetEmployeeByIdHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da opera??o.
/// </summary>
    public async Task<UseCaseResult<Employee>> HandleAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return UseCaseResult<Employee>.Failure(UseCaseErrorType.Validation, "Id is required");
        }

        var employee = await _repository.GetByIdAsync(id);
        return employee is null
            ? UseCaseResult<Employee>.Failure(UseCaseErrorType.NotFound, "Employee not found")
            : UseCaseResult<Employee>.Success(employee);
    }
}
