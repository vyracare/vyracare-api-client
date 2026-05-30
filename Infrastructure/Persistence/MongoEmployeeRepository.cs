using MongoDB.Driver;
using Vyracare.Api.Client.Features.Employees.Shared.Domain;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;
using Vyracare.Api.Client.Infrastructure.Persistence.Documents;

namespace Vyracare.Api.Client.Infrastructure.Persistence;

/// <summary>
/// Implementa a integra??o com a persist?ncia ou com uma depend?ncia externa da aplica??o.
/// </summary>
public sealed class MongoEmployeeRepository : IEmployeeRepository
{
    private readonly IMongoCollection<EmployeeDocument> _collection;

/// <summary>
/// Inicializa uma nova inst?ncia de MongoEmployeeRepository.
/// </summary>
    public MongoEmployeeRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<EmployeeDocument>("employees");
    }

/// <summary>
/// Recupera a cole??o de registros dispon?veis para esta feature.
/// </summary>
    public async Task<IReadOnlyCollection<Employee>> ListAsync()
    {
        var documents = await _collection.Find(Builders<EmployeeDocument>.Filter.Empty).ToListAsync();
        return documents.Select(MapToDomain).ToArray();
    }

/// <summary>
/// Recupera um registro espec?fico a partir do seu identificador.
/// </summary>
    public async Task<Employee?> GetByIdAsync(string id)
    {
        var document = await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();
        return document is null ? null : MapToDomain(document);
    }

/// <summary>
/// Recupera um colaborador ou usu?rio a partir do e-mail informado.
/// </summary>
    public async Task<Employee?> GetByEmailAsync(string email)
    {
        var document = await _collection.Find(item => item.Email == email).FirstOrDefaultAsync();
        return document is null ? null : MapToDomain(document);
    }

/// <summary>
/// Verifica se j? existe um colaborador cadastrado com o e-mail informado.
/// </summary>
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _collection.Find(item => item.Email == email).AnyAsync();
    }

/// <summary>
/// Persiste um novo registro e devolve a entidade resultante da opera??o.
/// </summary>
    public async Task<Employee> AddAsync(Employee employee)
    {
        var document = MapToDocument(employee);
        await _collection.InsertOneAsync(document);
        employee.Id = document.Id;
        return employee;
    }

    private static EmployeeDocument MapToDocument(Employee employee) => new()
    {
        Id = employee.Id,
        FullName = employee.FullName,
        Email = employee.Email,
        Role = employee.Role,
        Department = employee.Department,
        Phone = employee.Phone,
        AccessLevel = employee.AccessLevel,
        Active = employee.Active,
        CreatedAt = employee.CreatedAt,
        UpdatedAt = employee.UpdatedAt
    };

    private static Employee MapToDomain(EmployeeDocument document) => new()
    {
        Id = document.Id,
        FullName = document.FullName,
        Email = document.Email,
        Role = document.Role,
        Department = document.Department,
        Phone = document.Phone,
        AccessLevel = document.AccessLevel,
        Active = document.Active,
        CreatedAt = document.CreatedAt,
        UpdatedAt = document.UpdatedAt
    };
}
