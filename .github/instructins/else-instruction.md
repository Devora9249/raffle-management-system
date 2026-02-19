# Misc / other notes

Quick pointers
- Config: `server/appsettings.json` and `server/appsettings.Development.json` hold configuration. Secrets appear to be used (UserSecretsId in `server.csproj`) — do not expose secrets in PRs.
- Logging: Serilog is configured in `Program.cs` to write to console and `server/Logs`.
- Mapping: AutoMapper profiles live in `server/Mappings` — add profiles when creating new DTO/domain pairs.
- Middleware: Global exception handling is in `server/Middlewares` — prefer throwing domain-specific exceptions and let middleware format responses.

Common tasks
- Build server: `dotnet build server/server.csproj`
- Run server: `dotnet run --project server/server.csproj`
- Start client: `cd client; npm install; npm run start`

When in doubt
- Read `Program.cs` first to see registered services and middleware.
- Search for existing implementations before adding new files (use `grep`/search in editor).

Project overview
- This is a full-stack Raffle/Chinese Auction management application with an Angular frontend (`client/`) and an ASP.NET Core backend (`server/`). The backend targets .NET 8 and uses Entity Framework Core with SQL Server.

Tech & tooling
- Frontend: Angular (see `client/package.json`). UI theme and components include PrimeNG. Use `ng serve` for local dev.
- Backend: .NET 8 Web API, EF Core (SQL Server provider), AutoMapper, Serilog, Swashbuckle (Swagger) for API docs.
- Tests: `SERVER.Tests` contains an xUnit test project preconfigured with Moq and FluentAssertions.

Database & migrations
- EF Core migrations are under `server/Migrations/`. When adding migrations, run commands from the `server` folder and ensure `DefaultConnection` points to a dev/test database or use environment variables.

Secrets & environment
- `server.csproj` includes a `UserSecretsId` — local secrets should use `dotnet user-secrets` and should not be committed.
- For CI, prefer injecting secrets through the pipeline's secret store rather than committing them.

Swagger & API exploration
- Swagger is enabled in Development mode by default; you can browse the API at `/swagger` when running locally.

Tests & CI
- Run tests locally with `dotnet test SERVER.Tests/SERVER.Tests.csproj`.
- Consider adding a CI job (GitHub Actions) that runs `dotnet build` and `dotnet test` on PRs.

Frontend notes
- The client uses PrimeNG for UI components and themes. Check `client/angular.json` and `client/src/styles.scss` for theme imports.
- Install and run locally via `npm install` then `npm run start`.

Logging & diagnostics
- Serilog writes rolling logs to `server/Logs/log-.txt`. Use structured logging and avoid logging secrets.
- For local troubleshooting enable verbose logging in `appsettings.Development.json`.

Packaging & deployment hints
- The server is a standard ASP.NET Core Web API; build artifacts live under `server/bin/Debug/net8.0/` during development. For production, create a publish artifact via `dotnet publish` and follow your hosting provider's deployment steps.

Contact & code review
- No dedicated maintainers listed. When opening PRs, include rationale and links to relevant files (Program.cs, new services, mappings, migrations).

