namespace ThrottlePro.Shared.DTOs;

/// <summary>
/// ROAS and campaign performance metrics
/// </summary>
public class ROASSummary
{
    public int TotalCampaigns { get; set; }
    public int ActiveCampaigns { get; set; }
    public decimal TotalSpent { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal AverageROAS { get; set; }
    public int TotalSent { get; set; }
    public int TotalRedeemed { get; set; }
    public decimal RedemptionRate { get; set; }
}
