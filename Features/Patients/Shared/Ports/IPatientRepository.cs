using Vyracare.Api.Client.Features.Patients.Shared.Domain;

namespace Vyracare.Api.Client.Features.Patients.Shared.Ports;

/// <summary>
/// Define o contrato de persistência usado pela feature.
/// </summary>
public interface IPatientRepository
{
    Task<IReadOnlyCollection<Patient>> ListAsync();
    Task<Patient?> GetByIdAsync(string id);
    Task<Patient?> GetByCpfAsync(string cpf);
    Task<bool> ExistsByCpfAsync(string cpf);
    Task<Patient> AddAsync(Patient patient);
}
