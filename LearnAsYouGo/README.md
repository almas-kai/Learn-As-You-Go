# LearnAsYouGo

The project is used to create a backend-end side of the webapp. To learn more about web-development and Dotnet.

## Notes

`Application` folder is for business logic. Use cases, services, handlers, and DTOs. And of course their contracts.

`DataAccess` folder is for ORM setup, `DbContext`, repository implementations. In some architectures data access is merged with the infrastructure.

`Domain` should have zero deps to other layers. This layer defines rules.

`Infrastructure` is for external concerns, such as: file storage, email services, external APIs, Logging, implementations of interfaces from application layer.

## Connection strings

Secrets are set at the project level. We should set it inside of the `Api` layer, because this layer is responsible for startup. It reads the configs at startup.

Use `dotnet user-secrets init`, then `dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Database=LearnAsYouGo;Username=psql;Password=YourPassword"`.

Please note that the name `ConnectionStrings:Default` is used later.
