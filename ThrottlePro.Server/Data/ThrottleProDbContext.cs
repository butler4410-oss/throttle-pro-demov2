using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Entities;
using ThrottlePro.Shared.Interfaces;

namespace ThrottlePro.Server.Data;

/// <summary>
/// Database context for ThrottlePro application with multi-tenant support
/// </summary>
public class ThrottleProDbContext : DbContext
{
    private readonly ITenantContext? _tenantContext;

    public ThrottleProDbContext(DbContextOptions<ThrottleProDbContext> options, ITenantContext? tenantContext = null)
        : base(options)
    {
        _tenantContext = tenantContext;
    }

    // Core entities
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Visit> Visits => Set<Visit>();

    // Campaigns and Marketing
    public DbSet<Campaign> Campaigns => Set<Campaign>();
    public DbSet<Coupon> Coupons => Set<Coupon>();
    public DbSet<Segment> Segments => Set<Segment>();
    public DbSet<CustomerSegment> CustomerSegments => Set<CustomerSegment>();

    // Journey/Automation
    public DbSet<Journey> Journeys => Set<Journey>();
    public DbSet<JourneyStep> JourneySteps => Set<JourneyStep>();

    // Reporting
    public DbSet<ReportTemplate> ReportTemplates => Set<ReportTemplate>();
    public DbSet<ReportSchedule> ReportSchedules => Set<ReportSchedule>();
    public DbSet<ReportExecution> ReportExecutions => Set<ReportExecution>();
    public DbSet<ReportDataSource> ReportDataSources => Set<ReportDataSources>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure multi-tenant global query filters
        ConfigureGlobalFilters(modelBuilder);

        // Configure relationships and delete behaviors
        ConfigureRelationships(modelBuilder);

        // Configure decimal precision for SQL Server
        ConfigureDecimalPrecision(modelBuilder);

