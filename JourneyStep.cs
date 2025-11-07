using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.Entities;

/// <summary>
/// Represents a step in a customer journey
/// </summary>
public class JourneyStep
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid JourneyId { get; set; }

    [Required]
    public Guid ParentId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public int Order { get; set; }

    public CampaignChannel Channel { get; set; }

    public int DelayDays { get; set; } = 0;

    public int DelayHours { get; set; } = 0;

    [MaxLength(2000)]
    public string? ContentTemplate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    [ForeignKey(nameof(JourneyId))]
    public virtual Journey Journey { get; set; } = null!;

    [ForeignKey(nameof(ParentId))]
    public virtual Parent Parent { get; set; } = null!;
}
