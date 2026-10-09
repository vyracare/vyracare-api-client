using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Vyracare.Api.Client.Common.Configuration;
using Vyracare.Api.Client.Infrastructure.Persistence.Documents;

namespace Vyracare.Api.Client.Infrastructure.Persistence;

public sealed class MongoTenantIndexInitializer : IHostedService
{
    private readonly IMongoClient _client;
    private readonly MongoOptions _options;
    public MongoTenantIndexInitializer(IMongoClient client, IOptions<MongoOptions> options) => (_client, _options) = (client, options.Value);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var database = _client.GetDatabase(_options.Database);
        var populatedTenantOnly = new BsonDocument("tenantId", new BsonDocument("$type", "string"));
        await database.GetCollection<PatientDocument>("patients").Indexes.CreateOneAsync(
            new CreateIndexModel<PatientDocument>(
                Builders<PatientDocument>.IndexKeys.Ascending(item => item.TenantId).Ascending(item => item.Cpf),
                new CreateIndexOptions<PatientDocument> { Name = "ux_patients_tenant_cpf", Unique = true, PartialFilterExpression = populatedTenantOnly }),
            cancellationToken: cancellationToken);
        await database.GetCollection<EmployeeDocument>("employees").Indexes.CreateOneAsync(
            new CreateIndexModel<EmployeeDocument>(
                Builders<EmployeeDocument>.IndexKeys.Ascending(item => item.TenantId).Ascending(item => item.Email),
                new CreateIndexOptions<EmployeeDocument> { Name = "ux_employees_tenant_email", Unique = true, PartialFilterExpression = populatedTenantOnly }),
            cancellationToken: cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
