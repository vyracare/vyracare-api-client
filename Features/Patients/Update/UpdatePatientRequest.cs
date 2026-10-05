namespace Vyracare.Api.Client.Features.Patients.Update;

public sealed record UpdatePatientRequest(
    string FullName,
    string BirthDate,
    string Gender,
    string Email,
    string Phone,
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
    string? SunExposure,
    bool Smoking,
    bool Alcohol,
    bool PregnantOrBreastfeeding
);
