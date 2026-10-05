using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Features.Patients.Notes;

public sealed class ListPatientNotesHandler
{
    private readonly IPatientRepository _repository;

    public ListPatientNotesHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<UseCaseResult<IReadOnlyCollection<PatientNote>>> HandleAsync(string patientId)
    {
        var patient = await _repository.GetByIdAsync(patientId);
        return patient is null
            ? UseCaseResult<IReadOnlyCollection<PatientNote>>.Failure(UseCaseErrorType.NotFound, "Patient not found")
            : UseCaseResult<IReadOnlyCollection<PatientNote>>.Success(BuildHistory(patient));
    }

    /// <summary>
    /// Ordena as notas e inclui a abertura derivada para prontuarios criados antes desse evento existir.
    /// </summary>
    private static IReadOnlyCollection<PatientNote> BuildHistory(Patient patient)
    {
        var notes = patient.ProfessionalNotes.ToList();
        if (notes.All(note => note.Kind != PatientNote.RecordOpenedKind))
        {
            notes.Add(new PatientNote
            {
                Id = $"record-opened-{patient.Id}",
                Content = "Prontuário aberto com o cadastro inicial do paciente.",
                ProcedureName = "Abertura do prontuário",
                Kind = PatientNote.RecordOpenedKind,
                AuthorId = "system",
                AuthorName = "Sistema Vyracare",
                CreatedAt = patient.CreatedAt
            });
        }

        return notes.OrderByDescending(note => note.CreatedAt).ToArray();
    }
}
