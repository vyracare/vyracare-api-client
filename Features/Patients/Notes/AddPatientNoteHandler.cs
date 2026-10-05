using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Features.Patients.Notes;

public sealed class AddPatientNoteHandler
{
    private readonly IPatientRepository _repository;
    private readonly IClock _clock;

    public AddPatientNoteHandler(IPatientRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task<UseCaseResult<PatientNote>> HandleAsync(
        string patientId,
        AddPatientNoteRequest request,
        string authorId,
        string authorName)
    {
        if (string.IsNullOrWhiteSpace(patientId) || string.IsNullOrWhiteSpace(request.Content))
        {
            return UseCaseResult<PatientNote>.Failure(UseCaseErrorType.Validation, "PatientId and Content are required");
        }

        var note = new PatientNote
        {
            Content = request.Content.Trim(),
            ProcedureName = request.ProcedureName?.Trim(),
            Kind = PatientNote.ProfessionalNoteKind,
            AuthorId = authorId,
            AuthorName = string.IsNullOrWhiteSpace(authorName) ? "Funcionario" : authorName,
            CreatedAt = _clock.UtcNow
        };

        var created = await _repository.AddNoteAsync(patientId, note);
        return created is null
            ? UseCaseResult<PatientNote>.Failure(UseCaseErrorType.NotFound, "Patient not found")
            : UseCaseResult<PatientNote>.Success(created);
    }
}
