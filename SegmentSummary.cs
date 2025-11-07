using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Shared.DTOs;

/// <summary>
/// Summary data for a segment
/// </summary>
public class SegmentSummary
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public SegmentType Type { get; set; }
    public int CustomerCount { get; set; }
    public DateTime? LastCalculatedAt { get; set; }
    public bool IsActive { get; set; }
    public int ActiveCampaigns { get; set; }
}
