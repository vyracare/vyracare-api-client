using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Patients.Notes;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Tests.Patients.Notes;

public sealed class AddPatientNoteHandlerTests
{
    [Fact]
    public async Task Deve_registrar_autor_procedimento_e_data_na_nota()
    {
        var repository = new FakeRepository();
        var handler = new AddPatientNoteHandler(repository, new FixedClock());

        var result = await handler.HandleAsync(
            "patient-1",
            new AddPatientNoteRequest("Boa evolucao", "Peeling"),
            "employee-1",
            "Ana Clinica");

        Assert.True(result.IsSuccess);
        Assert.Equal("Ana Clinica", result.Value!.AuthorName);
        Assert.Equal("Peeling", result.Value.ProcedureName);
        Assert.Equal(new DateTime(2026, 10, 3, 12, 0, 0, DateTimeKind.Utc), result.Value.CreatedAt);
    }

    private sealed class FixedClock : IClock
    {
        public DateTime UtcNow => new(2026, 10, 3, 12, 0, 0, DateTimeKind.Utc);
    }

    private sealed class FakeRepository : IPatientRepository
    {
        public Task<PatientNote?> AddNoteAsync(string patientId, PatientNote note) => Task.FromResult<PatientNote?>(note);
        public Task<Patient> AddAsync(Patient patient) => Task.FromResult(patient);
        public Task<bool> ExistsByCpfAsync(string cpf) => Task.FromResult(false);
        public Task<Patient?> GetByCpfAsync(string cpf) => Task.FromResult<Patient?>(null);
        public Task<Patient?> GetByIdAsync(string id) => Task.FromResult<Patient?>(null);
        public Task<IReadOnlyCollection<Patient>> ListAsync(string? search = null) => Task.FromResult<IReadOnlyCollection<Patient>>([]);
        public Task<Patient?> UpdateAsync(Patient patient) => Task.FromResult<Patient?>(patient);
    }
}
