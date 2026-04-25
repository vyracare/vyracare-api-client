using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vyracare.Api.Client.Models;

public class PatientModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("fullName")]
    public string FullName { get; set; } = null!;

    [BsonElement("birthDate")]
    public string BirthDate { get; set; } = null!;

    [BsonElement("gender")]
    public string Gender { get; set; } = null!;

    [BsonElement("cpf")]
    public string Cpf { get; set; } = null!;

    [BsonElement("rg")]
    public string? Rg { get; set; }

    [BsonElement("email")]
    public string Email { get; set; } = null!;

    [BsonElement("phone")]
    public string Phone { get; set; } = null!;

    [BsonElement("whatsapp")]
    public string? Whatsapp { get; set; }

    [BsonElement("addressStreet")]
    public string AddressStreet { get; set; } = null!;

    [BsonElement("addressNumber")]
    public string AddressNumber { get; set; } = null!;

    [BsonElement("addressComplement")]
    public string? AddressComplement { get; set; }

    [BsonElement("addressNeighborhood")]
    public string AddressNeighborhood { get; set; } = null!;

    [BsonElement("addressCity")]
    public string AddressCity { get; set; } = null!;

    [BsonElement("addressState")]
    public string AddressState { get; set; } = null!;

    [BsonElement("addressZip")]
    public string AddressZip { get; set; } = null!;

    [BsonElement("emergencyContactName")]
    public string EmergencyContactName { get; set; } = null!;

    [BsonElement("emergencyContactPhone")]
    public string EmergencyContactPhone { get; set; } = null!;

    [BsonElement("mainComplaint")]
    public string MainComplaint { get; set; } = null!;

    [BsonElement("objectives")]
    public string Objectives { get; set; } = null!;

    [BsonElement("medicalConditions")]
    public string? MedicalConditions { get; set; }

    [BsonElement("allergies")]
    public string? Allergies { get; set; }

    [BsonElement("medications")]
    public string? Medications { get; set; }

    [BsonElement("previousSurgeries")]
    public string? PreviousSurgeries { get; set; }

    [BsonElement("aestheticProcedures")]
    public string? AestheticProcedures { get; set; }

    [BsonElement("skinType")]
    public string? SkinType { get; set; }

    [BsonElement("sunExposure")]
    public string? SunExposure { get; set; }

    [BsonElement("smoking")]
    public bool Smoking { get; set; }

    [BsonElement("alcohol")]
    public bool Alcohol { get; set; }

    [BsonElement("pregnantOrBreastfeeding")]
    public bool PregnantOrBreastfeeding { get; set; }

    [BsonElement("consent")]
    public bool Consent { get; set; }

    [BsonElement("notes")]
    public string? Notes { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
