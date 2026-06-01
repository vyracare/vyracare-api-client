using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vyracare.Api.Client.Infrastructure.Persistence.Documents;

/// <summary>
/// Representa o documento persistido no MongoDB para esta feature.
/// </summary>
public sealed class EmployeeDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
/// <summary>
/// Obtém ou define o identificador do registro.
/// </summary>
    public string? Id { get; set; }

    [BsonElement("fullName")]
/// <summary>
/// Obtém ou define o nome completo associado ao registro.
/// </summary>
    public string FullName { get; set; } = string.Empty;

    [BsonElement("email")]
/// <summary>
/// Obtém ou define o e-mail associado ao registro.
/// </summary>
    public string Email { get; set; } = string.Empty;

    [BsonElement("role")]
/// <summary>
/// Obtém ou define o papel atribuído ao registro.
/// </summary>
    public string Role { get; set; } = string.Empty;

    [BsonElement("department")]
/// <summary>
/// Obtém ou define o departamento associado ao registro.
/// </summary>
    public string? Department { get; set; }

    [BsonElement("phone")]
/// <summary>
/// Obtém ou define o telefone associado ao registro.
/// </summary>
    public string? Phone { get; set; }

    [BsonElement("accessLevel")]
/// <summary>
/// Obtém ou define o nível de acesso associado ao registro.
/// </summary>
    public string AccessLevel { get; set; } = string.Empty;

    [BsonElement("active")]
/// <summary>
/// Obtém ou define se o registro está ativo.
/// </summary>
    public bool Active { get; set; }

    [BsonElement("createdAt")]
/// <summary>
/// Obtém ou define a data de criação do registro.
/// </summary>
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
/// <summary>
/// Obtém ou define a data da última atualização do registro.
/// </summary>
    public DateTime UpdatedAt { get; set; }
}
