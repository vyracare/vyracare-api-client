# vyracare-api-client

API .NET 8 responsavel pelos cadastros operacionais de pacientes e colaboradores consumidos pelo `vyracare-app-user-mfe`.

## Estrutura

O projeto segue um modelo de `vertical slice` por feature.

- `Features/Patients`
  Fluxos de criacao, listagem, consulta por id e consulta por CPF.
- `Features/Employees`
  Fluxos de criacao, listagem, consulta por id e consulta por e-mail.
- `Features/*/Shared`
  Entidades de dominio (`Patient`, `Employee`) e portas de persistencia.
- `Common`
  Tipos compartilhados de configuracao, resultado de caso de uso, extensoes HTTP e abstração de tempo.
- `Infrastructure/Persistence`
  Adapters MongoDB para pacientes e colaboradores.
- `Infrastructure/DependencyInjection`
  Configuracao do container e bootstrap do banco.

Arquivos centrais:
- [Program.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Program.cs)
- [PatientsController.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Patients/PatientsController.cs)
- [EmployeesController.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Employees/EmployeesController.cs)
- [ServiceCollectionExtensions.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Infrastructure/DependencyInjection/ServiceCollectionExtensions.cs)

## Rotas

Base path:

- `/api/client`

Endpoints:
- `GET /api/client/patients`
- `GET /api/client/patients/{id}`
- `GET /api/client/patients/cpf/{cpf}`
- `POST /api/client/patients`
- `GET /api/client/employees`
- `GET /api/client/employees/{id}`
- `GET /api/client/employees/email/{email}`
- `POST /api/client/employees`

## Seguranca e configuracao

- JWT obrigatorio em todos os endpoints.
- Configuracao sensivel carregada via `SecretsManagerBootstrapper`.
- Secrets padrao:
  - `vyracare/shared/mongo`
  - `vyracare/shared/jwt-signing`

Fallbacks suportados:
- `MONGO_URI`
- `JWT_KEY`
- `JWT_ISSUER`
- `JWT_AUDIENCE`
- `CORS_ALLOWED_ORIGINS`

## Testes unitarios

Camada de testes:

- [Vyracare.Api.Client.Tests.csproj](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Vyracare.Api.Client.Tests/Vyracare.Api.Client.Tests.csproj)

Cobertura inicial incluida:
- `CreatePatientHandler`
- `CreateEmployeeHandler`

Comando esperado:

```bash
dotnet test Vyracare.Api.Client.Tests/Vyracare.Api.Client.Tests.csproj
```

## Integracao com frontend

O arquivo [.vyracare/mfe-consumer.json](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/.vyracare/mfe-consumer.json) declara o frontend consumidor e permite que a esteira atualize automaticamente os arquivos de ambiente quando a URL publicada da API muda.

## Execucao local

```bash
dotnet restore
dotnet build
dotnet run
```

## Deploy

Publica em AWS Lambda + HTTP API com Swagger habilitado em:

- `/swagger/index.html`
- `/swagger/v1/swagger.json`
