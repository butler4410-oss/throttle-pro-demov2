using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a marketing campaign
/// </summary>
public class Campaign
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ParentId { get; set; }

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

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey(nameof(ParentId))]
    public virtual Parent Parent { get; set; } = null!;

    [ForeignKey(nameof(SegmentId))]
    public virtual Segment? Segment { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
