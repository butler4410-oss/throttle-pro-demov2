using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents an automated customer journey
/// </summary>
public class Journey
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

    public JourneyTriggerType TriggerType { get; set; }

    public bool IsActive { get; set; } = false;

    public int TotalEnrolled { get; set; } = 0;

    public int TotalCompleted { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey(nameof(ParentId))]
    public virtual Parent Parent { get; set; } = null!;

    [ForeignKey(nameof(SegmentId))]
    public virtual Segment? Segment { get; set; }

    public virtual ICollection<JourneyStep> Steps { get; set; } = new List<JourneyStep>();
}
