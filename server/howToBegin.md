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