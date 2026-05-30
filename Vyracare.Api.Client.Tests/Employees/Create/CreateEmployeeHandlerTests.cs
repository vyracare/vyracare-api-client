using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Employees.Create;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Tests.Employees.Create;

/// <summary>
/// Agrupa os cen?rios de teste unit?rio relacionados a este componente.
/// </summary>
public sealed class CreateEmployeeHandlerTests
{
    [Fact]
/// <summary>
/// Executa a responsabilidade associada a d ev e r et or na r c on fl ic t q ua nd o e ma il j a e xi st ir.
/// </summary>
    public async Task Deve_retornar_conflict_quando_email_ja_existir()
    {
        var handler = new CreateEmployeeHandler(new FakeEmployeeRepository(true), new FixedClock());

        var result = await handler.HandleAsync(new CreateEmployeeRequest("Lenin", "lenin@vyracare.com", "Admin", "TI", "11999999999", "total", true));

        Assert.False(result.IsSuccess);
        Assert.Equal(UseCaseErrorType.Conflict, result.ErrorType);
    }

    [Fact]
/// <summary>
/// Executa a responsabilidade associada a d ev e c ri ar c ol ab or ad or q ua nd o e ma il f or i ne di to.
/// </summary>
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

/// <summary>
/// Executa a responsabilidade associada a f ak ee mp lo ye er ep os it or y.
/// </summary>
        public FakeEmployeeRepository(bool alreadyExists)
        {
            _alreadyExists = alreadyExists;
        }

/// <summary>
/// Obt?m ou define i te ms.
/// </summary>
        public List<Employee> Items { get; } = [];

/// <summary>
/// Persiste um novo registro e devolve a entidade resultante da opera??o.
/// </summary>
        public Task<Employee> AddAsync(Employee employee)
        {
            employee.Id ??= Guid.NewGuid().ToString("N");
            Items.Add(employee);
            return Task.FromResult(employee);
        }

/// <summary>
/// Verifica se j? existe um colaborador cadastrado com o e-mail informado.
/// </summary>
        public Task<bool> ExistsByEmailAsync(string email) => Task.FromResult(_alreadyExists);

/// <summary>
/// Recupera um colaborador ou usu?rio a partir do e-mail informado.
/// </summary>
        public Task<Employee?> GetByEmailAsync(string email) => Task.FromResult<Employee?>(null);

/// <summary>
/// Recupera um registro espec?fico a partir do seu identificador.
/// </summary>
        public Task<Employee?> GetByIdAsync(string id) => Task.FromResult<Employee?>(null);

/// <summary>
/// Recupera a cole??o de registros dispon?veis para esta feature.
/// </summary>
        public Task<IReadOnlyCollection<Employee>> ListAsync() => Task.FromResult<IReadOnlyCollection<Employee>>(Items);
    }

    private sealed class FixedClock : IClock
    {
        public DateTime UtcNow => new(2026, 5, 30, 12, 0, 0, DateTimeKind.Utc);
    }
}
