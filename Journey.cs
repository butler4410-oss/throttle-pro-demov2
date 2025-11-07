using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents an automated customer journey
/// </summary>
[Index(nameof(ParentId), nameof(IsActive))]
[Index(nameof(ParentId), nameof(TriggerType))]
[Index(nameof(SegmentId))]
public class Journey : TenantEntity
{

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

    // Navigation properties;

    [ForeignKey(nameof(SegmentId))]
    public virtual Segment? Segment { get; set; }

    public virtual ICollection<JourneyStep> Steps { get; set; } = new List<JourneyStep>();
}
