using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Junction table for Customer-Segment many-to-many relationship
/// </summary>
[Index(nameof(CustomerId), nameof(SegmentId), IsUnique = true)]
[Index(nameof(SegmentId), nameof(CustomerId))]
[Index(nameof(ParentId), nameof(SegmentId))]
public class CustomerSegment : TenantEntity
{
    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid SegmentId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(SegmentId))]
    public virtual Segment Segment { get; set; } = null!
}
