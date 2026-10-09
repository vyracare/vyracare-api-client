using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vyracare.Api.Client.Infrastructure.Persistence.Documents;

/// <summary>
/// Representa o documento persistido no MongoDB para esta feature.
/// </summary>
[BsonIgnoreExtraElements]
public sealed class PatientDocument
{
    [BsonElement("tenantId")] public string TenantId { get; set; } = string.Empty;
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
/// <summary>
/// Obtém ou define o identificador do registro.
/// </summary>
    public string? Id { get; set; }
    [BsonElement("fullName")] public string FullName { get; set; } = string.Empty;
    [BsonElement("birthDate")] public string BirthDate { get; set; } = string.Empty;
    [BsonElement("gender")] public string Gender { get; set; } = string.Empty;
    [BsonElement("cpf")] public string Cpf { get; set; } = string.Empty;
    [BsonElement("email")] public string Email { get; set; } = string.Empty;
    [BsonElement("phone")] public string Phone { get; set; } = string.Empty;
    [BsonElement("addressStreet")] public string AddressStreet { get; set; } = string.Empty;
    [BsonElement("addressNumber")] public string AddressNumber { get; set; } = string.Empty;
    [BsonElement("addressComplement")] public string? AddressComplement { get; set; }
    [BsonElement("addressNeighborhood")] public string AddressNeighborhood { get; set; } = string.Empty;
    [BsonElement("addressCity")] public string AddressCity { get; set; } = string.Empty;
    [BsonElement("addressState")] public string AddressState { get; set; } = string.Empty;
    [BsonElement("addressZip")] public string AddressZip { get; set; } = string.Empty;
    [BsonElement("emergencyContactName")] public string EmergencyContactName { get; set; } = string.Empty;
    [BsonElement("emergencyContactPhone")] public string EmergencyContactPhone { get; set; } = string.Empty;
    [BsonElement("mainComplaint")] public string MainComplaint { get; set; } = string.Empty;
    [BsonElement("objectives")] public string Objectives { get; set; } = string.Empty;
    [BsonElement("medicalConditions")] public string? MedicalConditions { get; set; }
    [BsonElement("allergies")] public string? Allergies { get; set; }
    [BsonElement("medications")] public string? Medications { get; set; }
    [BsonElement("previousSurgeries")] public string? PreviousSurgeries { get; set; }
    [BsonElement("aestheticProcedures")] public string? AestheticProcedures { get; set; }
    [BsonElement("skinType")] public string? SkinType { get; set; }
    [BsonElement("sunExposure")] public string? SunExposure { get; set; }
    [BsonElement("smoking")] public bool Smoking { get; set; }
    [BsonElement("alcohol")] public bool Alcohol { get; set; }
    [BsonElement("pregnantOrBreastfeeding")] public bool PregnantOrBreastfeeding { get; set; }
    [BsonElement("consent")] public bool Consent { get; set; }
    [BsonElement("notes")] public string? Notes { get; set; }
    [BsonElement("professionalNotes")] public List<PatientNoteDocument> ProfessionalNotes { get; set; } = [];
    [BsonElement("createdAt")] public DateTime CreatedAt { get; set; }
    [BsonElement("updatedAt")] public DateTime UpdatedAt { get; set; }
}

public sealed class PatientNoteDocument
{
    [BsonElement("id")] public string Id { get; set; } = string.Empty;
    [BsonElement("content")] public string Content { get; set; } = string.Empty;
    [BsonElement("procedureName")] public string? ProcedureName { get; set; }
    [BsonElement("authorId")] public string AuthorId { get; set; } = string.Empty;
    [BsonElement("authorName")] public string AuthorName { get; set; } = string.Empty;
    [BsonElement("createdAt")] public DateTime CreatedAt { get; set; }
}
