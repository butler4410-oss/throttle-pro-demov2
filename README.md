# ThrottlePro Platform

Multi-tenant customer engagement and campaign management platform for automotive quick lube service chains.

## 🚀 Overview

ThrottlePro is an enterprise-grade customer retention and campaign management system designed for automotive service operators like Grease Monkey and Jiffy Lube. Built on a sophisticated multi-tenant architecture supporting thousands of shops organized hierarchically under brands and franchise groups.

### Key Features

- **Multi-Tenant Architecture** - Parent → Store hierarchy with complete data isolation
- **Customer Lifecycle Management** - Track customers through New, Active, At Risk, Lapsed, Lost stages
- **Campaign Management** - Multi-channel (Direct Mail, Email, SMS) with ROAS tracking
- **Segment Builder** - Dynamic and static customer segmentation
- **Journey Automation** - Multi-step automated customer engagement
- **Custom Reporting Engine** - Template-based reports with scheduling
- **Real-time Analytics** - Executive dashboards with KPIs

## 🛠️ Technology Stack

**Frontend:**
- Blazor WebAssembly (.NET 8)
- MudBlazor UI Components
- Chart.js visualizations

**Backend:**
- ASP.NET Core Web API (.NET 8)
- Entity Framework Core 8
- SQL Server / LocalDB
- OData v8

**Key Patterns:**
- Multi-tenant data isolation
- Repository pattern
- Result pattern for error handling
- Soft deletes with full audit trail
- BaseEntity with audit fields

## 📋 Prerequisites

- Visual Studio 2022 (v17.8+)
- .NET 8 SDK
- SQL Server 2019+ or SQL Server Express
- SQL Server Management Studio 21 (recommended)

## 🏗️ Project Structure

```
ThrottlePro/
├── ThrottlePro.Shared/      # 11 entities, 7+ DTOs, 8 enums
├── ThrottlePro.Server/      # ASP.NET Core Web API
├── ThrottlePro.Client/      # Blazor WebAssembly
└── ThrottlePro.sln
```

### ThrottlePro.Shared
Shared library containing:
- **11 Core Entities**: Parent, Store, Customer, Vehicle, Visit, Coupon, Segment, CustomerSegment, Campaign, Journey, JourneyStep
- **4 Reporting Entities**: ReportTemplate, ReportSchedule, ReportExecution, ReportDataSource
- **7+ DTOs**: DashboardSummary, CustomerListItem, CustomerDetail, CampaignSummary, SegmentSummary, ROASSummary, ODataResponse
- **8 Enums**: CustomerLifecycleStage, CampaignStatus, CampaignChannel, SegmentType, JourneyTriggerType, ReportCategory, ReportType, ReportFrequency
- **3 Interfaces**: ITenantContext, IReportEngine, IReportScheduler
- **Constants**: Application-wide constants for headers, roles, thresholds, cache keys

### ThrottlePro.Server
ASP.NET Core Web API:
- DbContext with EF Core 8
- OData v8 endpoints
- Multi-tenant filtering middleware
- Repository pattern
- Custom reporting engine
- SQL Server integration

### ThrottlePro.Client
Blazor WebAssembly frontend:
- MudBlazor components
- Chart.js visualizations
- State management
- HTTP client services
- Responsive design

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/butler4410-oss/throttle-pro-demov2.git
cd throttle-pro-demov2
```

### 2. Configure Database
Edit `ThrottlePro.Server/appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ThrottleProDev;Trusted_Connection=true"
  }
}
```

### 3. Restore Packages
```bash
dotnet restore
```

### 4. Run Migrations
```bash
# Package Manager Console in Visual Studio
Update-Database

# Or .NET CLI
dotnet ef database update --project ThrottlePro.Server
```

### 5. Run Application
Press **F5** in Visual Studio or:
```bash
dotnet run --project ThrottlePro.Server
```

Access the API at: `https://localhost:7001/swagger`

## 🗄️ Database

### Connection Strings

**LocalDB (Development):**
```
Server=(localdb)\\mssqllocaldb;Database=ThrottleProDev;Trusted_Connection=true
```

**SQL Server Express:**
```
Server=.\\SQLEXPRESS;Database=ThrottlePro;Trusted_Connection=true
```

**Full SQL Server:**
```
Server=localhost;Database=ThrottlePro;Integrated Security=true
```

### Verify in SSMS
1. Connect to `(localdb)\mssqllocaldb`
2. Database: `ThrottleProDev`
3. Tables: Parents, Stores, Customers, Campaigns, etc.

## 🎨 Design System

