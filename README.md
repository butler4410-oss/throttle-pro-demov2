# ThrottlePro.Shared

This project contains all shared code between the ThrottlePro Server (API) and Client (Blazor WASM) projects.

## Structure

### Entities
Domain models representing the database schema with EF Core annotations:
- `Parent` - Multi-tenant parent companies (e.g., Grease Monkey Corporate)
- `Store` - Physical locations under a Parent
- `Customer` - Customer records with lifecycle tracking
- `Vehicle` - Customer vehicles
- `Visit` - Service visit transactions
- `Coupon` - Marketing coupons/offers
- `Segment` - Customer segments (static or dynamic)
- `CustomerSegment` - Junction table for Customer-Segment relationships
- `Campaign` - Marketing campaigns with ROAS tracking
- `Journey` - Automated customer journeys
- `JourneyStep` - Individual steps in a journey
- `ReportTemplate` - Report definitions (system or user-created)
- `ReportSchedule` - Scheduled report executions
- `ReportExecution` - Individual report runs with cached results
- `ReportDataSource` - Custom data source configurations

### DTOs
Data transfer objects for API responses:
- `DashboardSummary` - Main dashboard metrics
- `CustomerListItem` - Lightweight customer data for grids
- `CustomerDetail` - Complete customer information
- `CampaignSummary` - Campaign performance data
- `SegmentSummary` - Segment metadata
- `ROASSummary` - Campaign ROAS metrics
- `ODataResponse<T>` - Standard OData wrapper

### Enums
- `CustomerLifecycleStage` - New, Active, AtRisk, Lapsed, Lost
- `CampaignStatus` - Draft, Scheduled, Active, Paused, Completed, Cancelled
- `CampaignChannel` - DirectMail, Email, SMS, Meta, LandingPage, Phone
- `SegmentType` - Static, Dynamic
- `JourneyTriggerType` - SegmentEntry, VisitCompleted, CouponRedeemed, etc.
- `ReportCategory` - Customer, Campaign, Revenue, Lifecycle, Store, Executive, Custom
- `ReportType` - Table, Chart, Dashboard, KPI, Comparison, Trend
- `ReportFrequency` - Manual, Daily, Weekly, Monthly, Quarterly, Custom
- `ReportFormat` - PDF, Excel, CSV, JSON, HTML
- `ReportExecutionStatus` - Queued, Running, Completed, Failed, Cancelled
- `DataSourceType` - Entity, CustomSQL, StoredProcedure, API
- `AggregationType` - None, Count, Sum, Average, Min, Max, Percentage, Growth
- `ChartType` - Bar, Line, Pie, Doughnut, Area, Scatter, Funnel, Gauge

### Interfaces
- `ITenantContext` - Provides access to current Parent/Store context
- `IReportEngine` - Core report execution and export operations
- `IReportScheduler` - Report scheduling and automation
- `IReportRepository` - Report data access patterns

### Constants
Application-wide constants including:
- Header names (`X-Parent-Id`, `X-Store-Id`)
- Role names
- Lifecycle stage day thresholds
- Cache key patterns
- Pagination defaults
- Report execution limits and constraints
- Filter operators (13 types)
- Date interval options

## Reporting Engine

The ThrottlePro platform includes a comprehensive custom reporting engine that allows users to create, schedule, and export reports.

### Components

**Entities:**
- `ReportTemplate` - Report definitions (system or user-created)
- `ReportSchedule` - Scheduled report executions
- `ReportExecution` - Individual report runs with cached results
- `ReportDataSource` - Custom data source configurations

**DTOs:**
- `ReportConfiguration` - Complete report setup (fields, filters, grouping, sorting, calculations)
- `ReportVisualization` - Chart/visualization settings
- `ReportResult` - Execution results with data
- `ReportParameter` - Runtime parameter definitions
- Various request/response DTOs for API operations

**Enums:**
- `ReportCategory` - Customer, Campaign, Revenue, Lifecycle, Store, Executive, Custom
- `ReportType` - Table, Chart, Dashboard, KPI, Comparison, Trend
- `ReportFrequency` - Manual, Daily, Weekly, Monthly, Quarterly, Custom
- `ReportFormat` - PDF, Excel, CSV, JSON, HTML
- `ReportExecutionStatus` - Queued, Running, Completed, Failed, Cancelled
- `DataSourceType` - Entity, CustomSQL, StoredProcedure, API
- `AggregationType` - Count, Sum, Average, Min, Max, Percentage, Growth
- `ChartType` - Bar, Line, Pie, Doughnut, Area, Scatter, Funnel, Gauge

