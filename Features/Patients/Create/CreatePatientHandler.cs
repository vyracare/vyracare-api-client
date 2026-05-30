using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Features.Patients.Create;

/// <summary>
/// Implementa a regra de neg?cio do caso de uso representado por esta pasta.
/// </summary>
public sealed class CreatePatientHandler
{
    private readonly IPatientRepository _repository;
    private readonly IClock _clock;

/// <summary>
/// Inicializa uma nova inst?ncia de CreatePatientHandler.
/// </summary>
    public CreatePatientHandler(IPatientRepository repository, IClock clock)
    {
        _repository = repository;
        _clock = clock;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da opera??o.
/// </summary>
    public async Task<UseCaseResult<Patient>> HandleAsync(CreatePatientRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName) || string.IsNullOrWhiteSpace(request.Cpf))
        {
            return UseCaseResult<Patient>.Failure(UseCaseErrorType.Validation, "FullName and Cpf are required");
        }

        if (await _repository.ExistsByCpfAsync(request.Cpf.Trim()))
        {
            return UseCaseResult<Patient>.Failure(UseCaseErrorType.Conflict, "Ja existe um paciente cadastrado com este CPF.");
        }

        var timestamp = _clock.UtcNow;
        var patient = new Patient
        {
            FullName = request.FullName.Trim(),
            BirthDate = request.BirthDate,
            Gender = request.Gender,
            Cpf = request.Cpf.Trim(),
            Rg = request.Rg,
            Email = request.Email,
            Phone = request.Phone,
            Whatsapp = request.Whatsapp,
            AddressStreet = request.AddressStreet,
            AddressNumber = request.AddressNumber,
            AddressComplement = request.AddressComplement,
            AddressNeighborhood = request.AddressNeighborhood,
            AddressCity = request.AddressCity,
            AddressState = request.AddressState,
            AddressZip = request.AddressZip,
            EmergencyContactName = request.EmergencyContactName,
            EmergencyContactPhone = request.EmergencyContactPhone,
            MainComplaint = request.MainComplaint,
            Objectives = request.Objectives,
            MedicalConditions = request.MedicalConditions,
            Allergies = request.Allergies,
            Medications = request.Medications,
            PreviousSurgeries = request.PreviousSurgeries,
            AestheticProcedures = request.AestheticProcedures,
            SkinType = request.SkinType,
            SunExposure = request.SunExposure,
            Smoking = request.Smoking,
            Alcohol = request.Alcohol,
            PregnantOrBreastfeeding = request.PregnantOrBreastfeeding,
            Consent = request.Consent,
            Notes = request.Notes,
            CreatedAt = timestamp,
            UpdatedAt = timestamp
        };

        var created = await _repository.AddAsync(patient);
        return UseCaseResult<Patient>.Success(created);
    }
}
