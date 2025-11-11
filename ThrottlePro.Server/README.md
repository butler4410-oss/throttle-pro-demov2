# ThrottlePro.Server

ASP.NET Core Web API server for ThrottlePro - Multi-tenant CRM and Marketing Automation platform for automotive service centers.

## Features

- **Multi-Tenant Architecture**: Automatic data isolation via tenant context headers
- **OData Support**: Full OData query capabilities (filter, select, expand, orderby, count)
- **Entity Framework Core**: SQL Server database with migrations
- **Soft Deletes**: All entities support soft delete
- **Audit Trails**: Automatic tracking of created/updated timestamps and users
- **Swagger/OpenAPI**: Interactive API documentation
- **Structured Logging**: Serilog with console and file outputs

## Technology Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core 8.0
- SQL Server / LocalDB
- OData 8.2.5
- Serilog
- Swagger/OpenAPI

## Prerequisites

- Visual Studio 2022 (or later) with ASP.NET and web development workload
- .NET 8 SDK
- SQL Server 2019+ or SQL Server LocalDB
- SQL Server Management Studio 21 (optional, for database management)

## Getting Started

### 1. Database Setup

The application is configured to use SQL Server LocalDB for development:

**Connection String (Development):**
```
Server=(localdb)\mssqllocaldb;Database=ThrottleProDev;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true
```

### 2. Running Migrations

Migrations are applied automatically when running in Development mode. To manually run migrations:

**Using Package Manager Console (Visual Studio):**
```powershell
Add-Migration InitialCreate
Update-Database
```

**Using .NET CLI:**
```bash
dotnet ef migrations add InitialCreate --project ThrottlePro.Server
dotnet ef database update --project ThrottlePro.Server
```

### 3. Running the Application

**Visual Studio:**
1. Open the solution in Visual Studio 2022
2. Set `ThrottlePro.Server` as the startup project
3. Press F5 or click "Start Debugging"

**Command Line:**
```bash
cd ThrottlePro.Server
dotnet run
```

The application will start on:
- HTTPS: `https://localhost:7001`
- HTTP: `http://localhost:7000`

### 4. Accessing Swagger UI

Navigate to: `https://localhost:7001/swagger`

## API Endpoints

### OData Endpoints

All OData endpoints support standard OData query operations:

- **GET** `/odata/Parents` - List parent companies
- **GET** `/odata/Stores` - List stores
- **GET** `/odata/Customers` - List customers
- **GET** `/odata/Campaigns` - List campaigns
- **GET** `/odata/Segments` - List segments
- **GET** `/odata/Vehicles` - List vehicles
- **GET** `/odata/Visits` - List visits

**Example OData Queries:**
```
GET /odata/Customers?$filter=LifecycleStage eq 'Active'
GET /odata/Customers?$select=FirstName,LastName,Email
GET /odata/Customers?$expand=Vehicles,Visits
GET /odata/Customers?$orderby=LastName
GET /odata/Customers?$top=10&$skip=20
GET /odata/Customers?$count=true
```

### REST API Endpoints

- **GET** `/api/Dashboard/summary` - Dashboard summary with key metrics
- **GET** `/api/Dashboard/lifecycle-distribution` - Customer lifecycle breakdown
- **GET** `/api/Dashboard/revenue-trends` - 12-month revenue trends
- **GET** `/health` - Health check endpoint

## Multi-Tenant Usage

All tenant-scoped endpoints require the `X-Parent-Id` header to identify the tenant:

```http
GET /odata/Customers
Headers:
  X-Parent-Id: 3fa85f64-5717-4562-b3fc-2c963f66afa6
```

Optional `X-Store-Id` header can be provided for store-level filtering:

```http
GET /odata/Visits
Headers:
  X-Parent-Id: 3fa85f64-5717-4562-b3fc-2c963f66afa6
  X-Store-Id: 7c9e6679-7425-40de-944b-e07fc1f90ae7
```

## Database Schema

### Core Entities

- **Parent**: Tenant/parent company (e.g., Grease Monkey, Jiffy Lube)
- **Store**: Physical store location
- **Customer**: Customer records with lifecycle tracking
- **Vehicle**: Customer vehicles
- **Visit**: Service visits/transactions

### Marketing Entities

- **Campaign**: Marketing campaigns with tracking metrics
- **Coupon**: Promotional offers/coupons
- **Segment**: Customer segments (dynamic/static)
- **CustomerSegment**: Customer-to-segment relationships
- **Journey**: Automated customer journeys
- **JourneyStep**: Individual journey steps

