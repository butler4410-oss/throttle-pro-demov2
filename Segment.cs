using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a customer segment with rules or static membership
/// </summary>
public class Segment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid ParentId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public SegmentType Type { get; set; } = SegmentType.Dynamic;

    /// <summary>
    /// JSON-serialized rules for dynamic segments
    /// </summary>
    public string? RulesJson { get; set; }

    public int CustomerCount { get; set; } = 0;

    public DateTime? LastCalculatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey(nameof(ParentId))]
    public virtual Parent Parent { get; set; } = null!;

    public virtual ICollection<CustomerSegment> CustomerSegments { get; set; } = new List<CustomerSegment>();
    public virtual ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
    public virtual ICollection<Journey> Journeys { get; set; } = new List<Journey>();
}
