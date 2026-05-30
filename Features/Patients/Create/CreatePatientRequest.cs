namespace Vyracare.Api.Client.Features.Patients.Create;

/// <summary>
/// Define o contrato de entrada esperado por este caso de uso.
/// </summary>
public sealed record CreatePatientRequest(
    string FullName,
    string BirthDate,
    string Gender,
    string Cpf,
    string? Rg,
    string Email,
    string Phone,
    string? Whatsapp,
    string AddressStreet,
    string AddressNumber,
    string? AddressComplement,
    string AddressNeighborhood,
    string AddressCity,
    string AddressState,
    string AddressZip,
    string EmergencyContactName,
    string EmergencyContactPhone,
    string MainComplaint,
    string Objectives,
    string? MedicalConditions,
    string? Allergies,
    string? Medications,
    string? PreviousSurgeries,
    string? AestheticProcedures,
    string? SkinType,
    string? SunExposure,
    bool Smoking,
    bool Alcohol,
    bool PregnantOrBreastfeeding,
    bool Consent,
    string? Notes
);
