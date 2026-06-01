namespace Vyracare.Api.Client.Features.Patients.Shared.Domain;

/// <summary>
/// Representa a entidade de domínio principal desta feature.
/// </summary>
public sealed class Patient
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
/// Obtém ou define a data de nascimento associada ao registro.
/// </summary>
    public string BirthDate { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o gênero associado ao registro.
/// </summary>
    public string Gender { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o CPF associado ao registro.
/// </summary>
    public string Cpf { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade R g.
/// </summary>
    public string? Rg { get; set; }
/// <summary>
/// Obtém ou define o e-mail associado ao registro.
/// </summary>
    public string Email { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o telefone associado ao registro.
/// </summary>
    public string Phone { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade W ha ts ap p.
/// </summary>
    public string? Whatsapp { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade A dd re ss St re et.
/// </summary>
    public string AddressStreet { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade A dd re ss Nu mb er.
/// </summary>
    public string AddressNumber { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade A dd re ss Co mp le me nt.
/// </summary>
    public string? AddressComplement { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade A dd re ss Ne ig hb or ho od.
/// </summary>
    public string AddressNeighborhood { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade A dd re ss Ci ty.
/// </summary>
    public string AddressCity { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade A dd re ss St at e.
/// </summary>
    public string AddressState { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade A dd re ss Zi p.
/// </summary>
    public string AddressZip { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o nome do contato de emergência.
/// </summary>
    public string EmergencyContactName { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o telefone do contato de emergência.
/// </summary>
    public string EmergencyContactPhone { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade M ai nC om pl ai nt.
/// </summary>
    public string MainComplaint { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade O bj ec ti ve s.
/// </summary>
    public string Objectives { get; set; } = string.Empty;
/// <summary>
/// Obtém ou define o valor da propriedade M ed ic al Co nd it io ns.
/// </summary>
    public string? MedicalConditions { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade A ll er gi es.
/// </summary>
    public string? Allergies { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade M ed ic at io ns.
/// </summary>
    public string? Medications { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade P re vi ou sS ur ge ri es.
/// </summary>
    public string? PreviousSurgeries { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade A es th et ic Pr oc ed ur es.
/// </summary>
    public string? AestheticProcedures { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade S ki nT yp e.
/// </summary>
    public string? SkinType { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade S un Ex po su re.
/// </summary>
    public string? SunExposure { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade S mo ki ng.
/// </summary>
    public bool Smoking { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade A lc oh ol.
/// </summary>
    public bool Alcohol { get; set; }
/// <summary>
/// Obtém ou define o valor da propriedade P re gn an tO rB re as tf ee di ng.
/// </summary>
    public bool PregnantOrBreastfeeding { get; set; }
/// <summary>
/// Obtém ou define se o consentimento correspondente foi registrado.
/// </summary>
    public bool Consent { get; set; }
/// <summary>
/// Obtém ou define as observações associadas ao registro.
/// </summary>
    public string? Notes { get; set; }
/// <summary>
/// Obtém ou define a data de criação do registro.
/// </summary>
    public DateTime CreatedAt { get; set; }
/// <summary>
/// Obtém ou define a data da última atualização do registro.
/// </summary>
    public DateTime UpdatedAt { get; set; }
}
