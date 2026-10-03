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
            : UseCaseResult<IReadOnlyCollection<PatientNote>>.Success(
                patient.ProfessionalNotes.OrderByDescending(note => note.CreatedAt).ToArray());
    }
}
