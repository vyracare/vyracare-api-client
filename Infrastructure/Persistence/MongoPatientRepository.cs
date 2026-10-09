using MongoDB.Driver;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;
using Vyracare.Api.Client.Infrastructure.Persistence.Documents;
using Vyracare.Api.Client.Common.Tenancy;

namespace Vyracare.Api.Client.Infrastructure.Persistence;

/// <summary>
/// Implementa o acesso aos dados da feature usando a infraestrutura configurada.
/// </summary>
public sealed class MongoPatientRepository : IPatientRepository
{
    private readonly IMongoCollection<PatientDocument> _collection;
    private readonly string _tenantId;

/// <summary>
/// Inicializa uma nova instância de MongoPatientRepository.
/// </summary>
    public MongoPatientRepository(IMongoDatabase database, ITenantContext tenantContext)
    {
        _collection = database.GetCollection<PatientDocument>("patients");
        _tenantId = tenantContext.TenantId;
    }

/// <summary>
/// Recupera a coleção de registros disponíveis para a feature.
/// </summary>
    public async Task<IReadOnlyCollection<Patient>> ListAsync(string? search = null)
    {
        var filter = Builders<PatientDocument>.Filter.Eq(item => item.TenantId, _tenantId);
        if (!string.IsNullOrWhiteSpace(search))
        {
            var pattern = new MongoDB.Bson.BsonRegularExpression(System.Text.RegularExpressions.Regex.Escape(search), "i");
            filter &= Builders<PatientDocument>.Filter.Or(
                Builders<PatientDocument>.Filter.Regex(item => item.FullName, pattern),
                Builders<PatientDocument>.Filter.Regex(item => item.Phone, pattern),
                Builders<PatientDocument>.Filter.Regex(item => item.Email, pattern));
        }

        var documents = await _collection.Find(filter).SortBy(item => item.FullName).ToListAsync();
        return documents.Select(MapToDomain).ToArray();
    }

/// <summary>
/// Recupera um registro específico a partir do identificador informado.
/// </summary>
    public async Task<Patient?> GetByIdAsync(string id)
    {
        var document = await _collection.Find(item => item.TenantId == _tenantId && item.Id == id).FirstOrDefaultAsync();
        return document is null ? null : MapToDomain(document);
    }

/// <summary>
/// Recupera um registro específico a partir do CPF informado.
/// </summary>
    public async Task<Patient?> GetByCpfAsync(string cpf)
    {
        var document = await _collection.Find(item => item.TenantId == _tenantId && item.Cpf == cpf).FirstOrDefaultAsync();
        return document is null ? null : MapToDomain(document);
    }

/// <summary>
/// Executa a responsabilidade do método E xi st sB yC pf As yn c.
/// </summary>
    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        return await _collection.Find(item => item.TenantId == _tenantId && item.Cpf == cpf).AnyAsync();
    }

