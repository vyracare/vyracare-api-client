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
        var openingNote = notes.FirstOrDefault(note => note.Kind == PatientNote.RecordOpenedKind);
        if (openingNote is null)
        {
            notes.Add(new PatientNote
            {
                Id = $"record-opened-{patient.Id}",
                Content = ResolveOpeningContent(patient.Notes),
                ProcedureName = "Abertura do prontuário",
                Kind = PatientNote.RecordOpenedKind,
                AuthorId = "system",
                AuthorName = "Sistema Vyracare",
                CreatedAt = patient.CreatedAt
            });
        }
        else if (!string.IsNullOrWhiteSpace(patient.Notes))
        {
            openingNote.Content = patient.Notes.Trim();
        }

        return notes.OrderByDescending(note => note.CreatedAt).ToArray();
    }

    /// <summary>
    /// Normaliza a nota inicial usada na abertura derivada de prontuarios antigos.
    /// </summary>
    private static string ResolveOpeningContent(string? content) =>
        string.IsNullOrWhiteSpace(content)
            ? "Nenhuma nota registrada na abertura do prontuário."
            : content.Trim();
}
