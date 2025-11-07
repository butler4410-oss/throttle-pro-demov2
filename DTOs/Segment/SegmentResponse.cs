using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs.Segment;

/// <summary>
/// Response DTO for Segment entity - includes all display data
/// </summary>
public class SegmentResponse
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public SegmentType Type { get; set; }
    public string? RulesJson { get; set; }
    public int CustomerCount { get; set; }
    public DateTime? LastCalculatedAt { get; set; }
    public bool IsActive { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
