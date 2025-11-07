using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a service visit transaction
/// </summary>
[Index(nameof(CustomerId), nameof(VisitDate))]
[Index(nameof(StoreId), nameof(VisitDate))]
[Index(nameof(ParentId), nameof(VisitDate))]
[Index(nameof(InvoiceNumber))]
public class Visit : TenantEntity
{
    [Required]
    public Guid CustomerId { get; set; }

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

    // Navigation properties
    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(StoreId))]
    public virtual Store Store { get; set; } = null!;

    [ForeignKey(nameof(VehicleId))]
    public virtual Vehicle? Vehicle { get; set; }

    [ForeignKey(nameof(CouponId))]
    public virtual Coupon? Coupon { get; set; }
}
