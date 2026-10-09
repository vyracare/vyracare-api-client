namespace Vyracare.Api.Client.Features.Patients.Shared.Domain;

public sealed class PatientNote
{
    public const string ProfessionalNoteKind = "professional_note";
    public const string RecordOpenedKind = "record_opened";

    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string Content { get; set; } = string.Empty;
    public string? ProcedureName { get; set; }
    public string Kind { get; set; } = ProfessionalNoteKind;
    public string AuthorId { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
