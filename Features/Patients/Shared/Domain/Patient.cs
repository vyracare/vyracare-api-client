namespace Vyracare.Api.Client.Features.Patients.Shared.Domain;

public sealed class Patient
{
    public string? Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string BirthDate { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string? Rg { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Whatsapp { get; set; }
    public string AddressStreet { get; set; } = string.Empty;
    public string AddressNumber { get; set; } = string.Empty;
    public string? AddressComplement { get; set; }
    public string AddressNeighborhood { get; set; } = string.Empty;
    public string AddressCity { get; set; } = string.Empty;
    public string AddressState { get; set; } = string.Empty;
    public string AddressZip { get; set; } = string.Empty;
    public string EmergencyContactName { get; set; } = string.Empty;
    public string EmergencyContactPhone { get; set; } = string.Empty;
    public string MainComplaint { get; set; } = string.Empty;
    public string Objectives { get; set; } = string.Empty;
    public string? MedicalConditions { get; set; }
    public string? Allergies { get; set; }
    public string? Medications { get; set; }
    public string? PreviousSurgeries { get; set; }
    public string? AestheticProcedures { get; set; }
    public string? SkinType { get; set; }
    public string? SunExposure { get; set; }
    public bool Smoking { get; set; }
    public bool Alcohol { get; set; }
    public bool PregnantOrBreastfeeding { get; set; }
    public bool Consent { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
