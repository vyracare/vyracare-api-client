using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Features.Patients.GetById;

/// <summary>
/// Implementa o caso de uso correspondente a esta feature.
/// </summary>
public sealed class GetPatientByIdHandler
{
    private readonly IPatientRepository _repository;

/// <summary>
/// Inicializa uma nova instância de GetPatientByIdHandler.
/// </summary>
    public GetPatientByIdHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da operação.
/// </summary>
    public async Task<UseCaseResult<Patient>> HandleAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return UseCaseResult<Patient>.Failure(UseCaseErrorType.Validation, "Id is required");
        }

        var patient = await _repository.GetByIdAsync(id);
        return patient is null
            ? UseCaseResult<Patient>.Failure(UseCaseErrorType.NotFound, "Patient not found")
            : UseCaseResult<Patient>.Success(patient);
    }
}