/// <summary>
/// Persiste um novo registro e devolve a entidade resultante da operação.
/// </summary>
    public async Task<Patient> AddAsync(Patient patient)
    {
        var document = MapToDocument(patient);
        await _collection.InsertOneAsync(document);
        patient.Id = document.Id;
        return patient;
    }

    public async Task<Patient?> UpdateAsync(Patient patient)
    {
        var document = MapToDocument(patient);
        var result = await _collection.ReplaceOneAsync(item => item.TenantId == _tenantId && item.Id == patient.Id, document);
        return result.MatchedCount == 0 ? null : patient;
    }

    public async Task<PatientNote?> AddNoteAsync(string patientId, PatientNote note)
    {
        var noteDocument = MapNoteToDocument(note);
        var update = Builders<PatientDocument>.Update
            .Push(item => item.ProfessionalNotes, noteDocument)
            .Set(item => item.UpdatedAt, note.CreatedAt);
        var result = await _collection.UpdateOneAsync(item => item.TenantId == _tenantId && item.Id == patientId, update);
        return result.MatchedCount == 0 ? null : note;
    }

    private PatientDocument MapToDocument(Patient patient) => new()
    {
        TenantId = _tenantId,
        Id = patient.Id,
        FullName = patient.FullName,
        BirthDate = patient.BirthDate,
        Gender = patient.Gender,
        Cpf = patient.Cpf,
        Email = patient.Email,
        Phone = patient.Phone,
        AddressStreet = patient.AddressStreet,
        AddressNumber = patient.AddressNumber,
        AddressComplement = patient.AddressComplement,
        AddressNeighborhood = patient.AddressNeighborhood,
        AddressCity = patient.AddressCity,
        AddressState = patient.AddressState,
        AddressZip = patient.AddressZip,
        EmergencyContactName = patient.EmergencyContactName,
        EmergencyContactPhone = patient.EmergencyContactPhone,
        MainComplaint = patient.MainComplaint,
        Objectives = patient.Objectives,
        MedicalConditions = patient.MedicalConditions,
        Allergies = patient.Allergies,
        Medications = patient.Medications,
        PreviousSurgeries = patient.PreviousSurgeries,
        AestheticProcedures = patient.AestheticProcedures,
        SkinType = patient.SkinType,
        SunExposure = patient.SunExposure,
        Smoking = patient.Smoking,
        Alcohol = patient.Alcohol,
        PregnantOrBreastfeeding = patient.PregnantOrBreastfeeding,
        Consent = patient.Consent,
        Notes = patient.Notes,
        ProfessionalNotes = patient.ProfessionalNotes.Select(MapNoteToDocument).ToList(),
        CreatedAt = patient.CreatedAt,
        UpdatedAt = patient.UpdatedAt
    };

    private static Patient MapToDomain(PatientDocument document) => new()
    {
        Id = document.Id,
        FullName = document.FullName,
        BirthDate = document.BirthDate,
        Gender = document.Gender,
        Cpf = document.Cpf,
        Email = document.Email,
        Phone = document.Phone,
        AddressStreet = document.AddressStreet,
        AddressNumber = document.AddressNumber,
        AddressComplement = document.AddressComplement,
        AddressNeighborhood = document.AddressNeighborhood,
        AddressCity = document.AddressCity,
        AddressState = document.AddressState,
        AddressZip = document.AddressZip,
        EmergencyContactName = document.EmergencyContactName,
        EmergencyContactPhone = document.EmergencyContactPhone,
        MainComplaint = document.MainComplaint,
        Objectives = document.Objectives,
        MedicalConditions = document.MedicalConditions,
        Allergies = document.Allergies,
        Medications = document.Medications,
        PreviousSurgeries = document.PreviousSurgeries,
        AestheticProcedures = document.AestheticProcedures,
        SkinType = document.SkinType,
        SunExposure = document.SunExposure,
        Smoking = document.Smoking,
        Alcohol = document.Alcohol,
        PregnantOrBreastfeeding = document.PregnantOrBreastfeeding,
        Consent = document.Consent,
        Notes = document.Notes,
        ProfessionalNotes = (document.ProfessionalNotes ?? []).Select(MapNoteToDomain).ToList(),
        CreatedAt = document.CreatedAt,
        UpdatedAt = document.UpdatedAt
    };

    private static PatientNoteDocument MapNoteToDocument(PatientNote note) => new()
    {
        Id = note.Id,
        Content = note.Content,
        ProcedureName = note.ProcedureName,
        AuthorId = note.AuthorId,
        AuthorName = note.AuthorName,
        CreatedAt = note.CreatedAt
    };

    private static PatientNote MapNoteToDomain(PatientNoteDocument note) => new()
    {
        Id = note.Id,
        Content = note.Content,
        ProcedureName = note.ProcedureName,
        AuthorId = note.AuthorId,
        AuthorName = note.AuthorName,
        CreatedAt = note.CreatedAt
    };
}
