using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Employees.Create;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;

namespace Vyracare.Api.Client.Tests.Employees.Create;

/// <summary>
/// Representa o componente CreateEmployeeHandlerTests da aplicação.
/// </summary>
public sealed class CreateEmployeeHandlerTests
{
    [Fact]
/// <summary>
/// Executa a responsabilidade do método D ev e_r et or na r_c on fl ic t_q ua nd o_e ma il_j a_e xi st ir.
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
/// Executa a responsabilidade do método D ev e_c ri ar_c ol ab or ad or_q ua nd o_e ma il_f or_i ne di to.
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
/// Inicializa uma nova instância de FakeEmployeeRepository.
/// </summary>
        public FakeEmployeeRepository(bool alreadyExists)
        {
            _alreadyExists = alreadyExists;
        }

/// <summary>
/// Obtém ou define a coleção de itens usada no contexto do teste.
/// </summary>
        public List<Employee> Items { get; } = [];

/// <summary>
/// Persiste um novo registro e devolve a entidade resultante da operação.
/// </summary>
        public Task<Employee> AddAsync(Employee employee)
        {
            employee.Id ??= Guid.NewGuid().ToString("N");
            Items.Add(employee);
            return Task.FromResult(employee);
        }

/// <summary>
/// Executa a responsabilidade do método E xi st sB yE ma il As yn c.
/// </summary>
        public Task<bool> ExistsByEmailAsync(string email) => Task.FromResult(_alreadyExists);

/// <summary>
/// Recupera um registro específico a partir do e-mail informado.
/// </summary>
        public Task<Employee?> GetByEmailAsync(string email) => Task.FromResult<Employee?>(null);

/// <summary>
/// Recupera um registro específico a partir do identificador informado.
/// </summary>
        public Task<Employee?> GetByIdAsync(string id) => Task.FromResult<Employee?>(null);

/// <summary>
/// Recupera a coleção de registros disponíveis para a feature.
/// </summary>
        public Task<IReadOnlyCollection<Employee>> ListAsync() => Task.FromResult<IReadOnlyCollection<Employee>>(Items);
    }

    private sealed class FixedClock : IClock
    {
        public DateTime UtcNow => new(2026, 5, 30, 12, 0, 0, DateTimeKind.Utc);
    }
}
