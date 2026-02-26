# Raffle Management System - Server

The backend REST API for the Raffle Management System, built with ASP.NET Core 8.0 and Entity Framework Core.

## Table of Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [API Overview](#api-overview)
- [Environment Variables](#environment-variables)
- [Database](#database)
- [Authentication & Authorization](#authentication--authorization)
- [Logging](#logging)
- [Testing](#testing)
- [Development Guidelines](#development-guidelines)

## Overview

This is the server-side REST API of the Raffle Management System. It provides secure endpoints for managing users, gifts, categories, purchases, and raffle draws with role-based authorization.

The API is built using a clean architecture approach with separation of concerns:
- **Controllers** handle HTTP requests and responses
- **Services** contain business logic
- **Repositories** manage data access
- **DTOs** define data transfer objects for API contracts
- **Middlewares** handle cross-cutting concerns

## Technologies Used

### Core Framework

- **ASP.NET Core**: 8.0
- **C#**: Latest with nullable reference types enabled
- **.NET SDK**: 8.0

### Database & ORM

- **Entity Framework Core**: 8.0.7
- **SQL Server**: Database provider
- **EF Core Tools**: For migrations and database management

### Authentication & Security

- **JWT Bearer Authentication**: Microsoft.AspNetCore.Authentication.JwtBearer 8.0
- **BCrypt.Net-Next**: 4.0.3 - Password hashing

### Additional Libraries

- **AutoMapper**: 12.0.1 - Object-to-object mapping
- **Serilog**: 4.3.0 - Structured logging
  - Serilog.AspNetCore: 10.0.0
  - Serilog.Sinks.Console: 6.1.1
  - Serilog.Sinks.File: 7.0.0
- **Swashbuckle.AspNetCore**: 6.5.0 - Swagger/OpenAPI documentation

### Architecture Patterns

- Repository Pattern for data access
- Service Layer for business logic
- Dependency Injection
- Clean Architecture principles

## Prerequisites

Before you begin, ensure you have the following installed:

- **.NET SDK**: 8.0 or higher
- **SQL Server**: Local instance, Docker container, or remote server
- **Entity Framework Core Tools**: For running migrations

Verify your installation:

```bash
dotnet --version
dotnet ef --version
```

If EF Core tools are not installed:

```bash
dotnet tool install --global dotnet-ef
```

## Getting Started

### 1. Clone the Repository

```bash
git clone <repository-url>
cd finalProject/server
```

### 2. Set Up the Database

#### Option A: Using Docker (Recommended)

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourStrong@Pass123" -p 1433:1433 --name sql-server -d mcr.microsoft.com/mssql/server:2022-latest
```

#### Option B: Using Local SQL Server

Ensure SQL Server is running and accessible on your machine.

### 3. Configure the Application

Edit `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=RaffleDb;User Id=sa;Password=YourStrong@Pass123;TrustServerCertificate=True"
  },
  "Admin": {
    "Password": "your-admin-password"
  },
  "EmailSettings": {
    "AdminEmail": "your-email@example.com"
  }
}
```

**Important**: Update these settings before running the application:
- Database connection string
- Admin password
- Email settings (if using email functionality)

See [Environment Variables](#environment-variables) for detailed configuration options.

### 4. Restore Dependencies

```bash
dotnet restore
```

### 5. Apply Database Migrations

```bash
dotnet ef database update
```

This will create the database schema based on the existing migrations.

### 6. Run the Application

```bash
dotnet run
```

The API will start and listen on the configured ports (typically `http://localhost:5000` and `https://localhost:5001`).

### 7. Access the API Documentation

Navigate to the Swagger UI:

```
http://localhost:5000/swagger
```

Here you can explore all available endpoints and test them interactively.

## Project Structure

```
server/
├── Controllers/              # API endpoint controllers
│   ├── AuthController.cs    # Authentication endpoints
│   ├── CartController.cs    # Shopping cart management
│   ├── CategoryController.cs # Category CRUD
│   ├── DonorController.cs   # Donor-specific operations
│   ├── EmailController.cs   # Email notifications
│   ├── GiftController.cs    # Gift CRUD and queries
│   ├── PurchaseController.cs # Purchase management
│   ├── UserController.cs    # User management
│   └── WinningController.cs # Raffle draw and winners
│
├── Models/                   # Domain models (entities)
│   ├── UserModel.cs         # User entity
│   ├── GiftModel.cs         # Gift entity
│   ├── CategoryModel.cs     # Category entity
│   ├── PurchaseModel.cs     # Purchase entity
│   ├── WinningModel.cs      # Winning entity
│   └── enum/               # Enumerations
│
├── DTOs/                     # Data Transfer Objects
│   ├── AuthDto.cs           # Authentication DTOs
│   ├── CartDto.cs           # Cart DTOs
│   ├── CategoryDto.cs       # Category DTOs
│   ├── DonorDto.cs          # Donor DTOs
│   ├── GiftDto.cs           # Gift DTOs
│   ├── PurchaseDto.cs       # Purchase DTOs
│   ├── UserDto.cs           # User DTOs
│   └── WinningDto.cs        # Winning DTOs
│
├── Services/                 # Business logic layer
│   ├── Interfaces/          # Service interfaces
│   └── Implementations/     # Service implementations
│
├── Repositories/             # Data access layer
│   ├── Interfaces/          # Repository interfaces
│   └── Implementations/     # Repository implementations
│
├── Data/                     # Database context
│   ├── AppDbContext.cs      # EF Core DbContext
│   └── AppDbContextFactory.cs # Design-time factory
│
├── Mappings/                 # AutoMapper profiles
│   ├── CategoryProfile.cs
│   ├── DonorProfile.cs
│   ├── GiftProfile.cs
│   ├── PurchaseProfile.cs
│   ├── UserProfile.cs
│   └── WinningProfile.cs
│
├── Middlewares/              # Custom middleware
│   ├── ExceptionHandlingMiddleware.cs
│   └── ExceptionHandlingMiddlewareExtensions.cs
│
├── Migrations/               # EF Core migrations
├── Logs/                     # Application logs (auto-generated)
├── wwwroot/                  # Static files
├── Program.cs                # Application entry point
├── appsettings.json          # Production configuration
├── appsettings.Development.json # Development configuration
└── server.csproj             # Project file
```

## API Overview

The API follows RESTful conventions and is organized into the following resource groups:

### Authentication

- `POST /api/auth/login` - User login with email and password

### Users

- `GET /api/user` - Get all users (Admin only)
- `GET /api/user/{id}` - Get user by ID
- `POST /api/user` - Create new user (registration)
- `PUT /api/user/{id}` - Update user
- `DELETE /api/user/{id}` - Delete user (Admin only)

### Gifts

- `GET /api/gift` - Get all gifts with optional filters (sort, category, donor)
- `GET /api/gift/{id}` - Get gift by ID
- `POST /api/gift` - Create new gift (Admin/Donor)
- `PUT /api/gift/{id}` - Update gift
- `DELETE /api/gift/{id}` - Delete gift

### Categories

- `GET /api/category` - Get all categories
- `GET /api/category/{id}` - Get category by ID
- `POST /api/category` - Create category (Admin)
- `PUT /api/category/{id}` - Update category (Admin)
- `DELETE /api/category/{id}` - Delete category (Admin)

### Cart

- `GET /api/cart/{userId}` - Get user's cart
- `POST /api/cart` - Add item to cart
- `PUT /api/cart/{id}` - Update cart item
- `DELETE /api/cart/{id}` - Remove item from cart

### Purchases

- `GET /api/purchase` - Get all purchases
- `GET /api/purchase/{id}` - Get purchase by ID
- `POST /api/purchase` - Create purchase
- `POST /api/purchase/complete` - Complete purchase (checkout)

### Winnings

- `GET /api/winning` - Get all winnings
- `GET /api/winning/{id}` - Get winning by ID
- `POST /api/winning/draw` - Execute raffle draw (Admin)

### Email

- `POST /api/email/send` - Send email notification

### Donors

- `GET /api/donor` - Get all donors
- Additional donor-specific endpoints

**Note**: Detailed API documentation with request/response schemas is available via Swagger UI at `/swagger`.

## Environment Variables

The application can be configured through `appsettings.json` and `appsettings.Development.json` files.

### Required Configuration

#### Connection Strings

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=RaffleDb;User Id=sa;Password=YourStrong@Pass123;TrustServerCertificate=True"
  }
}
```

**Format**: `Server=<host>,<port>;Database=<db-name>;User Id=<username>;Password=<password>;TrustServerCertificate=True`

#### JWT Configuration

```json
{
  "Jwt": {
    "Key": "your-secret-key-at-least-32-characters-long",
    "Issuer": "CA-Api",
    "Audience": "CA-Client",
    "ExpiresMinutes": 60
  }
}
```

**Important**: Change the `Key` value in production to a strong, unique secret.

#### Email Settings

```json
{
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "EnableSSL": true,
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "AdminEmail": "admin@example.com"
  }
}
```

**Note**: For Gmail, use an [App Password](https://support.google.com/accounts/answer/185833) instead of your regular password.

#### Admin Configuration

```json
{
  "Admin": {
    "Password": "secure-admin-password"
  }
}
```

### Optional Configuration

#### Logging (Serilog)

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    }
  }
}
```

#### CORS

CORS is configured in `Program.cs` to allow requests from the Angular development server (`http://localhost:4200`). Modify this for production deployments.

### Using User Secrets (Development)

For sensitive data in development, use .NET User Secrets:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
dotnet user-secrets set "Jwt:Key" "your-secret-key"
```

## Database

### Technology

- **Database Management System**: Microsoft SQL Server
- **ORM**: Entity Framework Core 8.0.7
- **Migration Tool**: EF Core Migrations

### Entities

The database includes the following main entities:

- **Users**: User accounts with roles (Admin, Donor, User)
- **Gifts**: Raffle gifts/prizes
- **Categories**: Gift categories
- **Purchases**: User purchases (tickets)
- **Winnings**: Raffle winners
- **Cart**: Shopping cart items

### Migrations

#### Create a New Migration

```bash
dotnet ef migrations add MigrationName
```

#### Apply Migrations

```bash
dotnet ef database update
```

#### Rollback Migration

```bash
dotnet ef database update PreviousMigrationName
```

#### Remove Last Migration

```bash
dotnet ef migrations remove
```

### Database Schema

The schema is defined through Entity Framework Core models in the `Models/` directory. Relationships are configured using:

- One-to-many relationships (e.g., Category → Gifts, User → Purchases)
- Foreign keys and navigation properties
- Cascade delete policies

## Authentication & Authorization

### JWT Authentication

The API uses JSON Web Tokens (JWT) for stateless authentication:

1. User logs in via `POST /api/auth/login`
2. Server validates credentials and returns a JWT token
3. Client includes the token in the `Authorization` header for subsequent requests:
   ```
   Authorization: Bearer <token>
   ```

### Authorization Roles

Three roles are defined:

- **Admin** (Role = 0): Full system access
- **Donor** (Role = 1): Can manage their own gifts
- **User** (Role = 2): Can browse and purchase

### Protected Endpoints

Endpoints are protected using the `[Authorize]` attribute with role-based policies:

```csharp
[Authorize(Roles = "Admin")]
[HttpDelete("{id}")]
public async Task<ActionResult> Delete(int id)
```

## Logging

The application uses **Serilog** for structured logging.

### Log Destinations

- **Console**: Real-time log output during development
- **File**: Logs are written to `Logs/log-YYYYMMDD.txt`
  - Rolling interval: Daily
  - Retention: 14 days

### Log Levels

- **Information**: General application flow
- **Warning**: Unusual situations that don't cause errors
- **Error**: Errors and exceptions
- **Debug**: Detailed diagnostic information (development only)

### Viewing Logs

Log files are located in the `Logs/` directory. Each day creates a new log file.

### HTTP Request Logging

All HTTP requests are automatically logged with:
- HTTP method and path
- Response status code
- Duration

## Testing

### Unit Tests

Unit tests are located in the `SERVER.Tests/` project.

#### Run All Tests

```bash
dotnet test
```

#### Run Tests with Code Coverage

```bash
dotnet test /p:CollectCoverage=true
```

### Manual Testing

Use the Swagger UI at `/swagger` to test endpoints manually:

1. Navigate to `http://localhost:5000/swagger`
2. Expand an endpoint
3. Click "Try it out"
4. Fill in parameters
5. Click "Execute"

For protected endpoints:

1. First, authenticate via `/api/auth/login`
2. Copy the returned JWT token
3. Click the "Authorize" button in Swagger
4. Enter `Bearer <token>` and authorize
5. All subsequent requests will include the token

## Development Guidelines

### Code Style

- Follow C# coding conventions
- Use meaningful variable and method names
- Keep methods small and focused
- Write XML documentation comments for public APIs

### Architecture Principles

1. **Separation of Concerns**: Keep controllers thin, move logic to services
2. **Dependency Injection**: Register services in `Program.cs`
3. **Repository Pattern**: All data access through repositories
4. **DTOs**: Never expose domain models directly in APIs
5. **AutoMapper**: Use for mapping between models and DTOs

### Adding a New Feature

1. **Model**: Create the entity in `Models/`
2. **DTO**: Define DTOs in `DTOs/`
3. **Mapping**: Create AutoMapper profile in `Mappings/`
4. **Repository**: Create interface and implementation in `Repositories/`
5. **Service**: Create interface and implementation in `Services/`
6. **Controller**: Create controller in `Controllers/`
7. **Migration**: Generate and apply migration
8. **Tests**: Write unit tests in `SERVER.Tests/`

### Best Practices

- **Async/Await**: Use async methods for I/O operations
- **Exception Handling**: Let the global middleware handle exceptions
- **Validation**: Validate input using Data Annotations or FluentValidation
- **Logging**: Log important events and errors
- **Security**: Never log sensitive data (passwords, tokens)
- **Performance**: Use EF Core efficiently (avoid N+1 queries, use projections)

### Error Handling

The application uses global exception handling middleware:

- All exceptions are caught and logged
- Appropriate HTTP status codes are returned
- Error details are included in development mode only
- Generic error messages in production for security

## Troubleshooting

### Database Connection Fails

- Verify SQL Server is running
- Check the connection string in `appsettings.Development.json`
- Ensure the database user has proper permissions
- For Docker: Verify the container is running with `docker ps`

### Migration Errors

```bash
# Reset the database (WARNING: deletes all data)
dotnet ef database drop
dotnet ef database update
```

### Port Already in Use

Modify the port in `Properties/launchSettings.json`:

```json
{
  "profiles": {
    "http": {
      "applicationUrl": "http://localhost:5001"
    }
  }
}
```

### JWT Authentication Fails

- Verify the JWT settings in `appsettings.json` match between client and server
- Check that the token hasn't expired
- Ensure the `Authorization` header is formatted correctly: `Bearer <token>`

## Additional Resources

- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://learn.microsoft.com/en-us/ef/core/)
- [Serilog Documentation](https://serilog.net/)
- [AutoMapper Documentation](https://docs.automapper.org/)
- [Swagger/OpenAPI Documentation](https://swagger.io/docs/)

## Support

For issues and questions:

1. Check the [main README](../README.md) for general project information
2. Review the [client README](../client/README.md) for frontend-related issues
3. Consult the official ASP.NET Core documentation
4. Check existing issues in the project repository