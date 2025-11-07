using System.Text.Json;
using ThrottlePro.Shared.DTOs.Reporting;
using ThrottlePro.Shared.Entities;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Data;

/// <summary>
/// Provides pre-configured system report templates for common business scenarios
/// These templates are available to all tenants and cannot be modified or deleted
/// </summary>
public static class SystemReportTemplates
{
    /// <summary>
    /// Gets all available system report templates
    /// </summary>
    /// <param name="systemParentId">Parent ID to use for system templates (typically Guid.Empty or system tenant)</param>
    /// <returns>List of all system report templates</returns>
    public static List<ReportTemplate> GetAllTemplates(Guid systemParentId)
    {
        return new List<ReportTemplate>
        {
            CustomerLifecycleDistribution(systemParentId),
            CampaignROASAnalysis(systemParentId),
            MonthlyRevenueTrend(systemParentId),
            TopCustomersByLifetimeValue(systemParentId),
            StorePerformanceComparison(systemParentId),
            AtRiskCustomerAlert(systemParentId),
            CampaignChannelComparison(systemParentId),
            DailyVisitsSummary(systemParentId),
            CustomerAcquisitionFunnel(systemParentId),
            SegmentPerformanceReport(systemParentId)
        };
    }

    /// <summary>
    /// Customer Lifecycle Distribution - Pie chart showing customer breakdown by lifecycle stage
    /// </summary>
    private static ReportTemplate CustomerLifecycleDistribution(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Customer",
            Fields = new List<ReportField>
            {
                new() { FieldName = "LifecycleStage", DisplayName = "Lifecycle Stage", Order = 1, IsVisible = true },
                new() { FieldName = "Id", DisplayName = "Count", Aggregation = AggregationType.Count, Order = 2, IsVisible = true, Format = "#,##0" }
            },
            GroupBy = new List<ReportGroupBy>
            {
                new() { FieldName = "LifecycleStage" }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "LifecycleStage", Descending = false }
            }
        };

        var visualization = new ReportVisualization
        {
            ChartType = ChartType.Pie,
            XAxisField = "LifecycleStage",
            YAxisFields = new List<string> { "Count" },
            Colors = new List<string> { "#00A3E0", "#002D72", "#FFC72C", "#FF6B6B", "#95E1D3" },
            ShowLegend = true,
            ShowDataLabels = true,
            Title = "Customer Lifecycle Distribution"
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Customer Lifecycle Distribution",
            Description = "Shows the distribution of customers across lifecycle stages (New, Active, At Risk, Lapsed, Lost)",
            Category = ReportCategory.Lifecycle,
            Type = ReportType.Chart,
            ConfigurationJson = JsonSerializer.Serialize(config),
            VisualizationJson = JsonSerializer.Serialize(visualization),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Campaign ROAS Analysis - Table showing detailed campaign performance metrics
    /// </summary>
    private static ReportTemplate CampaignROASAnalysis(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Campaign",
            Fields = new List<ReportField>
            {
                new() { FieldName = "Name", DisplayName = "Campaign Name", Order = 1, IsVisible = true },
                new() { FieldName = "Channel", DisplayName = "Channel", Order = 2, IsVisible = true },
                new() { FieldName = "Sent", DisplayName = "Sent", Order = 3, IsVisible = true, Format = "#,##0" },
                new() { FieldName = "Redeemed", DisplayName = "Redeemed", Order = 4, IsVisible = true, Format = "#,##0" },
                new() { FieldName = "Spent", DisplayName = "Spent", Order = 5, IsVisible = true, Format = "$#,##0.00" },
                new() { FieldName = "Revenue", DisplayName = "Revenue", Order = 6, IsVisible = true, Format = "$#,##0.00" },
                new() { FieldName = "ROAS", DisplayName = "ROAS", Order = 7, IsVisible = true, Format = "0.00x" }
            },
            Filters = new List<ReportFilter>
            {
                new() { FieldName = "Status", Operator = "In", Value = new[] { "Active", "Completed" } }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "ROAS", Descending = true }
            }
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Campaign Performance - ROAS Analysis",
            Description = "Detailed analysis of campaign performance with ROAS calculations showing return on advertising spend",
            Category = ReportCategory.Campaign,
            Type = ReportType.Table,
            ConfigurationJson = JsonSerializer.Serialize(config),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Monthly Revenue Trend - Line chart showing revenue over time
    /// </summary>
    private static ReportTemplate MonthlyRevenueTrend(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Visit",
            Fields = new List<ReportField>
            {
                new() { FieldName = "VisitDate", DisplayName = "Month", Order = 1, IsVisible = true, Format = "MMM yyyy" },
                new() { FieldName = "NetAmount", DisplayName = "Revenue", Aggregation = AggregationType.Sum, Format = "$#,##0.00", Order = 2, IsVisible = true },
                new() { FieldName = "Id", DisplayName = "Visit Count", Aggregation = AggregationType.Count, Order = 3, IsVisible = true, Format = "#,##0" }
            },
            GroupBy = new List<ReportGroupBy>
            {
                new() { FieldName = "VisitDate", DateInterval = "month" }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "VisitDate", Descending = false }
            }
        };

        var visualization = new ReportVisualization
        {
            ChartType = ChartType.Line,
            XAxisField = "VisitDate",
            YAxisFields = new List<string> { "Revenue" },
            Colors = new List<string> { "#00A3E0" },
            ShowLegend = false,
            ShowDataLabels = false,
            Title = "Monthly Revenue Trend",
            XAxisLabel = "Month",
            YAxisLabel = "Revenue ($)"
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Monthly Revenue Trend",
            Description = "Track revenue trends over time by month with visit counts",
            Category = ReportCategory.Revenue,
            Type = ReportType.Trend,
            ConfigurationJson = JsonSerializer.Serialize(config),
            VisualizationJson = JsonSerializer.Serialize(visualization),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Top Customers by Lifetime Value - Table showing highest value customers
    /// </summary>
    private static ReportTemplate TopCustomersByLifetimeValue(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Customer",
            Fields = new List<ReportField>
            {
                new() { FieldName = "FirstName", DisplayName = "First Name", Order = 1, IsVisible = true },
                new() { FieldName = "LastName", DisplayName = "Last Name", Order = 2, IsVisible = true },
                new() { FieldName = "Email", DisplayName = "Email", Order = 3, IsVisible = true },
                new() { FieldName = "TotalSpent", DisplayName = "Lifetime Value", Order = 4, IsVisible = true, Format = "$#,##0.00" },
                new() { FieldName = "TotalVisits", DisplayName = "Total Visits", Order = 5, IsVisible = true, Format = "#,##0" },
                new() { FieldName = "AverageOrderValue", DisplayName = "Avg Order Value", Order = 6, IsVisible = true, Format = "$#,##0.00" },
                new() { FieldName = "LastVisitDate", DisplayName = "Last Visit", Order = 7, IsVisible = true, Format = "MMM dd, yyyy" }
            },
            Filters = new List<ReportFilter>
            {
                new() { FieldName = "IsActive", Operator = "Equals", Value = true }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "TotalSpent", Descending = true }
            },
            Pagination = new ReportPagination
            {
                PageSize = 50,
                CurrentPage = 1
            }
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Top Customers by Lifetime Value",
            Description = "Ranked list of customers by total lifetime spending with visit history",
            Category = ReportCategory.Customer,
            Type = ReportType.Table,
            ConfigurationJson = JsonSerializer.Serialize(config),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Store Performance Comparison - Bar chart comparing stores by revenue
    /// </summary>
    private static ReportTemplate StorePerformanceComparison(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Visit",
            Fields = new List<ReportField>
            {
                new() { FieldName = "StoreId", DisplayName = "Store", Order = 1, IsVisible = true },
                new() { FieldName = "NetAmount", DisplayName = "Revenue", Aggregation = AggregationType.Sum, Format = "$#,##0.00", Order = 2, IsVisible = true },
                new() { FieldName = "Id", DisplayName = "Visit Count", Aggregation = AggregationType.Count, Order = 3, IsVisible = true, Format = "#,##0" }
            },
            GroupBy = new List<ReportGroupBy>
            {
                new() { FieldName = "StoreId" }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "Revenue", Descending = true }
            }
        };

        var visualization = new ReportVisualization
        {
            ChartType = ChartType.Bar,
            XAxisField = "StoreId",
            YAxisFields = new List<string> { "Revenue" },
            Colors = new List<string> { "#002D72" },
            ShowLegend = false,
            ShowDataLabels = true,
            Title = "Store Performance Comparison",
            XAxisLabel = "Store",
            YAxisLabel = "Revenue ($)"
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Store Performance Comparison",
            Description = "Compare revenue and visit counts across all store locations",
            Category = ReportCategory.Store,
            Type = ReportType.Comparison,
            ConfigurationJson = JsonSerializer.Serialize(config),
            VisualizationJson = JsonSerializer.Serialize(visualization),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// At-Risk Customer Alert - Table showing customers at risk of lapsing
    /// </summary>
    private static ReportTemplate AtRiskCustomerAlert(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Customer",
            Fields = new List<ReportField>
            {
                new() { FieldName = "FirstName", DisplayName = "First Name", Order = 1, IsVisible = true },
                new() { FieldName = "LastName", DisplayName = "Last Name", Order = 2, IsVisible = true },
                new() { FieldName = "Email", DisplayName = "Email", Order = 3, IsVisible = true },
                new() { FieldName = "Phone", DisplayName = "Phone", Order = 4, IsVisible = true },
                new() { FieldName = "LastVisitDate", DisplayName = "Last Visit", Order = 5, IsVisible = true, Format = "MMM dd, yyyy" },
                new() { FieldName = "DaysSinceLastVisit", DisplayName = "Days Since Visit", Order = 6, IsVisible = true, Format = "#,##0" },
                new() { FieldName = "TotalSpent", DisplayName = "Lifetime Value", Order = 7, IsVisible = true, Format = "$#,##0.00" }
            },
            Filters = new List<ReportFilter>
            {
                new() { FieldName = "LifecycleStage", Operator = "Equals", Value = "AtRisk" },
                new() { FieldName = "IsActive", Operator = "Equals", Value = true, LogicalOperator = "AND" }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "TotalSpent", Descending = true }
            }
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "At-Risk Customer Alert",
            Description = "Identifies valuable customers who haven't visited recently and are at risk of lapsing",
            Category = ReportCategory.Lifecycle,
            Type = ReportType.Table,
            ConfigurationJson = JsonSerializer.Serialize(config),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Campaign Channel Comparison - Bar chart comparing campaign channels
    /// </summary>
    private static ReportTemplate CampaignChannelComparison(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Campaign",
            Fields = new List<ReportField>
            {
                new() { FieldName = "Channel", DisplayName = "Channel", Order = 1, IsVisible = true },
                new() { FieldName = "Sent", DisplayName = "Total Sent", Aggregation = AggregationType.Sum, Order = 2, IsVisible = true, Format = "#,##0" },
                new() { FieldName = "Redeemed", DisplayName = "Total Redeemed", Aggregation = AggregationType.Sum, Order = 3, IsVisible = true, Format = "#,##0" },
                new() { FieldName = "Revenue", DisplayName = "Total Revenue", Aggregation = AggregationType.Sum, Order = 4, IsVisible = true, Format = "$#,##0.00" },
                new() { FieldName = "ROAS", DisplayName = "Avg ROAS", Aggregation = AggregationType.Average, Order = 5, IsVisible = true, Format = "0.00x" }
            },
            GroupBy = new List<ReportGroupBy>
            {
                new() { FieldName = "Channel" }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "Revenue", Descending = true }
            }
        };

        var visualization = new ReportVisualization
        {
            ChartType = ChartType.Bar,
            XAxisField = "Channel",
            YAxisFields = new List<string> { "Revenue" },
            Colors = new List<string> { "#FFC72C" },
            ShowLegend = false,
            ShowDataLabels = true,
            Title = "Campaign Channel Performance",
            XAxisLabel = "Channel",
            YAxisLabel = "Revenue ($)"
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Campaign Channel Comparison",
            Description = "Compare performance metrics across different marketing channels (Email, SMS, Direct Mail, etc.)",
            Category = ReportCategory.Campaign,
            Type = ReportType.Comparison,
            ConfigurationJson = JsonSerializer.Serialize(config),
            VisualizationJson = JsonSerializer.Serialize(visualization),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Daily Visits Summary - Table showing daily visit and revenue statistics
    /// </summary>
    private static ReportTemplate DailyVisitsSummary(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Visit",
            Fields = new List<ReportField>
            {
                new() { FieldName = "VisitDate", DisplayName = "Date", Order = 1, IsVisible = true, Format = "MMM dd, yyyy" },
                new() { FieldName = "Id", DisplayName = "Visit Count", Aggregation = AggregationType.Count, Order = 2, IsVisible = true, Format = "#,##0" },
                new() { FieldName = "NetAmount", DisplayName = "Revenue", Aggregation = AggregationType.Sum, Order = 3, IsVisible = true, Format = "$#,##0.00" },
                new() { FieldName = "NetAmount", DisplayName = "Avg Ticket", Aggregation = AggregationType.Average, Order = 4, IsVisible = true, Format = "$#,##0.00" }
            },
            GroupBy = new List<ReportGroupBy>
            {
                new() { FieldName = "VisitDate", DateInterval = "day" }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "VisitDate", Descending = true }
            },
            Pagination = new ReportPagination
            {
                PageSize = 30,
                CurrentPage = 1
            }
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Daily Visits Summary",
            Description = "Daily breakdown of visit counts, revenue, and average ticket value",
            Category = ReportCategory.Executive,
            Type = ReportType.Table,
            ConfigurationJson = JsonSerializer.Serialize(config),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Customer Acquisition Funnel - Funnel chart showing customer journey stages
    /// </summary>
    private static ReportTemplate CustomerAcquisitionFunnel(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Customer",
            Fields = new List<ReportField>
            {
                new() { FieldName = "LifecycleStage", DisplayName = "Stage", Order = 1, IsVisible = true },
                new() { FieldName = "Id", DisplayName = "Count", Aggregation = AggregationType.Count, Order = 2, IsVisible = true, Format = "#,##0" }
            },
            Filters = new List<ReportFilter>
            {
                new() { FieldName = "LifecycleStage", Operator = "In", Value = new[] { "New", "Active" } }
            },
            GroupBy = new List<ReportGroupBy>
            {
                new() { FieldName = "LifecycleStage" }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "LifecycleStage", Descending = false }
            }
        };

        var visualization = new ReportVisualization
        {
            ChartType = ChartType.Funnel,
            XAxisField = "LifecycleStage",
            YAxisFields = new List<string> { "Count" },
            Colors = new List<string> { "#00A3E0", "#002D72" },
            ShowLegend = true,
            ShowDataLabels = true,
            Title = "Customer Acquisition Funnel"
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Customer Acquisition Funnel",
            Description = "Visualize customer progression through acquisition and activation stages",
            Category = ReportCategory.Customer,
            Type = ReportType.Chart,
            ConfigurationJson = JsonSerializer.Serialize(config),
            VisualizationJson = JsonSerializer.Serialize(visualization),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Segment Performance Report - Table showing segment sizes and engagement
    /// </summary>
    private static ReportTemplate SegmentPerformanceReport(Guid parentId)
    {
        var config = new ReportConfiguration
        {
            DataSource = "Segment",
            Fields = new List<ReportField>
            {
                new() { FieldName = "Name", DisplayName = "Segment Name", Order = 1, IsVisible = true },
                new() { FieldName = "Type", DisplayName = "Type", Order = 2, IsVisible = true },
                new() { FieldName = "CustomerCount", DisplayName = "Customer Count", Order = 3, IsVisible = true, Format = "#,##0" },
                new() { FieldName = "Description", DisplayName = "Description", Order = 4, IsVisible = true }
            },
            Filters = new List<ReportFilter>
            {
                new() { FieldName = "IsActive", Operator = "Equals", Value = true }
            },
            Sorting = new List<ReportSort>
            {
                new() { FieldName = "CustomerCount", Descending = true }
            }
        };

        return new ReportTemplate
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = "Segment Performance Report",
            Description = "Overview of all customer segments with member counts and definitions",
            Category = ReportCategory.Customer,
            Type = ReportType.Table,
            ConfigurationJson = JsonSerializer.Serialize(config),
            IsSystemTemplate = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }
}
