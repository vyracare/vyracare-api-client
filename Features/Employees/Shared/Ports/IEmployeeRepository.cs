using Vyracare.Api.Client.Features.Employees.Shared.Domain;

namespace Vyracare.Api.Client.Features.Employees.Shared.Ports;

public interface IEmployeeRepository
{
    Task<IReadOnlyCollection<Employee>> ListAsync();
    Task<Employee?> GetByIdAsync(string id);
    Task<Employee?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task<Employee> AddAsync(Employee employee);
}
