using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Patients.Create;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Tests.Patients.Create;

/// <summary>
/// Representa o componente CreatePatientHandlerTests da aplicação.
/// </summary>
public sealed class CreatePatientHandlerTests
{
    [Fact]
/// <summary>
/// Executa a responsabilidade do método D ev e_r et or na r_c on fl ic t_q ua nd o_c pf_j a_e xi st ir.
/// </summary>
    public async Task Deve_retornar_conflict_quando_cpf_ja_existir()
    {
        var handler = new CreatePatientHandler(new FakePatientRepository(true), new FixedClock());

        var result = await handler.HandleAsync(BuildRequest());

        Assert.False(result.IsSuccess);
        Assert.Equal(UseCaseErrorType.Conflict, result.ErrorType);
    }

    [Fact]
/// <summary>
/// Executa a responsabilidade do método D ev e_c ri ar_p ac ie nt e_q ua nd o_c pf_f or_i ne di to.
/// </summary>
    public async Task Deve_criar_paciente_quando_cpf_for_inedito()
    {
        var repository = new FakePatientRepository(false);
        var handler = new CreatePatientHandler(repository, new FixedClock());

        var result = await handler.HandleAsync(BuildRequest());

        Assert.True(result.IsSuccess);
        Assert.Single(repository.Items);
    }

    private static CreatePatientRequest BuildRequest() => new(
        "Paciente",
        "1990-01-01",
        "Feminino",
        "12345678900",
        null,
        "paciente@vyracare.com",
        "11999999999",
        null,
        "Rua A",
        "100",
        null,
        "Centro",
        "Sao Paulo",
        "SP",
        "01000-000",
        "Contato",
        "11888888888",
        "Queixa",
        "Objetivo",
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        false,
        false,
        false,
        true,
        null
    );

    private sealed class FakePatientRepository : IPatientRepository
    {
        private readonly bool _alreadyExists;

/// <summary>
/// Inicializa uma nova instância de FakePatientRepository.
/// </summary>
        public FakePatientRepository(bool alreadyExists)
        {
            _alreadyExists = alreadyExists;
        }

/// <summary>
/// Obtém ou define a coleção de itens usada no contexto do teste.
/// </summary>
        public List<Patient> Items { get; } = [];

/// <summary>
/// Persiste um novo registro e devolve a entidade resultante da operação.
/// </summary>
        public Task<Patient> AddAsync(Patient patient)
        {
            patient.Id ??= Guid.NewGuid().ToString("N");
            Items.Add(patient);
            return Task.FromResult(patient);
        }

/// <summary>
/// Executa a responsabilidade do método E xi st sB yC pf As yn c.
/// </summary>
        public Task<bool> ExistsByCpfAsync(string cpf) => Task.FromResult(_alreadyExists);

/// <summary>
/// Recupera um registro específico a partir do CPF informado.
/// </summary>
        public Task<Patient?> GetByCpfAsync(string cpf) => Task.FromResult<Patient?>(null);

/// <summary>
/// Recupera um registro específico a partir do identificador informado.
/// </summary>
        public Task<Patient?> GetByIdAsync(string id) => Task.FromResult<Patient?>(null);

/// <summary>
/// Recupera a coleção de registros disponíveis para a feature.
/// </summary>
        public Task<IReadOnlyCollection<Patient>> ListAsync() => Task.FromResult<IReadOnlyCollection<Patient>>(Items);
    }

    private sealed class FixedClock : IClock
    {
        public DateTime UtcNow => new(2026, 5, 30, 12, 0, 0, DateTimeKind.Utc);
    }
}
