using Vyracare.Api.Client.Features.Patients.Shared.Domain;

namespace Vyracare.Api.Client.Features.Patients.Shared.Ports;

/// <summary>
/// Implementa a integra??o com a persist?ncia ou com uma depend?ncia externa da aplica??o.
/// </summary>
public interface IPatientRepository
{
    Task<IReadOnlyCollection<Patient>> ListAsync();
    Task<Patient?> GetByIdAsync(string id);
    Task<Patient?> GetByCpfAsync(string cpf);
    Task<bool> ExistsByCpfAsync(string cpf);
    Task<Patient> AddAsync(Patient patient);
}
