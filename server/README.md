# Raffle Management System - Server

The backend REST API for the Raffle Management System, built with ASP.NET Core 8.0 and Entity Framework Core.

## Table of Contents

- [Overview](#overview)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [API Overview](#api-overview)
- [Database](#database)
- [Authentication & Authorization](#authentication--authorization)
- [Logging](#logging)
- [Testing](#testing)

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

- **JWT Bearer Authentication**: `Microsoft.AspNetCore.Authentication.JwtBearer` (used for issuing and validating JWT tokens)
- **Password hashing**: `BCrypt.Net-Next` (used for secure password hashing)



## Getting Started

Follow these steps to configure and run the backend API locally.

## 1. Initial Setup
Clone the repository and navigate to the server directory:
```bash
cd server
```


## 2. Configuration
Configure the database connection and secrets

Edit `appsettings.Development.json`. Below is a complete example that matches the project's configuration fields — sensitive values are redacted. Do NOT commit real secrets to source control; use User Secrets or environment variables for secrets.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },

  "Admin": {
    "Password": "<ADMIN_PASSWORD>"
  },

  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=RaffleDb;User Id=sa;Password=<DB_PASSWORD>;TrustServerCertificate=True"
  },

  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "EnableSSL": true,
    "Username": "<EMAIL_USERNAME>",
    "Password": "<EMAIL_APP_PASSWORD>",
    "AdminEmail": "<ADMIN_EMAIL>"
  },

  "AllowedHosts": "*",

  "Jwt": {
    "Key": "<JWT_SECRET_KEY>",
    "Issuer": "CA-Api",
    "Audience": "CA-Client",
    "ExpiresMinutes": 60
  }
}
```

**Note**: For Gmail, use an [App Password](https://support.google.com/accounts/answer/185833) instead of your regular password.

Important: Ensure you update the connection string, admin password, and email settings before proceeding to the next steps.



## 3. Restore and Update
Run the following commands to install dependencies and sync your database schema:

```bash
dotnet restore
```

Apply Entity Framework Core migrations to create/update the database schema:

```bash
dotnet ef database update
```

## 4. Run the Application

```bash
dotnet run
```

The API will listen on the configured ports (check `launchSettings.json`).

## 5. API Documentation
Open the Swagger UI (e.g., `http://localhost:5000/swagger`) to explore endpoints.


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



## Database

### Entities

The database includes the following main entities:

- **Users**: User accounts with roles (Admin, Donor, User)
- **Gifts**: Raffle gifts/prizes
- **Categories**: Gift categories
- **Purchases**: User purchases (tickets)
- **Winnings**: Raffle winners
- **Cart**: Shopping cart items

### Seed Data

A SQL script with sample data is available at [`documents/script.sql`](documents/script.sql).  
You can run it after applying the EF Core migrations to populate the database with initial data.

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
- **Donor** (Role = 1): Can see their own gifts
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


### Architecture Principles

1. **Separation of Concerns**: Keep controllers thin, move logic to services
2. **Dependency Injection**: Register services in `Program.cs`
3. **Repository Pattern**: All data access through repositories
4. **DTOs**: Never expose domain models directly in APIs
5. **AutoMapper**: Use for mapping between models and DTOs


### Error Handling

The application uses global exception handling middleware:

- All exceptions are caught and logged
- Appropriate HTTP status codes are returned
- Error details are included in development mode only
- Generic error messages in production for security

