vyracare-api-client (.NET 8) - MongoDB + AWS Lambda
---------------------------------------------------

Descricao:
  - API responsavel pelos cadastros operacionais consumidos pelo `vyracare-app-user-mfe`

Recursos iniciais:
  - Projeto .NET 8 preparado para AWS Lambda
  - MongoDB configurado com database `vyracare`
  - Collections separadas para `patients` e `employees`
  - Validacao basica de duplicidade por CPF e e-mail
  - Rotas base publicadas em `/api/client`
  - Swagger habilitado em `/swagger/index.html`
  - CORS habilitado por configuracao

Rotas principais:
  - `GET /api/client/patients`
  - `GET /api/client/patients/{id}`
  - `GET /api/client/patients/cpf/{cpf}`
  - `POST /api/client/patients`
  - `GET /api/client/employees`
  - `GET /api/client/employees/{id}`
  - `GET /api/client/employees/email/{email}`
  - `POST /api/client/employees`

Setup local:
  - Instale o .NET 8 SDK
  - Configure um cluster MongoDB e defina a env var `MONGO_URI` ou atualize `appsettings.json`
  - Ajuste `Cors:AllowedOrigins` caso precise restringir os dominios permitidos
  - `dotnet restore`
  - `dotnet build`
  - `dotnet run`

Para publicar:
  - `dotnet publish -c Release -o ./publish`
