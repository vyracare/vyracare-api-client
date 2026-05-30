using Vyracare.Api.Client.Common.Results;
using Vyracare.Api.Client.Features.Patients.Shared.Domain;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;

namespace Vyracare.Api.Client.Features.Patients.List;

public sealed class ListPatientsHandler
{
    private readonly IPatientRepository _repository;

    public ListPatientsHandler(IPatientRepository repository)
    {
        _repository = repository;
    }

    public async Task<UseCaseResult<IReadOnlyCollection<Patient>>> HandleAsync()
    {
        var patients = await _repository.ListAsync();
        return UseCaseResult<IReadOnlyCollection<Patient>>.Success(patients);
    }
}
