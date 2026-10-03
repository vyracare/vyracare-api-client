namespace Vyracare.Api.Client.Features.Patients.Notes;

public sealed record AddPatientNoteRequest(string Content, string? ProcedureName);
