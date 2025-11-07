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

### Interfaces
- `ITenantContext` - Provides access to current Parent/Store context

### Constants
Application-wide constants including:
- Header names (`X-Parent-Id`, `X-Store-Id`)
- Role names
- Lifecycle stage day thresholds
- Cache key patterns
- Pagination defaults

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
