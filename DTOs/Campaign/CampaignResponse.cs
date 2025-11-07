using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Campaign;

/// <summary>
/// Response DTO for Campaign entity - includes all display data
/// </summary>
public class CampaignResponse
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public Guid? SegmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public CampaignStatus Status { get; set; }
    public CampaignChannel Channel { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Budget and spending
    public decimal Budget { get; set; }
    public decimal Spent { get; set; }

    // Performance metrics
    public int TargetAudience { get; set; }
    public int Sent { get; set; }
    public int Delivered { get; set; }
    public int Opened { get; set; }
    public int Clicked { get; set; }
    public int Redeemed { get; set; }
    public decimal Revenue { get; set; }
    public decimal ROAS { get; set; }

    // Calculated metrics
    public decimal? OpenRate => Sent > 0 ? (decimal)Opened / Sent * 100 : null;
    public decimal? ClickRate => Opened > 0 ? (decimal)Clicked / Opened * 100 : null;
    public decimal? RedemptionRate => Sent > 0 ? (decimal)Redeemed / Sent * 100 : null;

    // Metadata
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
