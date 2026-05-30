using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Employees.Create;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Tests.Employees.Create;

public sealed class CreateEmployeeHandlerTests
{
    [Fact]
    public async Task Deve_retornar_conflict_quando_email_ja_existir()
    {
        var handler = new CreateEmployeeHandler(new FakeEmployeeRepository(true), new FixedClock());

        var result = await handler.HandleAsync(new CreateEmployeeRequest("Lenin", "lenin@vyracare.com", "Admin", "TI", "11999999999", "total", true));

        Assert.False(result.IsSuccess);
        Assert.Equal(UseCaseErrorType.Conflict, result.ErrorType);
    }

    [Fact]
    public async Task Deve_criar_colaborador_quando_email_for_inedito()
    {
        var repository = new FakeEmployeeRepository(false);
        var handler = new CreateEmployeeHandler(repository, new FixedClock());

        var result = await handler.HandleAsync(new CreateEmployeeRequest("Lenin", "lenin@vyracare.com", "Admin", "TI", "11999999999", "total", true));

        Assert.True(result.IsSuccess);
        Assert.Single(repository.Items);
    }

    private sealed class FakeEmployeeRepository : IEmployeeRepository
    {
        private readonly bool _alreadyExists;

        public FakeEmployeeRepository(bool alreadyExists)
        {
            _alreadyExists = alreadyExists;
        }

        public List<Employee> Items { get; } = [];

        public Task<Employee> AddAsync(Employee employee)
        {
            employee.Id ??= Guid.NewGuid().ToString("N");
            Items.Add(employee);
            return Task.FromResult(employee);
        }

        public Task<bool> ExistsByEmailAsync(string email) => Task.FromResult(_alreadyExists);

        public Task<Employee?> GetByEmailAsync(string email) => Task.FromResult<Employee?>(null);

        public Task<Employee?> GetByIdAsync(string id) => Task.FromResult<Employee?>(null);

        public Task<IReadOnlyCollection<Employee>> ListAsync() => Task.FromResult<IReadOnlyCollection<Employee>>(Items);
    }

    private sealed class FixedClock : IClock
    {
        public DateTime UtcNow => new(2026, 5, 30, 12, 0, 0, DateTimeKind.Utc);
    }
}
