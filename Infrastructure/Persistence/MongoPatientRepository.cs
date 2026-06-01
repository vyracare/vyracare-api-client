using MongoDB.Driver;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;
using Vyracare.Api.Client.Infrastructure.Persistence.Documents;

namespace Vyracare.Api.Client.Infrastructure.Persistence;

/// <summary>
/// Implementa o acesso aos dados da feature usando a infraestrutura configurada.
/// </summary>
public sealed class MongoPatientRepository : IPatientRepository
{
    private readonly IMongoCollection<PatientDocument> _collection;

/// <summary>
/// Inicializa uma nova instância de MongoPatientRepository.
/// </summary>
    public MongoPatientRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<PatientDocument>("patients");
    }

/// <summary>
/// Recupera a coleção de registros disponíveis para a feature.
/// </summary>
    public async Task<IReadOnlyCollection<Patient>> ListAsync()
    {
        var documents = await _collection.Find(Builders<PatientDocument>.Filter.Empty).ToListAsync();
        return documents.Select(MapToDomain).ToArray();
    }

/// <summary>
/// Recupera um registro específico a partir do identificador informado.
/// </summary>
    public async Task<Patient?> GetByIdAsync(string id)
    {
        var document = await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
        return document is null ? null : MapToDomain(document);
    }

/// <summary>
/// Recupera um registro específico a partir do CPF informado.
/// </summary>
    public async Task<Patient?> GetByCpfAsync(string cpf)
    {
        var document = await _collection.Find(item => item.Cpf == cpf).FirstOrDefaultAsync();
        return document is null ? null : MapToDomain(document);
    }

/// <summary>
/// Executa a responsabilidade do método E xi st sB yC pf As yn c.
/// </summary>
    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        return await _collection.Find(item => item.Cpf == cpf).AnyAsync();
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

    private static PatientDocument MapToDocument(Patient patient) => new()
    {
        Id = patient.Id,
        FullName = patient.FullName,
        BirthDate = patient.BirthDate,
        Gender = patient.Gender,
        Cpf = patient.Cpf,
        Rg = patient.Rg,
        Email = patient.Email,
        Phone = patient.Phone,
        Whatsapp = patient.Whatsapp,
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
        Rg = document.Rg,
        Email = document.Email,
        Phone = document.Phone,
        Whatsapp = document.Whatsapp,
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
        CreatedAt = document.CreatedAt,
        UpdatedAt = document.UpdatedAt
    };
}
