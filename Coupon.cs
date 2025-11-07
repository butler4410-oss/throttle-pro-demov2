using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a coupon/offer sent to customers
/// </summary>
[Index(nameof(Code), IsUnique = true)]
[Index(nameof(ParentId), nameof(IsRedeemed))]
[Index(nameof(CampaignId))]
[Index(nameof(CustomerId))]
[Index(nameof(ParentId), nameof(ExpirationDate))]
public class Coupon : TenantEntity
{

    public Guid? CampaignId { get; set; }

    public Guid? CustomerId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; }

    public int? DiscountPercentage { get; set; }

    public DateTime? SentDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public DateTime? RedeemedDate { get; set; }

    public bool IsRedeemed { get; set; } = false;

    // Navigation properties;

    [ForeignKey(nameof(CampaignId))]
    public virtual Campaign? Campaign { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
