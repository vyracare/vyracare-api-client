using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Features.Patients.List;

/// <summary>
/// Implementa a regra de neg?cio do caso de uso representado por esta pasta.
/// </summary>
public sealed class ListPatientsHandler
{
    private readonly IPatientRepository _repository;

/// <summary>
/// Inicializa uma nova inst?ncia de ListPatientsHandler.
/// </summary>
    public ListPatientsHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

/// <summary>
/// Executa o caso de uso e devolve o resultado padronizado da opera??o.
/// </summary>
    public async Task<UseCaseResult<IReadOnlyCollection<Patient>>> HandleAsync()
    {
        var patients = await _repository.ListAsync();
        return UseCaseResult<IReadOnlyCollection<Patient>>.Success(patients);
    }
}
