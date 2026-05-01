using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.AspNetCoreServer.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Vyracare.Api.Client.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var mongoUri = configuration["Mongo:ConnectionString"] ?? Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
var mongoDatabase = configuration["Mongo:Database"] ?? "vyracare";
var corsAllowedOriginsRaw = configuration["Cors:AllowedOrigins"] ?? Environment.GetEnvironmentVariable("CORS_ALLOWED_ORIGINS") ?? "*";
var corsAllowedOrigins = corsAllowedOriginsRaw
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoUri));
builder.Services.AddScoped(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDatabase));

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
    {
        if (corsAllowedOrigins.Length == 0 || corsAllowedOrigins.Contains("*"))
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            return;
        }

        policy.WithOrigins(corsAllowedOrigins).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "vyracare-api-client",
        Version = "v1",
        Description = "API responsavel pelos cadastros operacionais consumidos pelo vyracare-app-user-mfe."
    });
});
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<PatientService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "vyracare-api-client v1");
    options.RoutePrefix = "swagger";
});
app.UseHttpsRedirection();
app.UseCors("DefaultCors");
app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "ok", api = "vyracare-api-client" }));

app.Run();
