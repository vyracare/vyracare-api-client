using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;
using Vyracare.Api.Client.Features.Patients.Update;

namespace Vyracare.Api.Client.Tests.Patients.Update;

public sealed class UpdatePatientHandlerTests
{
    [Fact]
    public async Task Deve_atualizar_campos_permitidos_e_preservar_dados_imutaveis()
    {
        var patient = BuildPatient();
        var repository = new FakePatientRepository(patient);
        var handler = new UpdatePatientHandler(repository, new FixedClock());

        var result = await handler.HandleAsync(patient.Id!, BuildRequest());

        Assert.True(result.IsSuccess);
        Assert.Equal("Nome atualizado", patient.FullName);
        Assert.Equal("novo@vyracare.com", patient.Email);
        Assert.Equal("123.456.789-00", patient.Cpf);
        Assert.Equal("Mista", patient.SkinType);
        Assert.True(patient.Consent);
        Assert.Equal("Nota original", patient.Notes);
        Assert.Single(patient.ProfessionalNotes);
        Assert.Equal(new FixedClock().UtcNow, patient.UpdatedAt);
    }

    [Fact]
    public async Task Deve_retornar_not_found_quando_paciente_nao_existir()
    {
        var handler = new UpdatePatientHandler(new FakePatientRepository(null), new FixedClock());

        var result = await handler.HandleAsync("missing", BuildRequest());

        Assert.False(result.IsSuccess);
    }

    private static Patient BuildPatient() => new()
    {
        Id = "patient-1",
        FullName = "Nome original",
        Cpf = "123.456.789-00",
        SkinType = "Mista",
        Consent = true,
        Notes = "Nota original",
        ProfessionalNotes = [new PatientNote { Content = "Historico original" }],
        CreatedAt = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc),
        UpdatedAt = new DateTime(2026, 10, 1, 12, 0, 0, DateTimeKind.Utc)
    };

    private static UpdatePatientRequest BuildRequest() => new(
        "Nome atualizado",
        "1990-01-01",
        "Feminino",
        "novo@vyracare.com",
        "11999999999",
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
        "Moderada",
        false,
        false,
        false);

    private sealed class FakePatientRepository(Patient? patient) : IPatientRepository
    {
        public Task<Patient> AddAsync(Patient value) => Task.FromResult(value);
        public Task<bool> ExistsByCpfAsync(string cpf) => Task.FromResult(false);
        public Task<Patient?> GetByCpfAsync(string cpf) => Task.FromResult<Patient?>(null);
        public Task<Patient?> GetByIdAsync(string id) => Task.FromResult(patient?.Id == id ? patient : null);
        public Task<IReadOnlyCollection<Patient>> ListAsync(string? search = null) =>
            Task.FromResult<IReadOnlyCollection<Patient>>(patient is null ? [] : [patient]);
        public Task<Patient?> UpdateAsync(Patient value) => Task.FromResult<Patient?>(value);
        public Task<PatientNote?> AddNoteAsync(string patientId, PatientNote note) => Task.FromResult<PatientNote?>(note);
    }

    private sealed class FixedClock : IClock
    {
        public DateTime UtcNow => new(2026, 10, 5, 15, 0, 0, DateTimeKind.Utc);
    }
}
