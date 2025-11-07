using System.ComponentModel.DataAnnotations;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Segment;

/// <summary>
/// Request DTO for creating a new Segment
/// </summary>
public class CreateSegmentRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required]
    public SegmentType Type { get; set; } = SegmentType.Dynamic;

    /// <summary>
    /// JSON-serialized rules for dynamic segments
    /// Example: {"field":"LifecycleStage","operator":"equals","value":"AtRisk"}
    /// </summary>
    public string? RulesJson { get; set; }

    public bool IsActive { get; set; } = true;
}
