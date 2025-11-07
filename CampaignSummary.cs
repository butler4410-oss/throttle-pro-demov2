using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs;

/// <summary>
/// Summary data for a campaign
/// </summary>
public class CampaignSummary
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CampaignStatus Status { get; set; }
    public CampaignChannel Channel { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public decimal Budget { get; set; }
    public decimal Spent { get; set; }
    public int TargetAudience { get; set; }
    public int Sent { get; set; }
    public int Delivered { get; set; }
    public int Opened { get; set; }
    public int Clicked { get; set; }
    public int Redeemed { get; set; }
    public decimal Revenue { get; set; }
    public decimal ROAS { get; set; }
    
    public string? SegmentName { get; set; }
}
