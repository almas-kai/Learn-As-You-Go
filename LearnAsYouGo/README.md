# LearnAsYouGo

The project is used to create a backend-end side of the webapp. To learn more about web-development and Dotnet.

## Notes

`Application` folder is for business logic. Use cases, services, handlers, and DTOs. And of course their contracts.

`DataAccess` folder is for ORM setup, `DbContext`, repository implementations. In some architectures data access is merged with the infrastructure.

`Domain` should have zero deps to other layers. This layer defines rules.

`Infrastructure` is for external concerns, such as: file storage, email services, external APIs, Logging, implementations of interfaces from application layer.

## Local Development Setup

### Docker (Database + SMTP)

Start the required services with Docker Compose:

```bash
docker compose up -d
```

This starts:
- **PostgreSQL 17** on port `5432` — persistent via named volume `postgres_data`
- **Mailpit** — SMTP on port `1025`, Web UI at `http://localhost:8025`

All emails sent by the application are captured by Mailpit and visible in its Web UI. No emails leave your machine.

### Connection strings

Secrets are set at the project level. We should set it inside of the `Api` layer, because this layer is responsible for startup. It reads the configs at startup.

Use `dotnet user-secrets init` (if `.csproj` does not contain user-secret id section), then `dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Database=LearnAsYouGo;Username=postgres;Password=YourPassword"`.

Please note that the name `ConnectionStrings:Default` is used later.

## Roles

The app defines three roles (see `Shared/Constants/AppRoles.cs`):

| Role | Description |
|---|---|
| `Admin` | Full access. Seeded automatically on first run. |
| `User` | Standard user. Assigned after email confirmation. |
| `Guest` | Restricted access. Assigned on registration before email confirmation. |

**Default admin credentials** (dev only, configured via `SeedSettings` in `appsettings.Development.json`):
- Email: `admin@learnasyougo.com`
- Password: `Admin123!`

## Email Service

Implemented via **MailKit** (`Infrastructure/Services/SmtpEmailService.cs`). Configured through `SmtpSettings` in `appsettings.Development.json`.

Identity email flows supported:
- Email confirmation on registration
- Password reset link
- Password reset code

## Local Development Interfaces

When running the application locally, you can access the following useful interfaces:

- **API Documentation (Scalar)**: `http://localhost:5147/scalar/v1` (or `https://localhost:7195/scalar/v1`)
  *Here you can view, test, and interact with all API endpoints.*
- **Local Email Inbox (Mailpit)**: `http://localhost:8025`
  *Since the app runs with Mailpit via Docker, all outgoing emails (registration confirmations, etc.) are intercepted and displayed here. No real emails are sent!*
