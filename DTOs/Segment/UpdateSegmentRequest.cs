using System.ComponentModel.DataAnnotations;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Segment;

/// <summary>
/// Request DTO for updating an existing Segment (PATCH support - all nullable)
/// </summary>
public class UpdateSegmentRequest
{
    [MaxLength(200)]
    public string? Name { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    public SegmentType? Type { get; set; }

    /// <summary>
    /// JSON-serialized rules for dynamic segments
    /// </summary>
    public string? RulesJson { get; set; }

    public bool? IsActive { get; set; }
}
