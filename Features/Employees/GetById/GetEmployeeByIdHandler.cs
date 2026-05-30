using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Features.Employees.GetById;

public sealed class GetEmployeeByIdHandler
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByIdHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

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