### Reporting Entities

- **ReportTemplate**: Report definitions
- **ReportSchedule**: Scheduled report executions
- **ReportExecution**: Report execution history
- **ReportDataSource**: Custom data sources

## Seed Data

The application automatically seeds sample data in Development mode:

- 2 Parent companies (Grease Monkey, Jiffy Lube)
- 10 Stores (5 per parent)
- 200 Customers (100 per parent)
- ~300 Vehicles
- ~500 Visits
- 6 Segments
- 4 Active campaigns

## Configuration

### Connection Strings

Edit `appsettings.json` or `appsettings.Development.json`:

**LocalDB (Development):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ThrottleProDev;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=true"
}
```

**SQL Server Express:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.\\SQLEXPRESS;Database=ThrottlePro;Trusted_Connection=true;TrustServerCertificate=true"
}
```

**SQL Server (Windows Auth):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ThrottlePro;Integrated Security=true;TrustServerCertificate=true"
}
```

**Azure SQL:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:yourserver.database.windows.net,1433;Database=ThrottlePro;User Id=username;Password=password;Encrypt=true;TrustServerCertificate=false"
}
```

### CORS Configuration

Configure allowed origins in `appsettings.json`:

```json
"Cors": {
  "AllowedOrigins": [
    "http://localhost:5000",
    "https://localhost:5001"
  ]
}
```

## Project Structure

```
ThrottlePro.Server/
├── Controllers/          # API Controllers
│   ├── CustomersController.cs
│   ├── CampaignsController.cs
│   ├── DashboardController.cs
│   ├── ParentsController.cs
│   └── StoresController.cs
├── Data/                 # Database context and seed data
│   ├── ThrottleProDbContext.cs
│   └── SeedData.cs
├── Middleware/           # Custom middleware
│   └── TenantContextMiddleware.cs
├── Migrations/           # EF Core migrations (auto-generated)
├── Properties/           # Launch settings
│   └── launchSettings.json
├── appsettings.json      # Production settings
├── appsettings.Development.json  # Development settings
└── Program.cs            # Application entry point

```

## Troubleshooting

### Database Connection Issues

1. Verify SQL Server LocalDB is installed:
   ```bash
   sqllocaldb info
   ```

2. Create LocalDB instance if needed:
   ```bash
   sqllocaldb create mssqllocaldb
   sqllocaldb start mssqllocaldb
   ```

3. Test connection in SQL Server Management Studio:
   - Server name: `(localdb)\mssqllocaldb`
   - Authentication: Windows Authentication

### Migration Issues

1. Delete existing database:
   ```sql
   DROP DATABASE ThrottleProDev;
   ```

2. Remove migration files from `Migrations/` folder

3. Recreate migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

### Common Errors

**Error: "Cannot open database"**
- Ensure SQL Server LocalDB is running
- Check connection string in `appsettings.Development.json`

**Error: "Login failed for user"**
- Verify Windows Authentication is configured correctly
- Check SQL Server authentication mode

**Error: "OData endpoint not found"**
- Verify OData route configuration in `Program.cs`
- Check controller route attributes

## Development Tips

### Testing with Swagger

1. Navigate to `/swagger`
2. Expand an endpoint
3. Click "Try it out"
4. Add `X-Parent-Id` header (get from seed data or database)
5. Execute the request

### Viewing Logs

Logs are written to:
- Console output
- `logs/throttlepro-YYYYMMDD.txt`

### SQL Server Management Studio

Connect to LocalDB and browse the database:
1. Server: `(localdb)\mssqllocaldb`
2. Database: `ThrottleProDev`
3. Explore tables, indexes, and data

## Production Deployment

### Configuration Changes

1. Update connection string to production SQL Server
2. Set `ASPNETCORE_ENVIRONMENT=Production`
3. Update CORS origins to production URLs
4. Configure proper JWT secret key
5. Enable HTTPS certificate validation

### Best Practices

- Use Azure Key Vault or similar for secrets
- Enable Application Insights for monitoring
- Configure proper backup strategy for SQL Server
- Implement rate limiting and API throttling
- Set up health checks and monitoring
- Use Azure SQL with geo-replication for high availability

## Support

For issues or questions:
- Check the logs in `logs/` directory
- Review Swagger API documentation
- Inspect database with SQL Server Management Studio
- Enable detailed EF Core logging in `appsettings.Development.json`
