namespace Vyracare.Api.Client.Features.Employees.Shared.Domain;

/// <summary>
/// Representa a entidade de domínio principal desta feature.
/// </summary>
public sealed class Employee
{
/// <summary>
/// Obtém ou define o identificador do registro.
/// </summary>
    public string? Id { get; set; }
/// <summary>
/// Obtém ou define o nome completo associado ao registro.
/// </summary>
    public string FullName { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o e-mail associado ao registro.
/// </summary>
    public string Email { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o papel atribuído ao registro.
/// </summary>
    public string Role { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o departamento associado ao registro.
/// </summary>
    public string? Department { get; set; }
/// <summary>
/// Obtém ou define o telefone associado ao registro.
/// </summary>
    public string? Phone { get; set; }
/// <summary>
/// Obtém ou define o nível de acesso associado ao registro.
/// </summary>
    public string AccessLevel { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define se o registro está ativo.
/// </summary>
    public bool Active { get; set; }
/// <summary>
/// Obtém ou define a data de criação do registro.
/// </summary>
    public DateTime CreatedAt { get; set; }
/// <summary>
/// Obtém ou define a data da última atualização do registro.
/// </summary>
    public DateTime UpdatedAt { get; set; }
}