### System Templates

Pre-built reports included (via `SystemReportTemplates.cs`):
1. **Customer Lifecycle Distribution** - Pie chart showing customer breakdown by lifecycle stage
2. **Campaign ROAS Analysis** - Table with detailed campaign performance metrics
3. **Monthly Revenue Trend** - Line chart tracking revenue over time
4. **Top Customers by Lifetime Value** - Ranked table of highest-value customers
5. **Store Performance Comparison** - Bar chart comparing stores by revenue
6. **At-Risk Customer Alert** - Table identifying customers at risk of lapsing
7. **Campaign Channel Comparison** - Bar chart comparing performance across channels
8. **Daily Visits Summary** - Table with daily visit counts and revenue
9. **Customer Acquisition Funnel** - Funnel chart showing customer journey progression
10. **Segment Performance Report** - Table overview of all customer segments

### Features

- **Dynamic Query Builder** - Build reports from any entity with flexible field selection
- **Multi-Tenant Isolation** - Automatic ParentId filtering for data security
- **Flexible Filtering** - 13 filter operators (Equals, Contains, GreaterThan, Between, In, IsNull, etc.)
- **Aggregations** - Count, Sum, Average, Min, Max, Percentage, Growth calculations
- **Grouping** - Group by fields or date intervals (day, week, month, quarter, year)
- **Sorting** - Multiple sort fields with ascending/descending order
- **Pagination** - Configurable page sizes with total count tracking
- **Calculations** - Custom calculated fields with expressions
- **Visualizations** - 8 chart types with Matrix brand colors (#00A3E0, #002D72, #FFC72C)
- **Export Formats** - PDF, Excel, CSV, JSON, HTML output
- **Scheduling** - Daily, weekly, monthly, quarterly, or custom cron expressions
- **Email Delivery** - Automated report distribution to recipients
- **Result Caching** - Fast re-display of previous executions
- **Execution History** - Track all report runs with performance metrics

### Report Configuration Structure

```csharp
var config = new ReportConfiguration
{
    DataSource = "Customer",
    Fields = new List<ReportField>
    {
        new() { FieldName = "FirstName", DisplayName = "First Name" },
        new() { FieldName = "TotalSpent", DisplayName = "Lifetime Value",
                Format = "$#,##0.00", Aggregation = AggregationType.Sum }
    },
    Filters = new List<ReportFilter>
    {
        new() { FieldName = "LifecycleStage", Operator = "Equals", Value = "Active" }
    },
    GroupBy = new List<ReportGroupBy>
    {
        new() { FieldName = "LifecycleStage" }
    },
    Sorting = new List<ReportSort>
    {
        new() { FieldName = "TotalSpent", Descending = true }
    }
};
```

### Usage Example

```csharp
// Execute a system template
var result = await reportEngine.ExecuteReportAsync(templateId);

// Create a custom report
var request = new CreateReportTemplateRequest
{
    Name = "My Custom Report",
    Category = ReportCategory.Customer,
    Type = ReportType.Table,
    Configuration = config,
    Visualization = visualization
};
var template = await reportEngine.CreateTemplateAsync(request);

// Schedule a report
var schedule = new CreateReportScheduleRequest
{
    ReportTemplateId = template.Id,
    Name = "Weekly Sales Report",
    Frequency = ReportFrequency.Weekly,
    OutputFormat = ReportFormat.PDF,
    EmailRecipients = "manager@example.com,ceo@example.com"
};
await scheduler.CreateScheduleAsync(schedule);
```

## Multi-Tenancy

All entities include a `ParentId` for tenant isolation. The server applies automatic filtering based on the `X-Parent-Id` header. Store-level filtering uses `X-Store-Id` when provided.

## Usage

### Server Project
Reference for:
- EF Core DbContext entity configuration
- OData entity models
- Repository patterns
- Tenant filtering

### Client Project  
Reference for:
- Service layer DTOs
- State management models
- API response parsing
- Shared constants

## Lifecycle Stage Logic

Customer lifecycle stages are calculated based on `DaysSinceLastVisit`:
- **New**: 0-30 days since first visit
- **Active**: 31-90 days
- **At Risk**: 91-180 days
- **Lapsed**: 181-365 days
- **Lost**: 365+ days
