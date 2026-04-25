vyracare-api-client (.NET 8) - MongoDB + AWS Lambda
-------------------------------------

Descricao:
  - Api responsavel pelas interações com a camada de cliente

Recursos iniciais:
  - Projeto .NET 8 preparado para AWS Lambda
  - MongoDB configurado com database `vyracare`
  - Controller inicial para a collection `client`
  - Rotas base publicadas em `/api/client`

Setup local:
  - Install .NET 8 SDK
  - Configure a MongoDB cluster and set `MONGO_URI` env var or update `appsettings.json`
  - `dotnet restore`
  - `dotnet build`
  - `dotnet run`

To publish:
  - `dotnet publish -c Release -o ./publish`
