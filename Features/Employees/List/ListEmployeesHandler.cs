using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Features.Employees.List;

/// <summary>
/// Implementa a regra de neg?cio do caso de uso representado por esta pasta.
/// </summary>
public sealed class ListEmployeesHandler
{
    private readonly IEmployeeRepository _repository;

/// <summary>
/// Inicializa uma nova inst?ncia de ListEmployeesHandler.
/// </summary>
    public ListEmployeesHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da opera??o.
/// </summary>
    public async Task<UseCaseResult<IReadOnlyCollection<Employee>>> HandleAsync()
    {
        var employees = await _repository.ListAsync();
        return UseCaseResult<IReadOnlyCollection<Employee>>.Success(employees);
    }
}
