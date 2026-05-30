using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Features.Employees.List;

public sealed class ListEmployeesHandler
{
    private readonly IEmployeeRepository _repository;

    public ListEmployeesHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<UseCaseResult<IReadOnlyCollection<Employee>>> HandleAsync()
    {
        var employees = await _repository.ListAsync();
        return UseCaseResult<IReadOnlyCollection<Employee>>.Success(employees);
    }
}
