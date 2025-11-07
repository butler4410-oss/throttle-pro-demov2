using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs;

/// <summary>
/// Summary data for the main dashboard
/// </summary>
public class DashboardSummary
{
    public int TotalCustomers { get; set; }
    public int NewCustomersThisMonth { get; set; }
    public int ActiveCustomers { get; set; }
    public int AtRiskCustomers { get; set; }
    public int LapsedCustomers { get; set; }
    public int LostCustomers { get; set; }
    
    public decimal TotalRevenue { get; set; }
    public decimal RevenueThisMonth { get; set; }
    public decimal AverageOrderValue { get; set; }
    
    public int TotalVisits { get; set; }
    public int VisitsThisMonth { get; set; }
    
    public int ActiveCampaigns { get; set; }
    public int TotalCampaigns { get; set; }
    public decimal AverageROAS { get; set; }
    
    public int ActiveSegments { get; set; }
    public int TotalSegments { get; set; }
    
    public Dictionary<CustomerLifecycleStage, int> LifecycleBreakdown { get; set; } = new();
    public List<RecentActivity> RecentActivities { get; set; } = new();
}

public class RecentActivity
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string? CustomerName { get; set; }
}