        // Configure indexes (already defined via attributes but can be enhanced here)
        ConfigureIndexes(modelBuilder);
    }

    /// <summary>
    /// Configure global query filters for multi-tenancy and soft deletes
    /// </summary>
    private void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        // Apply tenant filter to all TenantEntity-derived types
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(TenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");

                // Tenant filter: e => !_tenantContext.HasParentId || e.ParentId == _tenantContext.ParentId
                var tenantFilter = System.Linq.Expressions.Expression.Lambda(
                    System.Linq.Expressions.Expression.OrElse(
                        System.Linq.Expressions.Expression.Not(
                            System.Linq.Expressions.Expression.Property(
                                System.Linq.Expressions.Expression.Constant(_tenantContext),
                                nameof(ITenantContext.HasParentId)
                            )
                        ),
                        System.Linq.Expressions.Expression.Equal(
                            System.Linq.Expressions.Expression.Property(parameter, nameof(TenantEntity.ParentId)),
                            System.Linq.Expressions.Expression.Property(
                                System.Linq.Expressions.Expression.Constant(_tenantContext),
                                nameof(ITenantContext.ParentId)
                            )
                        )
                    ),
                    parameter
                );

                // Soft delete filter: e => !e.IsDeleted
                var softDeleteFilter = System.Linq.Expressions.Expression.Lambda(
                    System.Linq.Expressions.Expression.Equal(
                        System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.IsDeleted)),
                        System.Linq.Expressions.Expression.Constant(false)
                    ),
                    parameter
                );

                // Combine filters
                var combinedFilter = System.Linq.Expressions.Expression.Lambda(
                    System.Linq.Expressions.Expression.AndAlso(
                        tenantFilter.Body,
                        softDeleteFilter.Body
                    ),
                    parameter
                );

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(combinedFilter);
            }
            else if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                // For non-tenant entities (like Parent), only apply soft delete filter
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
                var softDeleteFilter = System.Linq.Expressions.Expression.Lambda(
                    System.Linq.Expressions.Expression.Equal(
                        System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.IsDeleted)),
                        System.Linq.Expressions.Expression.Constant(false)
                    ),
                    parameter
                );

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(softDeleteFilter);
            }
        }
    }

    /// <summary>
    /// Configure relationships and cascade delete behaviors
    /// </summary>
    private void ConfigureRelationships(ModelBuilder modelBuilder)
    {
        // Use Restrict instead of Cascade to prevent accidental data loss
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Parent -> Children relationships
        modelBuilder.Entity<Parent>()
            .HasMany(p => p.Stores)
            .WithOne(s => s.Parent)
            .HasForeignKey(s => s.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Parent>()
            .HasMany(p => p.Customers)
            .WithOne(c => c.Parent)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Customer -> Vehicle relationship
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Vehicles)
            .WithOne(v => v.Customer)
            .HasForeignKey(v => v.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Customer -> Visit relationship
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Visits)
            .WithOne(v => v.Customer)
            .HasForeignKey(v => v.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Store -> Visit relationship
        modelBuilder.Entity<Store>()
            .HasMany(s => s.Visits)
            .WithOne(v => v.Store)
            .HasForeignKey(v => v.StoreId)
            .OnDelete(DeleteBehavior.Restrict);

        // Campaign -> Coupon relationship
        modelBuilder.Entity<Campaign>()
            .HasMany(c => c.Coupons)
            .WithOne(co => co.Campaign)
            .HasForeignKey(co => co.CampaignId)
            .OnDelete(DeleteBehavior.Restrict);

        // Segment relationships
        modelBuilder.Entity<Segment>()
            .HasMany(s => s.Campaigns)
            .WithOne(c => c.Segment)
            .HasForeignKey(c => c.SegmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Segment>()
            .HasMany(s => s.Journeys)
            .WithOne(j => j.Segment)
            .HasForeignKey(j => j.SegmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Journey -> JourneyStep relationship
        modelBuilder.Entity<Journey>()
            .HasMany(j => j.Steps)
            .WithOne(s => s.Journey)
            .HasForeignKey(s => s.JourneyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Report relationships
        modelBuilder.Entity<ReportTemplate>()
            .HasMany(rt => rt.Schedules)
            .WithOne(rs => rs.ReportTemplate)
            .HasForeignKey(rs => rs.ReportTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ReportTemplate>()
            .HasMany(rt => rt.Executions)
            .WithOne(re => re.ReportTemplate)
            .HasForeignKey(re => re.ReportTemplateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ReportSchedule>()
            .HasMany(rs => rs.Executions)
            .WithOne(re => re.ReportSchedule)
            .HasForeignKey(re => re.ReportScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    /// <summary>
    /// Configure decimal precision for SQL Server (18,2)
    /// </summary>
    private void ConfigureDecimalPrecision(ModelBuilder modelBuilder)
    {
        // Customer decimals
        modelBuilder.Entity<Customer>()
            .Property(c => c.TotalSpent)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Customer>()
            .Property(c => c.AverageOrderValue)
            .HasPrecision(18, 2);

        // Visit decimals
        modelBuilder.Entity<Visit>()
            .Property(v => v.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Visit>()
            .Property(v => v.DiscountAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Visit>()
            .Property(v => v.NetAmount)
            .HasPrecision(18, 2);

        // Campaign decimals
        modelBuilder.Entity<Campaign>()
            .Property(c => c.Budget)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Campaign>()
            .Property(c => c.Spent)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Campaign>()
            .Property(c => c.Revenue)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Campaign>()
            .Property(c => c.ROAS)
            .HasPrecision(10, 2);

        // Coupon decimals
        modelBuilder.Entity<Coupon>()
            .Property(c => c.DiscountAmount)
            .HasPrecision(18, 2);
    }

    /// <summary>
    /// Configure additional indexes for performance
    /// </summary>
    private void ConfigureIndexes(ModelBuilder modelBuilder)
    {
        // Most indexes are already defined via attributes on entities
        // Add any additional composite indexes here if needed

        // Example: Composite index for common query patterns
        modelBuilder.Entity<Visit>()
            .HasIndex(v => new { v.ParentId, v.CustomerId, v.VisitDate })
            .HasDatabaseName("IX_Visits_ParentId_CustomerId_VisitDate");

        modelBuilder.Entity<Customer>()
            .HasIndex(c => new { c.ParentId, c.LifecycleStage, c.IsActive })
            .HasDatabaseName("IX_Customers_ParentId_LifecycleStage_IsActive");
    }

    /// <summary>
    /// Override SaveChangesAsync to set audit fields automatically
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = "system"; // TODO: Get from current user context
                    if (entry.Entity is TenantEntity tenantEntity && _tenantContext?.HasParentId == true)
                    {
                        tenantEntity.ParentId = _tenantContext.ParentId!.Value;
                    }
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = "system"; // TODO: Get from current user context
                    // Prevent modification of CreatedAt and CreatedBy
                    entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                    entry.Property(nameof(BaseEntity.CreatedBy)).IsModified = false;
                    break;

                case EntityState.Deleted:
                    // Implement soft delete
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = now;
                    entry.Entity.DeletedBy = "system"; // TODO: Get from current user context
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