**Matrix Brand Colors:**
- Primary Blue: `#00A3E0`
- Dark Blue: `#002D72`
- Yellow: `#FFC72C`

## 📊 Key Entities

### Core Entities
- **Parent** - Multi-tenant parent companies (e.g., Grease Monkey Corporate)
- **Store** - Physical locations under a Parent
- **Customer** - Customer records with lifecycle tracking
- **Vehicle** - Customer vehicles
- **Visit** - Service visit transactions

### Marketing Entities
- **Coupon** - Marketing coupons/offers
- **Segment** - Customer segments (static or dynamic)
- **Campaign** - Marketing campaigns with ROAS tracking
- **Journey** - Automated customer journeys

### Reporting Entities
- **ReportTemplate** - Report definitions (system or user-created)
- **ReportSchedule** - Scheduled report executions
- **ReportExecution** - Individual report runs with cached results
- **ReportDataSource** - Custom data source configurations

## 🔄 Customer Lifecycle Stages

Customer lifecycle stages are calculated based on `DaysSinceLastVisit`:
- **New**: 0-30 days since first visit
- **Active**: 31-90 days
- **At Risk**: 91-180 days
- **Lapsed**: 181-365 days
- **Lost**: 365+ days

## 📈 Key Metrics

- Direct mail campaigns: 42.3% response rate
- Email campaigns: 23.8% response rate
- Customer lifecycle stages tracked in real-time
- ROAS tracking per campaign
- Multi-channel attribution

## 🧪 Testing

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test /p:CollectCoverage=true
```

## 📦 Deployment

### Build for Production
```bash
dotnet publish ThrottlePro.Server -c Release -o ./publish
```

### Generate Migration Script
```bash
dotnet ef migrations script --project ThrottlePro.Server -o migration.sql
```

### Docker Support (Future)
```bash
docker build -t throttlepro:latest .
docker run -p 8080:80 throttlepro:latest
```

## 🔐 Security

- Multi-tenant data isolation via `ParentId`
- Automatic tenant filtering in all queries
- JWT authentication (to be implemented)
- Role-based authorization
- SQL injection protection via EF Core
- Input validation and sanitization

## 🤝 Contributing

1. Create feature branch: `git checkout -b feature/your-feature`
2. Commit changes: `git commit -m "feat: add feature"`
3. Push branch: `git push origin feature/your-feature`
4. Create Pull Request

### Commit Message Convention
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation changes
- `refactor:` - Code refactoring
- `test:` - Adding tests
- `chore:` - Maintenance tasks

## 📄 License

Proprietary and confidential. See [LICENSE](LICENSE) for details.

## 📚 Documentation

- [ThrottlePro.Shared Documentation](ThrottlePro.Shared/README.md) - Detailed entity and DTO documentation
- [API Documentation](ThrottlePro.Server/README.md) - API endpoints and usage (coming soon)
- [Client Documentation](ThrottlePro.Client/README.md) - Frontend architecture (coming soon)

## 🐛 Troubleshooting

### Database Connection Issues
1. Verify SQL Server LocalDB is installed
2. Check connection string in `appsettings.Development.json`
3. Run `sqllocaldb info` to see available instances
4. Run `sqllocaldb start mssqllocaldb` if needed

### Migration Issues
```bash
# Drop database and recreate
dotnet ef database drop --project ThrottlePro.Server
dotnet ef database update --project ThrottlePro.Server
```

### Build Issues
```bash
# Clean solution
dotnet clean
rm -rf **/bin **/obj

# Restore and rebuild
dotnet restore
dotnet build
```

## 🗺️ Roadmap

- [ ] JWT Authentication & Authorization
- [ ] Email service integration (SendGrid/SMTP)
- [ ] SMS service integration (Twilio)
- [ ] Direct mail API integration
- [ ] Advanced reporting visualizations
- [ ] Real-time notifications (SignalR)
- [ ] Mobile app (MAUI)
- [ ] API rate limiting
- [ ] Caching layer (Redis)
- [ ] Docker containerization
- [ ] CI/CD pipelines
- [ ] Unit and integration tests
- [ ] Performance optimization
- [ ] Admin portal
- [ ] Customer portal

## 👥 Team

- **Product Owner**: Chase
- **Repository**: https://github.com/butler4410-oss/throttle-pro-demov2

## 📞 Support

For issues and questions:
- GitHub Issues: https://github.com/butler4410-oss/throttle-pro-demov2/issues
- Email: support@throttlepro.com (placeholder)

---

**ThrottlePro** - Powering customer engagement for automotive service chains.

Built with ❤️ using .NET 8 and Blazor WebAssembly
