using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vyracare.Api.Client.Infrastructure.Persistence.Documents;

public sealed class EmployeeDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("fullName")]
    public string FullName { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("role")]
    public string Role { get; set; } = string.Empty;

    [BsonElement("department")]
    public string? Department { get; set; }

    [BsonElement("phone")]
    public string? Phone { get; set; }

    [BsonElement("accessLevel")]
    public string AccessLevel { get; set; } = string.Empty;

    [BsonElement("active")]
    public bool Active { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}
