using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Vyracare.Api.Client.Common.Configuration;
using Vyracare.Api.Client.Common.Time;
using Vyracare.Api.Client.Features.Employees.Create;
using Vyracare.Api.Client.Features.Employees.GetByEmail;
using Vyracare.Api.Client.Features.Employees.GetById;
using Vyracare.Api.Client.Features.Employees.List;
using Vyracare.Api.Client.Features.Employees.Shared.Ports;
using Vyracare.Api.Client.Features.Patients.Create;
using Vyracare.Api.Client.Features.Patients.GetByCpf;
using Vyracare.Api.Client.Features.Patients.GetById;
using Vyracare.Api.Client.Features.Patients.List;
using Vyracare.Api.Client.Features.Patients.Shared.Ports;
using Vyracare.Api.Client.Infrastructure.Persistence;
using Vyracare.Api.Client.Infrastructure.Time;

namespace Vyracare.Api.Client.Infrastructure.DependencyInjection;

/// <summary>
/// Centraliza extens?es reutiliz?veis usadas pela aplica??o.
/// </summary>
public static class ServiceCollectionExtensions
{
/// <summary>
/// Registra os servi?os necess?rios para conectar a aplica??o ao MongoDB.
/// </summary>
    public static IServiceCollection AddMongo(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoClient(options.ConnectionString);
        });

        services.AddScoped(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return sp.GetRequiredService<IMongoClient>().GetDatabase(options.Database);
        });

        return services;
    }

/// <summary>
/// Registra os handlers, portas e servi?os centrais da aplica??o no container de depend?ncias.
/// </summary>
    public static IServiceCollection AddClientCore(this IServiceCollection services)
    {
        services.AddSingleton<IClock, SystemClock>();

        services.AddScoped<IEmployeeRepository, MongoEmployeeRepository>();
        services.AddScoped<IPatientRepository, MongoPatientRepository>();

        services.AddScoped<CreateEmployeeHandler>();
        services.AddScoped<GetEmployeeByEmailHandler>();
        services.AddScoped<GetEmployeeByIdHandler>();
        services.AddScoped<ListEmployeesHandler>();

        services.AddScoped<CreatePatientHandler>();
        services.AddScoped<GetPatientByCpfHandler>();
        services.AddScoped<GetPatientByIdHandler>();
        services.AddScoped<ListPatientsHandler>();

        return services;
    }
}
