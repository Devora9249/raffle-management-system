# Chinese Auction / Raffle Management System - Project Guide

## 📋 Table of Contents
1. [Project Overview](#project-overview)
2. [Tech Stack](#tech-stack)
3. [Project Structure](#project-structure)
4. [Development Setup](#development-setup)
5. [Configuration & Secrets](#configuration--secrets)
6. [Database & Migrations](#database--migrations)
7. [Logging & Diagnostics](#logging--diagnostics)
8. [Architecture Guidelines](#architecture-guidelines)
9. [Testing](#testing)
10. [API Documentation](#api-documentation)
11. [Deployment](#deployment)
12. [Troubleshooting](#troubleshooting)

---

## Project Overview

**Purpose**: Full-stack online raffle (Chinese auction) management system with roles for Admin, Donor, and Users.

**Key Features**:
- User authentication and authorization (JWT-based)
- Gift/item management for raffles
- Shopping cart and purchase system
- Winner selection and notifications
- Donor dashboard and statistics
- Admin panel for system management

---

## Tech Stack

### Frontend
- **Framework**: Angular (TypeScript, SCSS)
- **UI Library**: PrimeNG (`primeng`, `primeicons`, `@primeng/themes`, `@primeuix/themes`)
- **Location**: `client/` directory
- **Dev Server**: `ng serve` at http://localhost:4200

**Important**: When adding PrimeNG components, ensure:
1. Corresponding UI module imports are added to the Angular module
2. Required CSS/theme files are included
3. Check `client/angular.json` and `client/src/styles.scss` for theme imports

### Backend
- **Framework**: .NET 8 (C#), ASP.NET Core Web API
- **ORM**: Entity Framework Core with SQL Server provider
- **Authentication**: JWT (JSON Web Tokens)
- **Logging**: Serilog (console + rolling file logs)
- **Mapping**: AutoMapper
- **API Documentation**: Swashbuckle (Swagger)
- **Location**: `server/` directory

### Testing
- **Framework**: xUnit
- **Mocking**: Moq
- **Assertions**: FluentAssertions
- **Location**: `SERVER.Tests/`

---

## Project Structure

```
finalProject/
├── client/                    # Angular frontend
│   ├── src/
│   │   ├── app/              # Angular components, services, etc.
│   │   ├── styles.scss       # Global styles
│   │   └── index.html
│   ├── angular.json
│   └── package.json
│
├── server/                    # ASP.NET Core backend
│   ├── Controllers/          # HTTP endpoints (thin layer)
│   ├── Services/             # Business logic
│   │   ├── Interfaces/
│   │   └── Implementations/
│   ├── Repositories/         # Data access layer
│   │   ├── Interfaces/
│   │   └── Implementations/
│   ├── Data/                 # EF Core DbContext
│   ├── DTOs/                 # Data transfer objects
│   ├── Models/               # Domain entities
│   ├── Mappings/             # AutoMapper profiles
│   ├── Middlewares/          # Custom middleware (exception handling, etc.)
│   ├── Migrations/           # EF Core migrations
│   ├── Logs/                 # Serilog output files
│   ├── wwwroot/              # Static files
│   ├── Program.cs            # Application entry point & DI configuration
│   ├── appsettings.json      # Configuration
│   └── server.csproj
│
├── SERVER.Tests/             # Server-side tests
│   └── Services/
│
├── finalProject.sln          # Solution file
└── README.md
```

### Key Files to Review
- `server/Program.cs` - Registered services, middleware, and application configuration
- `server/appsettings.json` / `appsettings.Development.json` - Configuration settings
- `server/Data/AppDbContext.cs` - Entity Framework context and entity configurations
- `client/angular.json` - Angular project configuration
- `client/src/styles.scss` - Global styles and theme imports

---

## Development Setup

### Prerequisites
- **.NET 8 SDK** - Verify with `dotnet --info`
- **Node.js & npm** - For Angular development
- **SQL Server** - LocalDB or full SQL Server instance
- **Windows Environment** - Use PowerShell-compatible commands

### Initial Setup

#### Backend
```powershell
# From server/ directory
dotnet restore
dotnet build
dotnet run
```

The backend will start on the configured port (check `launchSettings.json`).

#### Frontend
```powershell
# From client/ directory
npm install
npm run start
# or
ng serve
```

The Angular dev server will start at http://localhost:4200.

### Common Commands

| Task | Command | Location |
|------|---------|----------|
| Build server | `dotnet build server/server.csproj` | Repo root |
| Run server | `dotnet run --project server/server.csproj` | Repo root |
| Build client | `npm run build` | `client/` |
| Start client dev | `npm run start` or `ng serve` | `client/` |
| Run all tests | `dotnet test` | Repo root |
| Run specific tests | `dotnet test SERVER.Tests/SERVER.Tests.csproj` | Repo root |

---

## Configuration & Secrets

### Configuration Files
- **`server/appsettings.json`** - Base configuration (committed to repo)
- **`server/appsettings.Development.json`** - Development overrides (committed to repo)
- **User Secrets** - Sensitive data (NOT committed)


**Never commit secrets to version control!**

```powershell
# Set a secret
dotnet user-secrets set "JwtOptions:SecretKey" "your-secret-key" --project server

# List all secrets
dotnet user-secrets list --project server
```

### Key Configuration Sections
- **ConnectionStrings:DefaultConnection** - SQL Server connection string
- **JwtOptions** - JWT authentication settings (SecretKey, Issuer, Audience, etc.)
- **Serilog** - Logging configuration

### Environment Variables
Configuration can be overridden via environment variables:
- Connection strctionStrings__DefaultConnection`
- JWT secret: `JwtOptions__SecretKey`

---

## Database & Migrations

### Entity Framework Core Setup
- **Provider**: SQL Server
- **Connection String**: Look in `appsettings.json` under `ConnectionStrings:DefaultConnection`
- **DbContext**: `server/Data/AppDbContext.cs`
- **Migrations**: `server/Migrations/`

### Migration Commands
All EF Core commands should be run from the `server/` directory:

```powershell
# Add a new migration
dotnet ef migrations add MigrationName

# Apply migrations to database
dotnet ef database update

# Remove last migration (only if not applied)
dotnet ef migrations remove

# Generate SQL script for a migration
dotnet ef migrations script
```

**Important**: 
- Ensure `DefaultConnection` points to a valid SQL Server instance before running migrations
- For testing purposes, consider using EF Core InMemory provider
- Migrations are automatically applied on startup in Development mode (check `Program.cs`)

### Testing with Databases
- **Unit Tests**: Use EF InMemory provider or mock repositories
- **Integration Tests**: Use TestContainers or a test SQL Server instance

---

## Logging & Diagnostics

### Serilog Configuration
The project uses Serilog for structured logging:
- **Console Output**: Enabled by default
- **File Output**: Rolling files in `server/Logs/log-yyyyMMdd.txt`
- **Configuration**: See `Program.cs` for setup

### Logging Best Practices
1. **Use Structured Logging**:
   ```csharp
   _logger.LogInformation("Processing gift {GiftId} for user {UserId}", giftId, userId);
   ```

2. **Appropriate Log Levels**:
   - `Debug` - Detailed diagnostic info (development only)
   - `Information` - General flow of the application
   - `Warning` - Unexpected but handled situations
   - `Error` - Errors and exceptions
   - `Critical` - Critical failures requiring immediate attention

3. **Never Log Secrets or PII**:
   - Mask passwords, tokens, and sensitive user data
   - Be careful with credit card info, emails, phone numbers

4. **Inject ILogger<T>**:
   ```csharp
   public class MyService
   {
       private readonly ILogger<MyService> _logger;
       
       public MyService(ILogger<MyService> logger)
       {
           _logger = logger;
       }
   }
   ```

### Viewing Logs
- **Development**: Check console output and `server/Logs/` directory
- **Production**: Integrate with centralized logging (Seq, Application Insights, etc.)

---

## Architecture Guidelines

### Architectural Layers
The backend follows a clean architecture pattern:

1. **Controllers** (`server/Controllers/`)
   - Thin HTTP layer
   - Input validation and model binding
   - Delegate to services
   - See [controller-instruction.md](controller-instruction.md) for details

2. **Services** (`server/Services/`)
   - Business logic and orchestration
   - Map between domain models and DTOs
   - Handle exceptions
   - See [service-instruction.md](service-instruction.md) for details

3. **Repositories** (`server/Repositories/`)
   - Data access only
   - EF Core operations
   - Return domain entities (NOT DTOs)
   - See [repository-instructions.md](repository-instructions.md) for details

4. **Data** (`server/Data/`)
   - EF Core DbContext
   - Entity configurations

### Separation of Concerns
- **Controllers** → Validate input, call services, return responses
- **Services** → Business logic, entity ↔ DTO mapping, orchestration
- **Repositories** → Data access, queries, persistence
- **DTOs** → Data transfer between client and server (in `server/DTOs/`)
- **Models** → Domain entities (in `server/Models/`)

### Dependency Injection
All services, repositories, and dependencies are registered in `Program.cs`:

```csharp
// Example
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<IGiftRepository, GiftRepository>();
```

### AutoMapper
Mapping profiles are in `server/Mappings/`:
- Create a new profile class for each entity/DTO pair
- Register profiles automatically via `AddAutoMapper(typeof(Program))`

### Middleware
Custom middleware is in `server/Middlewares/`:
- **ExceptionHandlingMiddleware** - Global exception handler (returns consistent error responses)
- Add new middleware in `Program.cs` with `app.UseMiddleware<T>()`

### Coding Conventions

#### C# / .NET
- **Naming**: PascalCase for public types/methods, camelCase for private fields (with `_` prefix)
- **Async**: Use async/await for I/O operations, return `Task<T>`
- **DI**: Constructor injection for dependencies
- **Testing**: Add unit tests for non-trivial logic in `SERVER.Tests`

#### Angular
- Use Angular CLI for code generation
- Follow existing TypeScript/SCSS conventions
- Keep component logic lean, delegate to services

---

## Testing

### Test Project
- **Location**: `SERVER.Tests/`
- **Framework**: xUnit
- **Tools**: Moq (mocking), FluentAssertions (assertions)

### Running Tests
```powershell
# Run all tests
dotnet test

# Run specific test project
dotnet test SERVER.Tests/SERVER.Tests.csproj

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"
```

### Test Guidelines
1. **Unit Tests**: Mock dependencies (repositories, external services)
2. **Integration Tests**: Use real or in-memory database
3. **Add Tests For**:
   - New services and business logic
   - Complex controllers
   - Critical repository methods

### Example Test Structure
```csharp
public class GiftServiceTests
{
    private readonly Mock<IGiftRepository> _mockRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GiftService _service;

    public GiftServiceTests()
    {
        _mockRepo = new Mock<IGiftRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new GiftService(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsGift_WhenExists()
    {
        // Arrange, Act, Assert
    }
}
```

---

## API Documentation

### Swagger / OpenAPI
Swagger is enabled in **Development mode** by default.

**Access**: Navigate to `/swagger` when running the server locally (e.g., http://localhost:5000/swagger)

**Features**:
- Interactive API documentation
- Try out endpoints directly from browser
- View request/response schemas
- Authentication: Use the "Authorize" button to add JWT token

### Adding API Documentation
Use XML comments and attributes:

```csharp
/// <summary>
/// Gets a gift by ID
/// </summary>
/// <param name="id">The gift ID</param>
/// <returns>The gift details</returns>
[HttpGet("{id}")]
public async Task<ActionResult<GiftDto>> GetById(int id)
{
    // ...
}
```

---

## Deployment

### Building for Production

#### Backend
```powershell
# Publish release build
dotnet publish server/server.csproj -c Release -o ./publish

# Output will be in ./publish directory
```

**Deployment Checklist**:
1. Set production connection string (via environment variables)
2. Configure JWT secrets securely
3. Update CORS policy if needed
4. Enable HTTPS
5. Run migrations on production database
6. Configure logging (consider external logging service)

#### Frontend
```powershell
# From client/ directory
npm run build

# Output will be in client/dist/ directory
```

**Deployment Checklist**:
1. Update API base URL for production
2. Enable production optimizations
3. Configure environment files
4. Serve via web server (IIS, Nginx, etc.) or CDN

### Hosting Options
- **Backend**: Azure App Service, IIS, Docker, Kubernetes
- **Frontend**: Azure Static Web Apps, Netlify, Vercel, or serve from backend wwwroot
- **Database**: Azure SQL Database, SQL Server on VM

---

## Troubleshooting

### Common Issues

#### .NET Build Failures
```powershell
# Check SDK version
dotnet --info

# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

Expected SDK: **.NET 8.0** (minimum)

#### Database Connection Issues
1. Verify connection string in `appsettings.json` or user secrets
2. Ensure SQL Server is running
3. Check firewall rules
4. For testing, use EF InMemory provider:
   ```csharp
   services.AddDbContext<AppDbContext>(options =>
       options.UseInMemoryDatabase("TestDb"));
   ```

#### Angular Build Failures
```powershell
# Clean node_modules and reinstall
Remove-Item -Recurse -Force node_modules
Remove-Item package-lock.json
npm install

# Or use clean install
npm ci
```

#### Migration Failures
- Ensure you're in the `server/` directory
- Check that EF Core tools are installed: `dotnet tool install --global dotnet-ef`
- Verify connection string is correct
- Check for pending migrations: `dotnet ef migrations list`

#### JWT Authentication Issues
- Verify JWT secret is configured (user secrets or environment variables)
- Check token expiration
- Ensure `Issuer` and `Audience` match between generation and validation
- Use browser dev tools to inspect Authorization header

### Getting Help
1. Check existing tests for usage examples
2. Search for similar implementations in the codebase
3. Review `Program.cs` for service registrations and middleware
4. Check log files in `server/Logs/`

---

## Safety Checklist Before Changes

✅ **Before Committing**:
1. Run tests: `dotnet test`
2. Build server: `dotnet build server/server.csproj`
3. Build client: `npm run build` (from `client/`)
4. Verify no secrets in code
5. Check for TypeScript/linting errors

✅ **Before Adding Migrations**:
1. Confirm connection string points to dev/test database
2. Review generated migration code
3. Test migration on local database first

✅ **Before Deploying**:
1. All tests passing
2. Production configuration verified
3. Secrets configured in hosting environment
4. Database backup taken (if applying migrations)
5. Rollback plan prepared

---

## Quick Reference

### Important Commands
```powershell
# Check .NET SDK
dotnet --info

# List all dotnet tools
dotnet tool list -g

# Secrets management
dotnet user-secrets list --project server
dotnet user-secrets set "Key" "Value" --project server

# EF Core
dotnet ef migrations add Name
dotnet ef database update
dotnet ef migrations list

# Testing
dotnet test
dotnet test --logger "console;verbosity=detailed"
```

### Key Concepts
- **Windows Environment**: Use PowerShell, not bash
- **Thin Controllers**: Validation + Service calls only
- **Services = Business Logic**: Orchestration and mapping
- **Repositories = Data Access**: EF Core operations only
- **No DTOs in Repositories**: Return domain entities
- **Structured Logging**: Use Serilog properly
- **DI Everything**: Register in `Program.cs`

---

## Related Documentation
- [Controller Guidelines](controller-instruction.md)
- [Service Guidelines](service-instruction.md)
- [Repository Guidelines](repository-instructions.md)
- [Microservices Concepts](micro-services-guide-organized.md)

---

**Last Updated**: Use PR descriptions with context when opening changes. Update this file with discoveries during development.

 
