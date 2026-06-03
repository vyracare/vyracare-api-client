# vyracare-api-client

## Visao geral

Esta API concentra os cadastros operacionais usados pelo `vyracare-app-user-mfe`.

Hoje ela tem dois dominios principais:

- pacientes;
- colaboradores.

O projeto usa `vertical slice`, entao cada caso de uso fica agrupado por feature.

---

## Como ler este projeto pela primeira vez

Leia nesta ordem:

1. [Program.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Program.cs)
   Mostra como a aplicacao sobe e quais servicos sao registrados.

2. Os controllers:
   - [PatientsController.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Patients/PatientsController.cs)
   - [EmployeesController.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Employees/EmployeesController.cs)

3. Uma feature completa de paciente:
   - [CreatePatientRequest.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Patients/Create/CreatePatientRequest.cs)
   - [CreatePatientHandler.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Patients/Create/CreatePatientHandler.cs)

4. Uma feature completa de colaborador:
   - [CreateEmployeeRequest.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Employees/Create/CreateEmployeeRequest.cs)
   - [CreateEmployeeHandler.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Employees/Create/CreateEmployeeHandler.cs)

5. As portas:
   - [IPatientRepository.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Patients/Shared/Ports/IPatientRepository.cs)
   - [IEmployeeRepository.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Features/Employees/Shared/Ports/IEmployeeRepository.cs)

6. Os adapters de persistencia:
   - [MongoPatientRepository.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Infrastructure/Persistence/MongoPatientRepository.cs)
   - [MongoEmployeeRepository.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Infrastructure/Persistence/MongoEmployeeRepository.cs)

7. Os testes:
   - [CreatePatientHandlerTests.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Vyracare.Api.Client.Tests/Patients/Create/CreatePatientHandlerTests.cs)
   - [CreateEmployeeHandlerTests.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Vyracare.Api.Client.Tests/Employees/Create/CreateEmployeeHandlerTests.cs)

---

## Estrutura de pastas

### `Common`

Guarda pecas reutilizaveis por toda a API:

- configuracoes tipadas;
- resultado padrao dos handlers;
- extensoes HTTP;
- abstração de tempo.

### `Features/Patients`

Agrupa tudo que diz respeito a pacientes:

- criar;
- listar;
- buscar por id;
- buscar por CPF.

### `Features/Employees`

Agrupa tudo que diz respeito a colaboradores:

- criar;
- listar;
- buscar por id;
- buscar por e-mail.

### `Shared`

Dentro de cada feature existe uma pasta `Shared` com:

- entidade de dominio;
- portas do dominio.

### `Infrastructure`

Contem os detalhes tecnicos:

- leitura de secrets;
- conexao com MongoDB;
- repositorios Mongo;
- configuracao de DI.

### `Vyracare.Api.Client.Tests`

Projeto de testes unitarios focado nos handlers.

---

## Fluxo passo a passo de uma requisicao

Vamos usar `POST /api/client/patients`.

1. O frontend envia o JSON do formulario.
2. O controller recebe e converte para `CreatePatientRequest`.
3. O controller chama `CreatePatientHandler`.
4. O handler valida regras minimas, como nome e CPF.
5. O handler consulta `IPatientRepository` para verificar duplicidade.
6. O handler monta a entidade `Patient`.
7. O handler envia a entidade para o repositorio.
8. O controller traduz o resultado para `201 Created` ou erro.

O mesmo raciocinio vale para os fluxos de colaboradores.

---

## Endpoints

Base path:

- `/api/client`

### Pacientes

- `GET /api/client/patients`
- `GET /api/client/patients/{id}`
- `GET /api/client/patients/cpf/{cpf}`
- `POST /api/client/patients`

### Colaboradores

- `GET /api/client/employees`
- `GET /api/client/employees/{id}`
- `GET /api/client/employees/email/{email}`
- `POST /api/client/employees`

---

## Seguranca e configuracao

Todos os endpoints exigem JWT.

Segredos sensiveis:

- `Mongo:ConnectionString`
- `Jwt:Key`

Eles sao carregados por:

- [SecretsManagerBootstrapper.cs](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/Infrastructure/SecretsManagerBootstrapper.cs)

Secrets padrao:

- `vyracare/shared/mongo-prod`
- `vyracare/shared/mongo-dev`
- `vyracare/shared/jwt-signing-prod`
- `vyracare/shared/jwt-signing-dev`

Fallbacks:

- `MONGO_URI`
- `JWT_KEY`
- `JWT_ISSUER`
- `JWT_AUDIENCE`
- `CORS_ALLOWED_ORIGINS`

---

## Integracao com frontend

Este projeto declara seu consumidor em:

- [.vyracare/mfe-consumer.json](C:/Users/lenin/OneDrive/Desktop/GitHub/Vyracare/vyracare-api-client/.vyracare/mfe-consumer.json)

Isso permite que a pipeline atualize automaticamente a URL da API no frontend quando o gateway mudar.

---

## Testes unitarios

### O que esta coberto hoje

- conflito por CPF no cadastro de paciente;
- criacao de paciente com sucesso;
- conflito por e-mail no cadastro de colaborador;
- criacao de colaborador com sucesso.

### Como rodar

```bash
dotnet restore
dotnet build --no-restore
dotnet test Vyracare.Api.Client.Tests/Vyracare.Api.Client.Tests.csproj --no-restore
```

### Como evoluir os testes

Ao criar uma nova feature:

1. escreva o handler;
2. identifique as portas usadas;
3. crie fakes simples para essas portas;
4. valide ao menos um cenario feliz e um de erro.

---

## Como adicionar um novo endpoint

Exemplo: `GET /api/client/patients/email/{email}`.

Passos:

1. Criar a pasta `Features/Patients/GetByEmail`.
2. Criar o handler correspondente.
3. Avaliar se a porta `IPatientRepository` precisa de um novo metodo.
4. Implementar o metodo em `MongoPatientRepository`.
5. Expor a rota no `PatientsController`.
6. Registrar o handler em `ServiceCollectionExtensions`.
7. Criar o teste unitario.

---

## Execucao local

```bash
dotnet restore
dotnet build
dotnet run
```

Swagger:

- `/swagger/index.html`

---

## Resumo para desenvolvedores

Pense na API assim:

- o controller so recebe e devolve HTTP;
- o handler concentra a regra;
- a entidade representa o dominio;
- a porta define o contrato;
- o repositorio Mongo implementa esse contrato;
- o teste prova que o handler funciona sem precisar do banco real.

## Convencao de commits

Os commits deste repositorio devem ser escritos em portugues.

Padrao recomendado:

- `feat: adiciona consulta de paciente por email`
- `fix: corrige validacao de documento do cliente`
- `docs: atualiza explicacao da arquitetura da api client`
