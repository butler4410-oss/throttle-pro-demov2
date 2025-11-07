using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a service visit transaction
/// </summary>
public class Visit
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid ParentId { get; set; }

    [Required]
    public Guid StoreId { get; set; }

    public Guid? VehicleId { get; set; }

    public Guid? CouponId { get; set; }

    [MaxLength(100)]
    public string? InvoiceNumber { get; set; }

    [Required]
    public DateTime VisitDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DiscountAmount { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal NetAmount { get; set; }

    [MaxLength(500)]
    public string? ServicesPerformed { get; set; }

    public int? VehicleMileage { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(ParentId))]
    public virtual Parent Parent { get; set; } = null!;

    [ForeignKey(nameof(StoreId))]
    public virtual Store Store { get; set; } = null!;

    [ForeignKey(nameof(VehicleId))]
    public virtual Vehicle? Vehicle { get; set; }

    [ForeignKey(nameof(CouponId))]
    public virtual Coupon? Coupon { get; set; }
}
