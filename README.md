# UserCrud

CRUD simples de usuário em ASP.NET Core Web API, para fins de estudo.

## Stack

- .NET 8 / ASP.NET Core Web API (Controllers)
- Entity Framework Core + SQLite (banco de dados local, arquivo `usercrud.db`)
- Swagger (disponível em ambiente de desenvolvimento)

## Estrutura

- `Models/User.cs` — entidade `User` (Id, Name, Email, CreatedAt)
- `Data/AppDbContext.cs` — DbContext do EF Core
- `Controllers/UsersController.cs` — endpoints CRUD
- `Migrations/` — migrations do EF Core

## Como rodar

```bash
dotnet restore
dotnet run
```

O banco SQLite (`usercrud.db`) é criado automaticamente na primeira execução, e as migrations pendentes são aplicadas na inicialização (`db.Database.Migrate()` em `Program.cs`).

A API sobe por padrão em `https://localhost:xxxx` (verifique a porta impressa no console). O Swagger fica disponível em `/swagger` em ambiente de desenvolvimento.

## Endpoints

| Método | Rota              | Descrição                        |
|--------|-------------------|-----------------------------------|
| GET    | `/api/users`      | Lista todos os usuários           |
| GET    | `/api/users/{id}` | Busca um usuário pelo id          |
| POST   | `/api/users`      | Cria um novo usuário              |
| PUT    | `/api/users/{id}` | Atualiza um usuário existente     |
| DELETE | `/api/users/{id}` | Remove um usuário                 |

### Exemplo de payload (POST/PUT)

```json
{
  "name": "Guilherme",
  "email": "guilherme@example.com"
}
```

O e-mail é validado como único: tentar criar/atualizar com um e-mail já existente retorna `409 Conflict`.

## Comandos úteis do EF Core

Instale a ferramenta global (se ainda não tiver):

```bash
dotnet tool install -g dotnet-ef
```

Criar uma nova migration após alterar o modelo:

```bash
dotnet ef migrations add NomeDaMigration
```

Aplicar migrations manualmente (opcional, já ocorre automaticamente ao rodar a app):

```bash
dotnet ef database update
```
