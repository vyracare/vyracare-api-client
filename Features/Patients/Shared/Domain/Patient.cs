namespace Vyracare.Api.Client.Features.Patients.Shared.Domain;

/// <summary>
/// Representa uma parte da arquitetura desta API.
/// </summary>
public sealed class Patient
{
/// <summary>
/// Identificador do registro ou do recurso processado.
/// </summary>
    public string? Id { get; set; }
/// <summary>
/// Obt?m ou define f ul ln am e.
/// </summary>
    public string FullName { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define b ir th da te.
/// </summary>
    public string BirthDate { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define g en de r.
/// </summary>
    public string Gender { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define c pf.
/// </summary>
    public string Cpf { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define r g.
/// </summary>
    public string? Rg { get; set; }
/// <summary>
/// Obt?m ou define e ma il.
/// </summary>
    public string Email { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define p ho ne.
/// </summary>
    public string Phone { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define w ha ts ap p.
/// </summary>
    public string? Whatsapp { get; set; }
/// <summary>
/// Obt?m ou define a dd re ss st re et.
/// </summary>
    public string AddressStreet { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define a dd re ss nu mb er.
/// </summary>
    public string AddressNumber { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define a dd re ss co mp le me nt.
/// </summary>
    public string? AddressComplement { get; set; }
/// <summary>
/// Obt?m ou define a dd re ss ne ig hb or ho od.
/// </summary>
    public string AddressNeighborhood { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define a dd re ss ci ty.
/// </summary>
    public string AddressCity { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define a dd re ss st at e.
/// </summary>
    public string AddressState { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define a dd re ss zi p.
/// </summary>
    public string AddressZip { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define e me rg en cy co nt ac tn am e.
/// </summary>
    public string EmergencyContactName { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define e me rg en cy co nt ac tp ho ne.
/// </summary>
    public string EmergencyContactPhone { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define m ai nc om pl ai nt.
/// </summary>
    public string MainComplaint { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define o bj ec ti ve s.
/// </summary>
    public string Objectives { get; set; } = string.Empty;
/// <summary>
/// Obt?m ou define m ed ic al co nd it io ns.
/// </summary>
    public string? MedicalConditions { get; set; }
/// <summary>
/// Obt?m ou define a ll er gi es.
/// </summary>
    public string? Allergies { get; set; }
/// <summary>
/// Obt?m ou define m ed ic at io ns.
/// </summary>
    public string? Medications { get; set; }
/// <summary>
/// Obt?m ou define p re vi ou ss ur ge ri es.
/// </summary>
    public string? PreviousSurgeries { get; set; }
/// <summary>
/// Obt?m ou define a es th et ic pr oc ed ur es.
/// </summary>
    public string? AestheticProcedures { get; set; }
/// <summary>
/// Obt?m ou define s ki nt yp e.
/// </summary>
    public string? SkinType { get; set; }
/// <summary>
/// Obt?m ou define s un ex po su re.
/// </summary>
    public string? SunExposure { get; set; }
/// <summary>
/// Obt?m ou define s mo ki ng.
/// </summary>
    public bool Smoking { get; set; }
/// <summary>
/// Obt?m ou define a lc oh ol.
/// </summary>
    public bool Alcohol { get; set; }
/// <summary>
/// Obt?m ou define p re gn an to rb re as tf ee di ng.
/// </summary>
    public bool PregnantOrBreastfeeding { get; set; }
/// <summary>
/// Obt?m ou define c on se nt.
/// </summary>
    public bool Consent { get; set; }
/// <summary>
/// Obt?m ou define n ot es.
/// </summary>
    public string? Notes { get; set; }
/// <summary>
/// Data de cria??o do registro.
/// </summary>
    public DateTime CreatedAt { get; set; }
/// <summary>
/// Data da ?ltima atualiza??o do registro.
/// </summary>
    public DateTime UpdatedAt { get; set; }
}
