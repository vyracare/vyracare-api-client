using Vyracare.Api.Client.Features.Employees.Shared.Domain;

namespace Vyracare.Api.Client.Features.Employees.Shared.Ports;

/// <summary>
/// Implementa a integra??o com a persist?ncia ou com uma depend?ncia externa da aplica??o.
/// </summary>
public interface IEmployeeRepository
{
    Task<IReadOnlyCollection<Employee>> ListAsync();
    Task<Employee?> GetByIdAsync(string id);
    Task<Employee?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
    Task<Employee> AddAsync(Employee employee);
}
