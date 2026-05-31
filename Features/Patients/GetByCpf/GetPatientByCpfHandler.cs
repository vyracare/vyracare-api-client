using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Features.Patients.GetByCpf;

/// <summary>
/// Implementa o caso de uso correspondente a esta feature.
/// </summary>
public sealed class GetPatientByCpfHandler
{
    private readonly IPatientRepository _repository;

/// <summary>
/// Inicializa uma nova instância de GetPatientByCpfHandler.
/// </summary>
    public GetPatientByCpfHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da operação.
/// </summary>
    public async Task<UseCaseResult<Patient>> HandleAsync(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return UseCaseResult<Patient>.Failure(UseCaseErrorType.Validation, "Cpf is required");
        }

        var patient = await _repository.GetByCpfAsync(cpf.Trim());
        return patient is null
            ? UseCaseResult<Patient>.Failure(UseCaseErrorType.NotFound, "Patient not found")
            : UseCaseResult<Patient>.Success(patient);
    }
}
