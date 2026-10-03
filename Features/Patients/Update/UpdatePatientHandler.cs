using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Features.Patients.Update;

public sealed class UpdatePatientHandler
{
    private readonly IPatientRepository _repository;
    private readonly IClock _clock;

    public UpdatePatientHandler(IPatientRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

    public async Task<UseCaseResult<Patient>> HandleAsync(string id, UpdatePatientRequest request)
    {
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(request.FullName) || string.IsNullOrWhiteSpace(request.Cpf))
        {
            return UseCaseResult<Patient>.Failure(UseCaseErrorType.Validation, "Id, FullName and Cpf are required");
        }

        var patient = await _repository.GetByIdAsync(id);
        if (patient is null)
        {
            return UseCaseResult<Patient>.Failure(UseCaseErrorType.NotFound, "Patient not found");
        }

        var normalizedCpf = request.Cpf.Trim();
        if (!string.Equals(patient.Cpf, normalizedCpf, StringComparison.Ordinal) && await _repository.ExistsByCpfAsync(normalizedCpf))
        {
            return UseCaseResult<Patient>.Failure(UseCaseErrorType.Conflict, "Ja existe um paciente cadastrado com este CPF.");
        }

        patient.FullName = request.FullName.Trim();
        patient.BirthDate = request.BirthDate;
        patient.Gender = request.Gender;
        patient.Cpf = normalizedCpf;
        patient.Email = request.Email.Trim();
        patient.Phone = request.Phone.Trim();
        patient.AddressStreet = request.AddressStreet;
        patient.AddressNumber = request.AddressNumber;
        patient.AddressComplement = request.AddressComplement;
        patient.AddressNeighborhood = request.AddressNeighborhood;
        patient.AddressCity = request.AddressCity;
        patient.AddressState = request.AddressState;
        patient.AddressZip = request.AddressZip;
        patient.EmergencyContactName = request.EmergencyContactName;
        patient.EmergencyContactPhone = request.EmergencyContactPhone;
        patient.MainComplaint = request.MainComplaint;
        patient.Objectives = request.Objectives;
        patient.MedicalConditions = request.MedicalConditions;
        patient.Allergies = request.Allergies;
        patient.Medications = request.Medications;
        patient.PreviousSurgeries = request.PreviousSurgeries;
        patient.AestheticProcedures = request.AestheticProcedures;
        patient.SkinType = request.SkinType;
        patient.SunExposure = request.SunExposure;
        patient.Smoking = request.Smoking;
        patient.Alcohol = request.Alcohol;
        patient.PregnantOrBreastfeeding = request.PregnantOrBreastfeeding;
        patient.Consent = request.Consent;
        patient.Notes = request.Notes;
        patient.UpdatedAt = _clock.UtcNow;

        var updated = await _repository.UpdateAsync(patient);
        return updated is null
            ? UseCaseResult<Patient>.Failure(UseCaseErrorType.NotFound, "Patient not found")
            : UseCaseResult<Patient>.Success(updated);
    }
}
