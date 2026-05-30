using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Patients.Create;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Tests.Patients.Create;

public sealed class CreatePatientHandlerTests
{
    [Fact]
    public async Task Deve_retornar_conflict_quando_cpf_ja_existir()
    {
        var handler = new CreatePatientHandler(new FakePatientRepository(true), new FixedClock());

        var result = await handler.HandleAsync(BuildRequest());

        Assert.False(result.IsSuccess);
        Assert.Equal(UseCaseErrorType.Conflict, result.ErrorType);
    }

    [Fact]
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

        public FakePatientRepository(bool alreadyExists)
        {
            _alreadyExists = alreadyExists;
        }

        public List<Patient> Items { get; } = [];

        public Task<Patient> AddAsync(Patient patient)
        {
            patient.Id ??= Guid.NewGuid().ToString("N");
            Items.Add(patient);
            return Task.FromResult(patient);
        }

        public Task<bool> ExistsByCpfAsync(string cpf) => Task.FromResult(_alreadyExists);

        public Task<Patient?> GetByCpfAsync(string cpf) => Task.FromResult<Patient?>(null);

        public Task<Patient?> GetByIdAsync(string id) => Task.FromResult<Patient?>(null);

        public Task<IReadOnlyCollection<Patient>> ListAsync() => Task.FromResult<IReadOnlyCollection<Patient>>(Items);
    }

    private sealed class FixedClock : IClock
    {
        public DateTime UtcNow => new(2026, 5, 30, 12, 0, 0, DateTimeKind.Utc);
    }
}
