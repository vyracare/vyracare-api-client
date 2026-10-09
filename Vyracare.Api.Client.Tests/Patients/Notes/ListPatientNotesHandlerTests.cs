using Vyracare.Api.Client.Features.Patients.Notes;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Tests.Patients.Notes;

public sealed class ListPatientNotesHandlerTests
{
    [Fact]
    public async Task Deve_incluir_abertura_derivada_em_prontuario_antigo()
    {
        var patient = BuildPatient();
        var result = await new ListPatientNotesHandler(new FakePatientRepository(patient)).HandleAsync(patient.Id!);

        Assert.True(result.IsSuccess);
        var openingNote = Assert.Single(result.Value!);
        Assert.Equal(PatientNote.RecordOpenedKind, openingNote.Kind);
        Assert.Equal("Nota inicial do paciente", openingNote.Content);
        Assert.Equal(patient.CreatedAt, openingNote.CreatedAt);
        Assert.Equal("Sistema Vyracare", openingNote.AuthorName);
    }

    [Fact]
    public async Task Nao_deve_duplicar_abertura_ja_persistida()
    {
        var patient = BuildPatient();
        patient.ProfessionalNotes.Add(new PatientNote
        {
            Kind = PatientNote.RecordOpenedKind,
            Content = "Prontuario aberto com o cadastro inicial do paciente.",
            CreatedAt = patient.CreatedAt
        });

        var result = await new ListPatientNotesHandler(new FakePatientRepository(patient)).HandleAsync(patient.Id!);

        Assert.True(result.IsSuccess);
        var openingNote = Assert.Single(result.Value!);
        Assert.Equal("Nota inicial do paciente", openingNote.Content);
    }

    private static Patient BuildPatient() => new()
    {
        Id = "patient-1",
        FullName = "Paciente",
        Notes = "  Nota inicial do paciente  ",
        CreatedAt = new DateTime(2026, 10, 5, 12, 0, 0, DateTimeKind.Utc),
        UpdatedAt = new DateTime(2026, 10, 5, 12, 0, 0, DateTimeKind.Utc)
    };

    private sealed class FakePatientRepository(Patient patient) : IPatientRepository
    {
        public Task<Patient> AddAsync(Patient value) => Task.FromResult(value);
        public Task<bool> ExistsByCpfAsync(string cpf) => Task.FromResult(false);
        public Task<Patient?> GetByCpfAsync(string cpf) => Task.FromResult<Patient?>(null);
        public Task<Patient?> GetByIdAsync(string id) => Task.FromResult<Patient?>(id == patient.Id ? patient : null);
        public Task<IReadOnlyCollection<Patient>> ListAsync(string? search = null) =>
            Task.FromResult<IReadOnlyCollection<Patient>>([patient]);
        public Task<Patient?> UpdateAsync(Patient value) => Task.FromResult<Patient?>(value);
        public Task<PatientNote?> AddNoteAsync(string patientId, PatientNote note) => Task.FromResult<PatientNote?>(note);
    }
}
