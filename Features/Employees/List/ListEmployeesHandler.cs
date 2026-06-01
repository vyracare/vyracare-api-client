using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Features.Employees.List;

/// <summary>
/// Implementa o caso de uso correspondente a esta feature.
/// </summary>
public sealed class ListEmployeesHandler
{
    private readonly IEmployeeRepository _repository;

/// <summary>
/// Inicializa uma nova instância de ListEmployeesHandler.
/// </summary>
    public ListEmployeesHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da operação.
/// </summary>
    public async Task<UseCaseResult<IReadOnlyCollection<Employee>>> HandleAsync()
    {
        var employees = await _repository.ListAsync();
        return UseCaseResult<IReadOnlyCollection<Employee>>.Success(employees);
    }
}
