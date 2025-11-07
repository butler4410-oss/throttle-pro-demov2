using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a marketing campaign
/// </summary>
[Index(nameof(ParentId), nameof(Status))]
[Index(nameof(ParentId), nameof(StartDate))]
[Index(nameof(SegmentId))]
[Index(nameof(ParentId), nameof(Channel))]
public class Campaign : TenantEntity
{

    public Guid? SegmentId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public CampaignStatus Status { get; set; } = CampaignStatus.Draft;

    public CampaignChannel Channel { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Budget { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Spent { get; set; } = 0;

    public int TargetAudience { get; set; } = 0;

    public int Sent { get; set; } = 0;

    public int Delivered { get; set; } = 0;

    public int Opened { get; set; } = 0;

    public int Clicked { get; set; } = 0;

    public int Redeemed { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Revenue { get; set; } = 0;

    [Column(TypeName = "decimal(10,2)")]
    public decimal ROAS { get; set; } = 0;

    // Navigation properties;

    [ForeignKey(nameof(SegmentId))]
    public virtual Segment? Segment { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
