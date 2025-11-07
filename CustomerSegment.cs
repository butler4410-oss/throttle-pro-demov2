using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Junction table for Customer-Segment many-to-many relationship
/// </summary>
public class CustomerSegment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid CustomerId { get; set; }

    [Required]
    public Guid SegmentId { get; set; }

    [Required]
    public Guid ParentId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey(nameof(SegmentId))]
    public virtual Segment Segment { get; set; } = null!;

    [ForeignKey(nameof(ParentId))]
    public virtual Parent Parent { get; set; } = null!;
}
